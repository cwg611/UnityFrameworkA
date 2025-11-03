using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBFriendFocusItem : MonoBehaviour
    {
        private bool isFirst = true;
        private Image Icon, Sex;
        private Text Name, Sign, btn_Txt;
        private GameObject Btn;
        private AttentionToMeList m_Data;
        private IsFocus m_FocusState; //关注状态

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
                 GameData.curClickFocusId = m_Data.userId;
                 if (m_FocusState == IsFocus.Focus || m_FocusState == IsFocus.AllFocus)
                 {
                     //去取消关注
                     DataMgr.Instance.UnfollowToUserReq.userId = GameData.userId;
                     DataMgr.Instance.UnfollowToUserReq.toAttentionUser = m_Data.userId;
                     UIBFriendFocusListPanel.Act_OpenWindow(true);
                 }
                 else if (m_FocusState == IsFocus.UnFocus)
                 {
                     //去关注
                     DataMgr.Instance.followToUserReq.userId = GameData.userId;
                     DataMgr.Instance.followToUserReq.toAttentionUser = m_Data.userId;
                     NetMgr.Instance.C2S_Friend_FollowToUser();
                 }
             });

            GameTools.Instance.AddClickEvent(gameObject, () =>
            {
                GameData.curFriendUserId = m_Data.userId;
                GameData.isLookAttentionProfile = true;
                UIMgr.Instance.Open(UIPath.UIBFriendProfilePanel);
            });
            //关注
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_FollowToUser, S2C_Friend_FollowToUserCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_UnFollowToUser, S2C_Friend_UnFollowToUserCallBack);
        }

        void setFocusView()
        {
            // 4  5  6
            if (m_FocusState == IsFocus.Focus)
            {
                //UIBFriendFocusListPanel.imgQiehuan.SetImg(5, Btn);
                Btn.GetComponent<Image>().color = new Color32(63,63,63,255);
                btn_Txt.text = "已关注";
            }
            else if (m_FocusState == IsFocus.UnFocus)
            {
                //UIBFriendFocusListPanel.imgQiehuan.SetImg(4, Btn);
                Btn.GetComponent<Image>().color = new Color32(22,173,205,255);
                btn_Txt.text = "关注";
            }
            else if (m_FocusState == IsFocus.AllFocus)
            {
                //UIBFriendFocusListPanel.imgQiehuan.SetImg(6, Btn);
                Btn.GetComponent<Image>().color = new Color32(63, 63, 63, 255);
                btn_Txt.text = "互相关注";
            }
        }

        //_isMyFocus: true  我关注的   false: 
        public void InitItem(AttentionToMeList data, bool _isMyFocus)
        {
            if (isFirst)
            {
                SetObj();
                isFirst = false;
            }
            m_Data = data;
            //判断状态
            //如果是 我关注的，默认是  已经关注
            //再判断是否互相关注
            if (_isMyFocus)
            {
                m_FocusState = IsFocus.Focus;
                if (DataMgr.Instance.attentionListRes.attentionToMeList != null && getEachState(data.userId, DataMgr.Instance.attentionListRes.attentionToMeList))
                {
                    m_FocusState = IsFocus.AllFocus;
                }
            }
            //如果是关注我的，默认是   未关注
            else
            {
                m_FocusState = IsFocus.UnFocus;
                if (DataMgr.Instance.attentionListRes.meAttentionList != null && getEachState(data.userId, DataMgr.Instance.attentionListRes.meAttentionList))
                {
                    m_FocusState = IsFocus.AllFocus;
                }
            }
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
            //if (data.gender == 1)
            //{
            //    UIBFriendFocusListPanel.imgQiehuan.SetImg(2, Sex.gameObject);
            //}
            //else
            //{
            //    UIBFriendFocusListPanel.imgQiehuan.SetImg(3, Sex.gameObject);
            //}
            //昵称
            Name.text = data.nickName;
            //签名
            Sign.text = data.personalizedSignature;
            setFocusView();

            
        }

        //判断是否互相关注
        bool getEachState(long _userId, List<AttentionToMeList> dataList)
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
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_FollowToUser, S2C_Friend_FollowToUserCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_UnFollowToUser, S2C_Friend_UnFollowToUserCallBack);
            GameData.curClickFocusId = 0;
        }

        //关注接口回调
        void S2C_Friend_FollowToUserCallBack(object o)
        {
            if (GameData.curClickFocusId == m_Data.userId)
            {
                debug.Log_Blue("关注成功");
                //成功以后需要刷新数据
                NetMgr.Instance.C2S_Friend_AttentionList(GameData.userId.ToString());
            }
            //刷新我的资料关注数量
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
        }

        //取消关注回调
        void S2C_Friend_UnFollowToUserCallBack(object o)
        {
            if (GameData.curClickFocusId == m_Data.userId)
            {
                debug.Log_Blue("取消关注成功");
                UIBFriendFocusListPanel.Act_OpenWindow(false);
                //成功以后需要刷新数据
                NetMgr.Instance.C2S_Friend_AttentionList(GameData.userId.ToString());
            }
            //刷新我的资料关注数量
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
        }
    }
}

