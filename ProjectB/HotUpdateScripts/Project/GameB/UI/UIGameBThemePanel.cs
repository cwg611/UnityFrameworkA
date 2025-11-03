using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using My.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 解锁角色和主题
    /// </summary>
    public class UIGameBThemePanel : BasePanel
    {
        private RectTransform rectTransform;
        private Text txt_Process;
        private Image img_ButtonBg, img_CharacterTog, img_ThemeTog;
        private Toggle tog_Character, tog_Theme;

        private Color32 choosedColor = new Color32(), normalClolr = new Color32(72, 71, 71, 255);
        private Sprite themeSprite;

        private Button[] CharacterButtons, ThemeButtons;
        private ToggleGroup CharacterGroup, ThemeGroup;

        private Material mat_UIGrey;

        private List<int> characterUnLocked;
        private List<int> themeUnLocked;
        private Dictionary<int, int> characterStateDic;//1 未解锁 2 待解锁 3 已解锁
        private Dictionary<int, int> themeStateDic;

        private bool openCharacter = true;

        public override void InitPanel(object o)
        {
            base.InitPanel(o);

            rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.zero;

            //InitView
            img_ButtonBg = transform.Find("BackButton/ButtonBg").GetComponent<Image>();
            txt_Process = transform.Find("ProcessText").GetComponent<Text>();
            tog_Character = transform.Find("CharacterToggle").GetComponent<Toggle>();
            tog_Theme = transform.Find("ThemeToggle").GetComponent<Toggle>();
            img_CharacterTog = tog_Character.GetComponent<Image>();
            img_ThemeTog = tog_Theme.GetComponent<Image>();

            CharacterButtons = transform.Find("CharacterView/CharacterItems").GetComponentsInChildren<Button>();
            ThemeButtons = transform.Find("ThemeView/ThemeItems").GetComponentsInChildren<Button>();

            mat_UIGrey = JResource.LoadRes<Material>("UI_Grey.mat", JResource.MatchMode.Material);

            characterUnLocked = new List<int>();
            themeUnLocked = new List<int>();
            characterStateDic = new Dictionary<int, int>();
            for (int i = 1; i <= 9; i++)
            {
                characterStateDic.Add(i, 1);
            }
            themeStateDic = new Dictionary<int, int>();
            for (int i = 1; i <= 6; i++)
            {
                themeStateDic.Add(i, 1);
            }
            //InitEvent
            tog_Character.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    img_CharacterTog.color = choosedColor;
                    img_ThemeTog.color = normalClolr;
                    txt_Process.text = GameBData.LockedCharacterNum + "/9";
                }
            });
            tog_Theme.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    img_CharacterTog.color = normalClolr;
                    img_ThemeTog.color = choosedColor;
                    txt_Process.text = GameBData.LockedThemeNum + "/6";
                }
            });

            for (int i = 0; i < CharacterButtons.Length; i++)
            {
                int index = i;
                CharacterButtons[index].onClick.AddListener(() => { CharacterItemOnClick(index); });
            }
            for (int i = 0; i < ThemeButtons.Length; i++)
            {
                int index = i;
                ThemeButtons[index].onClick.AddListener(() => { ThemeItemOnClick(index); });
            }
            transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                DOTweenMgr.Instance.DoScale(gameObject, Vector3.one, Vector3.zero, .15f, () =>
                {
                    MsgCenter.Call(null, MsgCode.GameB_SwitchCharacter, null);
                    UIMgr.Instance.Close(UIPath.UIGameBThemePanel);
                    UIMgr.Instance.Open(UIPath.UIGameBStartPanel);
                });
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_Jump_Get_Role_Theme, GetRoleThemeCallBack);

            SetViewThemeColor();

            //CharacterItemOnClick(GameBData.CharacterId);
            //ThemeItemOnClick(GameBData.ThemeId);

            NetMgr.Instance.C2S_Game_Jump_Get_Role_Theme();//获取解锁状态
        }

        public override void ReleasePanel()
        {
            base.ReleasePanel();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_Jump_Get_Role_Theme, GetRoleThemeCallBack);
        }

        //解锁状态回调
        private void GetRoleThemeCallBack(object o)
        {
            DataJumpGameUnlocked data = o as DataJumpGameUnlocked;
            for (int i = 0; i < data.toLockedRolesId.Count; i++)
            {
                characterStateDic[data.toLockedRolesId[i]] = 2;
            }
            for (int i = 0; i < data.unLockedRolesId.Count; i++)
            {
                characterStateDic[data.unLockedRolesId[i]] = 3;
            }

            for (int i = 0; i < data.toLockedThemesId.Count; i++)
            {
                themeStateDic[data.toLockedThemesId[i]] = 2;
            }
            for (int i = 0; i < data.unLockedThemesId.Count; i++)
            {
                themeStateDic[data.unLockedThemesId[i]] = 3;
            }
            characterUnLocked.AddRange(data.unLockedRolesId);
            themeUnLocked.AddRange(data.unLockedThemesId);

            openCharacter = (data.toLockedRolesId.Count == 0 && data.toLockedThemesId.Count > 0) ? false : true;

            GameBData.LockedCharacterNum = 9 - data.LockedRolesId.Count;
            GameBData.LockedThemeNum = 6 - data.LockedThemesId.Count;
            txt_Process.text = GameBData.LockedCharacterNum + "/9";

            CharacterItemOnClick(GameBData.CharacterId);
            debug.Log_yellow("GameBData.ThemeId--" + GameBData.ThemeId);
            ThemeItemOnClick(GameBData.ThemeId);
            SetLockedState();
            if (rectTransform.localScale==Vector3.zero)
            {
                tog_Character.isOn = openCharacter;
                tog_Theme.isOn = !openCharacter;
                DOTweenMgr.Instance.DoScale(gameObject, Vector3.zero, Vector3.one, .3f, null);
            }
        }

        private void CharacterItemOnClick(int index)
        {
            if (index == 0)//从已解锁中随机一个
            {
                for (int i = 0; i < CharacterButtons.Length; i++)
                {
                    CharacterButtons[i].transform.GetChild(3).localScale =
                        i == index ? Vector3.one : Vector3.zero;
                }
                index = characterUnLocked[UnityEngine.Random.Range(0, characterUnLocked.Count)];
                if (GameBData.CharacterId != index)
                {
                    GameBData.CharacterId = index;
                }
            }
            else
            {
                DataMgr.Instance.dataGameBUnLockedState.isCharacter = true;
                DataMgr.Instance.dataGameBUnLockedState.id = index;
                if (characterStateDic[index] == 1)//未解锁
                {
                    debug.Log_yellow("角色未解锁--" + index);
                    DataMgr.Instance.dataGameBUnLockedState.isUnLocked = false;
                    UIMgr.Instance.Open(UIPath.UIBUnLockProgressPanel);
                }
                else
                {
                    if (GameBData.CharacterId != index)
                    {
                        GameBData.CharacterId = index;
                    }
                    for (int i = 0; i < CharacterButtons.Length; i++)
                    {
                        CharacterButtons[i].transform.GetChild(3).localScale =
                            i == index ? Vector3.one : Vector3.zero;
                    }
                    if (characterStateDic[index] == 2)//新解锁
                    {
                        debug.Log_yellow("角色新解锁--" + index);
                        DataMgr.Instance.dataGameBUnLockedState.isUnLocked = true;
                        UIMgr.Instance.Open(UIPath.UIBUnLockProgressPanel);
                    }
                }
            }
        }

        private void ThemeItemOnClick(int index)
        {
            if (index == 0)//从已解锁中随机一个
            {
                for (int i = 0; i < ThemeButtons.Length; i++)
                {
                    ThemeButtons[i].transform.GetChild(1).localScale =
                        i == index ? Vector3.one : Vector3.zero;
                }
                index = themeUnLocked[UnityEngine.Random.Range(0, themeUnLocked.Count)];
                if (GameBData.ThemeId != index)
                {
                    GameBData.ThemeId = index;
                    SetViewThemeColor();
                    MsgCenter.Call(null, MsgCode.GameB_SwitchTheme, null);
                }
            }
            else
            {
                DataMgr.Instance.dataGameBUnLockedState.isCharacter = false;
                DataMgr.Instance.dataGameBUnLockedState.id = index;
                if (themeStateDic[index] == 1)
                {
                    debug.Log_yellow("主题未解锁--" + index);
                    DataMgr.Instance.dataGameBUnLockedState.isUnLocked = false;
                    UIMgr.Instance.Open(UIPath.UIBUnLockProgressPanel);
                }
                else
                {
                    if (GameBData.ThemeId != index)
                    {
                        GameBData.ThemeId = index;
                        SetViewThemeColor();
                        MsgCenter.Call(null, MsgCode.GameB_SwitchTheme, null);
                    }
                    for (int i = 0; i < ThemeButtons.Length; i++)
                    {
                        ThemeButtons[i].transform.GetChild(1).localScale =
                            i == index ? Vector3.one : Vector3.zero;
                    }
                    if (themeStateDic[index] == 2)
                    {
                        debug.Log_yellow("主题新解锁--" + index);
                        DataMgr.Instance.dataGameBUnLockedState.isUnLocked = true;
                        UIMgr.Instance.Open(UIPath.UIBUnLockProgressPanel);
                    }
                }
            }
        }

        //设置解锁状态
        private void SetLockedState()
        {
            for (int i = 1; i < CharacterButtons.Length; i++)
            {
                int state = characterStateDic[i];

                CharacterButtons[i].GetComponent<Image>().material = state == 1 ? mat_UIGrey : null;
                CharacterButtons[i].transform.GetChild(1).localScale = state == 1 ? Vector3.zero : Vector3.one;
                CharacterButtons[i].transform.GetChild(2).localScale = state == 1 ? Vector3.one : Vector3.zero;
                CharacterButtons[i].transform.GetChild(4).localScale = state == 1 ? Vector3.one : Vector3.zero;
                CharacterButtons[i].transform.GetChild(5).localScale = state == 2 ? Vector3.one : Vector3.zero;
                CharacterButtons[i].transform.GetChild(5).GetChild(0).GetComponent<Image>().color =
                    GameBData.ThemeColors[GameBData.ThemeId - 1];
            }

            for (int i = 1; i < ThemeButtons.Length; i++)
            {
                int state = themeStateDic[i];

                ThemeButtons[i].GetComponent<Image>().material = state == 1 ? mat_UIGrey : null;
                ThemeButtons[i].transform.GetChild(0).GetComponent<Image>().material = state == 1 ? mat_UIGrey : null;
                ThemeButtons[i].transform.GetChild(2).localScale = state == 1 ? Vector3.one : Vector3.zero;
                ThemeButtons[i].transform.GetChild(3).localScale = state == 2 ? Vector3.one : Vector3.zero;
                ThemeButtons[i].transform.GetChild(3).GetChild(0).GetComponent<Image>().color =
                    GameBData.ThemeColors[GameBData.ThemeId - 1];
            }
        }

        /// <summary>
        /// 设置页面主题颜色
        /// </summary>
        private void SetViewThemeColor()
        {
            //主题颜色
            choosedColor = GameBData.ThemeColors[GameBData.ThemeId - 1];

            themeSprite = JResource.LoadRes<Sprite>("FunJump/Theme/renwudi0" + GameBData.ThemeId + ".png", JResource.MatchMode.UI);

            img_ButtonBg.color = choosedColor;

            if (tog_Character.isOn)
            {
                img_CharacterTog.color = choosedColor;
            }
            else
            {
                img_ThemeTog.color = choosedColor;
            }

            if (themeSprite != null)
            {
                for (int i = 0; i < CharacterButtons.Length; i++)
                {
                    CharacterButtons[i].GetComponent<Image>().overrideSprite = themeSprite;
                }
                for (int i = 0; i < ThemeButtons.Length; i++)
                {
                    ThemeButtons[i].GetComponent<Image>().overrideSprite = themeSprite;
                }
            }
            //for (int i = 0; i < CharacterButtons.Length; i++)
            //{
            //CharacterButtons[i].transform.GetChild(5).GetComponent<Image>().color = GameBData.ThemeColors[GameBData.ThemeId - 1];

            //}

        }


    }
}
