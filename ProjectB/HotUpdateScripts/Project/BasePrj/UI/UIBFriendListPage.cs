using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using System;
using UnityEngine.EventSystems;

namespace My.UI.Panel
{
    public class UIBFriendListPage : FriendPageBase
    {
        private List<UserFriend> userFriendsData;

        private ScrollRect chatListScrollRect;
        //private Transform ChatListPage;
        //private ItemList chatItemList;
        private TableView tableView;

        private float beginRect;
        private GameObject targetObj;//当前长按时选择的Item
        float timer;  //计时器：判断点击还是长按
        private bool isOpen; //是否打开了弹窗

        public override void InitPage()
        {
            if (HasInitPage) return;
            chatListScrollRect = GameTools.GetByName(transform, "ChatListPage").GetComponent<ScrollRect>();

            //Content_ChatList = GameTools.GetByName(transform, "Content_ChatList").transform;
            //chatItemList = Content_ChatList.gameObject.AddComponent<ItemList>();
            tableView = gameObject.AddComponent<TableView>();

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ChatRelationList, S2C_Friend_FriendListCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_RemoveChatRelation, S2C_Friend_RemoveChatRelationCallBack);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_NewFriendList, S2C_Friend_NewFriendListCallBack); //监听新增加的聊天好友
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ApplyViewUserRealInfo, S2C_Friend_ApplyViewUserRealInfo);//收到认证请求，刷新好友列表
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, S2C_Friend_ApplyViewUserRealInfo);//收到认证请求，刷新好友列表

        }

        public override void OpenPage()
        {
            base.OpenPage();
            if (!HasInitPage)
            {
                GameData.ResetFriendListPage = true;
                NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());
            }
            base.InitPage();
        }

        public override void ClosePage()
        {
            base.ClosePage();
            GameData.isOpenFriendList = false;
        }

        void Update()
        {
            if (GameData.isOpenTipPanel || GameData.isOpenChatPage) return;
            if (Input.GetMouseButtonDown(0))
            {
                timer = 0;
                beginRect = chatListScrollRect.verticalNormalizedPosition;
                isOpen = false;
            }
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if (Mathf.Abs(chatListScrollRect.verticalNormalizedPosition - beginRect) > 0.001f) return;

                if (GetOverUI() == null) return;
                if (GetOverUI().name.StartsWith("FriendItem"))
                {
                    if (timer >= 0.8f)
                    {
                        targetObj = GetOverUI();
                        CommonPopWinPanel.Instance.ShowPopOne("确定删除TA吗？", ok: "删除", cancle: "留下TA", okAction: () =>
                        {
                            targetObj.GetComponent<UIBFriendItem>().SendToPb();
                        });
                        isOpen = true;
                    }
                }
            }
        }


        public GameObject GetOverUI()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            GraphicRaycaster gr = UIMgr.Instance.UIRoot.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);
            if (results.Count != 0)
            {
                return results[0].gameObject;
            }
            return null;
        }



        private void S2C_Friend_FriendListCallBack(object o)
        {
            userFriendsData = DataMgr.Instance.dataFriendList.userFriendList;
            RefreahFriendList(userFriendsData);
        }

        //删除聊天关系 回调
        void S2C_Friend_RemoveChatRelationCallBack(object o)
        {
            GameData.ResetFriendListPage = false;
            NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());
        }

        private void RefreahFriendList(List<UserFriend> m_Data)
        {
            if (m_Data != null && m_Data.Count > 0)
            {
                m_Data = SortListByTime(m_Data);
                Action<int, GameObject> onItemRender = (i, go) =>
                {
                    go.GetComponent<UIBFriendItem>().SetItemData(m_Data[i]);
                };
                if (tableView != null)
                {
                    if (!tableView.Init)
                    {
                        tableView.onItemCreated = (go) => { go.AddComponent<UIBFriendItem>(); };
                        tableView.onItemRender = onItemRender;
                        tableView.InitTable(m_Data.Count, Rechristen: false);
                    }
                    else
                    {
                        tableView.onItemRender = onItemRender;
                        tableView.ReLoadTable(m_Data.Count, GameData.ResetFriendListPage);
                    }
                }
            }
            else
            {
                if (tableView != null && tableView.Init)
                {
                    tableView.ReLoadTable(0);
                }
            }
        }

        /// <summary>
        /// 按消息的最后时间排序   如果是新加好友  默认为添加好友关系的时间
        /// </summary>
        /// <param name="UserFriendList"></param>
        /// <returns></returns>
        private List<UserFriend> SortListByTime(List<UserFriend> UserFriendList)
        {
            List<UserFriend> _UserFriendList = UserFriendList;
            _UserFriendList.Sort((a, b) =>
            {
                int result = 0;
                if (a.lastMessageSendTime == null || b.lastMessageSendTime == null) return 0;
                int _a = GameTools.TmGetTimeStamp(DateTime.Parse(a.lastMessageSendTime));
                int _b = (GameTools.TmGetTimeStamp(DateTime.Parse(b.lastMessageSendTime)));
                if (_a - _b >= 0)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                }
                return result;
            });
            return _UserFriendList;
        }

        //收到新消息——————待优化
        void S2C_Friend_SendMessageCallBack(object o)
        {
            if (DataMgr.Instance.dataSendMessage.toUserId==GameData.userId)
            {
                GameData.ResetFriendListPage = false;
                NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());
            }
        }

        //新增聊天关系回调
        void S2C_Friend_NewFriendListCallBack(object o)
        {
            UserFriend newUserFriend = DataMgr.Instance.newUserFriend;
            //判断是否已经是好友，防止服务器出错
            if (userFriendsData != null)
            {
                for (int i = 0; i < userFriendsData.Count; i++)
                {
                    if (newUserFriend.userId == userFriendsData[i].userId)
                    {
                        return;
                    }
                }
            }
            else { userFriendsData = new List<UserFriend>(); }
            userFriendsData.Add(newUserFriend);
            GameData.ResetFriendListPage = true;
            RefreahFriendList(userFriendsData);
        }

        //处理认证交换请求
        void S2C_Friend_ApplyViewUserRealInfo(object o)
        {
            if (!GameData.isOpenChatPage)
            {
                GameData.ResetFriendListPage = false;
                NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());//刷新好友列表
            }
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_RemoveChatRelation, S2C_Friend_RemoveChatRelationCallBack);

            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ChatRelationList, S2C_Friend_FriendListCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_NewFriendList, S2C_Friend_NewFriendListCallBack); //监听新增加的聊天好友
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ApplyViewUserRealInfo, S2C_Friend_ApplyViewUserRealInfo); //收到认证请求，刷新好友列表
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, S2C_Friend_ApplyViewUserRealInfo);//收到认证请求，刷新好友列表

        }
    }
}
