using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBBlackListItem : MonoBehaviour
    {
        private bool isFirst = true;
        private Image Icon, Sex;
        private Text Name, Sign, btn_Txt;
        private GameObject Btn;
        private UserFriend m_Data;

        void SetObj()
        {
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            Sex = GameTools.GetByName(transform, "Sex").GetComponent<Image>();
            Name = GameTools.GetByName(transform, "Name").GetComponent<Text>();
            Sign = GameTools.GetByName(transform, "Sign").GetComponent<Text>();
            btn_Txt = GameTools.GetByName(transform, "btn_Txt").GetComponent<Text>();
            Btn = GameTools.GetByName(transform, "Btn");
            GameTools.Instance.AddClickEvent(Btn, () =>
            {
                CommonPopWinPanel.Instance.ShowPopOne("你是否将"+ m_Data.nickName+ "移出黑名单",okAction:()=> {
                    //移出黑名单
                    NetMgr.Instance.C2S_Friend_PullInOrPullOutBlackList(GameData.userId, m_Data.userId, 6);
                }
                );
               
            });

            //GameTools.Instance.AddClickEvent(gameObject, () =>
            //{
            //    debug.Log_yellow("查看黑名单好友");
            //    GameData.curFriendUserId = m_Data.userId;
            //    GameData.isLookAttentionProfile = true;
            //    UIMgr.Instance.Open(UIPath.UIBFriendProfilePanel);
            //});
        }


        public void InitItem(UserFriend data, bool _isMyFocus)
        {
            if (isFirst)
            {
                SetObj();
                isFirst = false;
            }
            m_Data = data;

            //头像
            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (Icon == null) return;
                Icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(Icon);
            }, data.headImgUrl);
            //性别 1男 0女
            if (data.gender == 1)
            {
                UIBFriendHomePanel.IconQieHuan.SetImg(6, Sex.gameObject);
            }
            else
            {
                UIBFriendHomePanel.IconQieHuan.SetImg(7, Sex.gameObject);
            }
            //昵称
            Name.text = data.nickName;
            //签名
            Sign.text = data.personalizedSignature;

        }

        //判断是否互相关注
        bool getEachState(int _userId, List<AttentionToMeList> dataList)
        {
            if (dataList.Count == 0) return false;
            bool isEach = false;
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i].userId == _userId)
                {
                    isEach = true;
                }
            }
            return isEach;
        }

        void OnDestroy()
        {
            GameData.curClickFocusId = 0;
        }
    }
}
