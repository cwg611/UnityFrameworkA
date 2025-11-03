using DG.Tweening;
using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using HotUpdateScripts.Project.GameB;
using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using My.Msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 解锁状态弹窗
    /// </summary>
    public class UIBUnLockProgressPanel : BasePanel
    {
        private Image img_Character, img_Theme, img_EnsureBtn;

        private Text txt_Description, txt_Progress, txt_EnsureBtn;

        private DataGameBUnLockedState UnLockedState;

        private Material mat_UIGrey;

        private GameObject popWin;

        private Transform bgMask;

        public override void InitPanel(object o)
        {
            base.InitPanel(o);

            popWin = transform.Find("PopWin").gameObject;

            bgMask = transform.Find("BGMask");

            DOTweenMgr.Instance.DoScale(popWin, Vector3.zero, Vector3.one, .3f, null);

            UnLockedState = DataMgr.Instance.dataGameBUnLockedState;

            img_Character = transform.Find("PopWin/CharacterImage").GetComponent<Image>();
            img_Theme = transform.Find("PopWin/ThemeImage").GetComponent<Image>();
            img_EnsureBtn = transform.Find("PopWin/EnsureButton").GetComponent<Image>();

            txt_Description = transform.Find("PopWin/DescriptionText").GetComponent<Text>();
            txt_Progress = transform.Find("PopWin/ProgressText").GetComponent<Text>();
            txt_EnsureBtn = transform.Find("PopWin/EnsureButton/Text").GetComponent<Text>();

            mat_UIGrey = JResource.LoadRes<Material>("UI_Grey.mat", JResource.MatchMode.Material);


            transform.Find("PopWin/EnsureButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                DOTweenMgr.Instance.DoScale(gameObject, Vector3.one, Vector3.zero, .15f, () =>
                {
                    UIMgr.Instance.Close(UIPath.UIBUnLockProgressPanel);
                });
            });

            bgMask.GetComponent<Button>().onClick.AddListener(() =>
            {
                DOTweenMgr.Instance.DoScale(gameObject, Vector3.one, Vector3.zero, .15f, () =>
                {
                    UIMgr.Instance.Close(UIPath.UIBUnLockProgressPanel);
                });
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_Jump_Unlock_Role_Theme, OpenView);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_Jump_Check_Unlock_Condition, OpenView);

            if (UnLockedState.isUnLocked)//新解锁
            {
                if (UnLockedState.isCharacter)
                {
                    NetMgr.Instance.C2S_Game_Jump_Unlock_Role_Theme(true, UnLockedState.id);
                }
                else
                {
                    NetMgr.Instance.C2S_Game_Jump_Unlock_Role_Theme(false, UnLockedState.id);
                }
            }
            else//未解锁
            {
                if (UnLockedState.isCharacter)
                {
                    NetMgr.Instance.C2S_Game_Jump_Check_Unlock_Condition(true, UnLockedState.id);
                }
                else
                {
                    NetMgr.Instance.C2S_Game_Jump_Check_Unlock_Condition(false, UnLockedState.id);
                }
            }

            SetView();
        }

        private void SetView()
        {
            img_Character.transform.localScale = UnLockedState.isCharacter ? Vector3.one : Vector3.zero;
            img_Theme.transform.localScale = UnLockedState.isCharacter ? Vector3.zero : Vector3.one;
            if (UnLockedState.isUnLocked)//新解锁
            {
                if (UnLockedState.isCharacter)
                {
                    img_Character.sprite =
                        JResource.LoadRes<Sprite>("FunJump/Character/character0" + UnLockedState.id + "-1.png", JResource.MatchMode.UI);
                }
                else
                {
                    img_Theme.sprite = JResource.LoadRes<Sprite>("FunJump/Theme/theme0" + UnLockedState.id + ".png", JResource.MatchMode.UI);
                    img_Theme.material = null;
                }
            }
            else//未解锁
            {
                if (UnLockedState.isCharacter)
                {
                    img_Character.sprite =
                       JResource.LoadRes<Sprite>("FunJump/Character/character0" + UnLockedState.id + "-2.png", JResource.MatchMode.UI);
                }
                else
                {
                    img_Theme.sprite = JResource.LoadRes<Sprite>("FunJump/Theme/theme0" + UnLockedState.id + ".png", JResource.MatchMode.UI);
                    img_Theme.material = mat_UIGrey;
                }
            }

            var themeColor = GameBData.ThemeColors[GameBData.ThemeId - 1];
            txt_Description.color = themeColor;
            txt_Progress.color = new Color32(themeColor.r, themeColor.g, themeColor.b, 180);
            img_EnsureBtn.color = themeColor;
            //txt_EnsureBtn.color = themeColor;
        }

        private void OpenView(object o)
        {
            var data = o as DataUnLockProgress;
            if (UnLockedState.isUnLocked)
            {
                if (data.unlockSuccess)
                {
                    txt_Description.text = "已解锁";
                    NetMgr.Instance.C2S_Game_Jump_Get_Role_Theme();//刷新解锁状态
                }

            }
            else
            {
                switch (data.unlockConditionDescription)
                {
                    case "游戏局数":
                        txt_Description.text = "玩" + data.unlockConditionNumber + "局游戏";
                        break;
                    case "跳跃次数":
                        txt_Description.text = "跳跃" + data.unlockConditionNumber + "次";
                        break;
                    case "最高得分":
                        txt_Description.text = "得到" + data.unlockConditionNumber + "或更高分";
                        break;
                    case "连玩天数":
                        txt_Description.text = "连续玩" + data.unlockConditionNumber + "天";
                        break;
                    case "完美跳跃":
                        txt_Description.text = "完美跳跃" + data.unlockConditionNumber + "次";
                        break;
                    case "解锁角色":
                        txt_Description.text = "解锁" + data.unlockConditionNumber + "个角色";
                        break;
                    case "解锁主题":
                        txt_Description.text = "解锁" + data.unlockConditionNumber + "个主题";
                        break;
                    default:
                        break;
                }

                txt_Progress.text = data.userHave + "/" + data.unlockConditionNumber;
            }
        }


        public override void ReleasePanel()
        {
            base.ReleasePanel();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_Jump_Unlock_Role_Theme, OpenView);

            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_Jump_Check_Unlock_Condition, OpenView);

        }
    }
}
