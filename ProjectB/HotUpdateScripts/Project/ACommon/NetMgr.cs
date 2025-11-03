using JEngine.Core;
using System;
using System.Text;
using BestHTTP.WebSocket;
using UnityEngine;
using LitJson;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.Game.GameA.Data;
using My.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// WebSocket消息码 （客户端 服务器）
    /// </summary>
    public class WebSocketCode
    {
        //Server -> Client
        public const string S2C_Test_Test = "S2C_Test_Test";

        //Client -> Server
        public const string C2S_Test_Test = "C2S_Test_Test";

        #region
        public const string C2S_Game_GetServerTime = "C2S_Game_GetServerTime"; //获取服务器时间
        public const string S2C_Game_GetServerTime = "S2C_Game_GetServerTime";
        #endregion

        #region Other
        public const string C2S_Other_SaveSession = "C2S_Other_SaveSession";
        #endregion

        #region UpdateData_
        public const string S2C_Update_UpdateGameData = "S2C_Update_UpdateGameData"; //实时更新游戏用户信息数据
        public const string S2C_Update_UpdateBagData = "S2C_Update_UpdateBagData";  //实时更新背包道具数据
        #endregion

        #region Main_
        public const string C2S_Game_FirstTimePlay = "C2S_Game_FirstTimePlay";//第一次登入游戏传给服务器
        public const string S2C_Game_FirstTimePlay = "S2C_Game_FirstTimePlay";

        public const string S2C_Main_HomeGetInfo = "S2C_Main_HomeGetInfo";//获取主场景信息
        public const string C2S_Main_HomeGetInfo = "C2S_Main_HomeGetInfo";//获取主场景信息

        public const string C2S_System_Broadcast = "C2S_System_Broadcast";//系统广播公告
        public const string S2C_System_Broadcast = "S2C_System_Broadcast";//系统广播公告
        #endregion

        #region Activity
        public const string C2S_Activity_PrizeList = "C2S_Activity_PrizeList";//获取活动排行榜
        public const string S2C_Activity_PrizeList = "S2C_Activity_PrizeList";

        public const string C2S_User_Get_Red_Packet = "C2S_User_Get_Red_Packet";//用户领取红包
        public const string S2C_User_Get_Red_Packet = "S2C_User_Get_Red_Packet";


        public const string C2S_Get_Newest_Red_List = "C2S_Get_Newest_Red_List";//获取最新的红包记录
        public const string S2C_Get_Newest_Red_List = "S2C_Get_Newest_Red_List";
        #endregion

        #region Love_
        public const string C2S_Love_TaskGetList = "C2S_Love_TaskGetList";//请求任务列表      eGetTaskListReq
        public const string S2C_Love_TaskGetList = "S2C_Love_TaskGetList";//请求任务列表      eGetTaskListRes

        public const string C2S_Love_TaskUpdateItem = "C2S_Love_TaskUpdateItem";//更新普通任务（进度 状态）
        public const string S2C_Love_TaskUpdateItem = "S2C_Love_TaskUpdateItem";

        public const string C2S_Love_TaskUpdateItemReward = "C2S_Love_TaskUpdateItemReward";//任务领奖
        public const string S2C_Love_TaskUpdateItemReward = "S2C_Love_TaskUpdateItemReward";

        public const string C2S_Love_TaskUpdateSignIn = "C2S_Love_TaskUpdateSignIn";//领取签到奖励
        public const string S2C_Love_TaskUpdateSignIn = "S2C_Love_TaskUpdateSignIn";

        public const string C2S_Love_GetChargeTime = "C2S_Love_GetChargeTime";
        public const string S2C_Love_GetChargeTime = "S2C_Love_GetChargeTime";//获取充能结束时间

        public const string C2S_Love_Recharge = "C2S_Love_Recharge"; //充能
        public const string S2C_Love_Recharge = "S2C_Love_Recharge";

        public const string C2S_Init_UserInfo = "C2S_Init_UserInfo";//重置数据
        public const string S2C_Init_UserInfo = "S2C_Init_UserInfo";


        //定义
        public const string C2S_Main_GetBagInfo = "C2S_Main_GetBagInfo";//获取背包数据
        public const string S2C_Main_GetBagInfo = "S2C_Main_GetBagInfo";

        #endregion

        #region Hope_
        public const string C2S_Love_GetDonateList = "C2S_Love_GetDonateList"; //捐献列表数据
        public const string S2C_Love_GetDonateList = "S2C_Love_GetDonateList";

        public const string C2S_Love_DonateDetailsInfo = "C2S_Love_DonateDetailsInfo"; //捐献项目详情
        public const string S2C_Love_DonateDetailsInfo = "S2C_Love_DonateDetailsInfo";

        public const string C2S_Love_Donate = "C2S_Love_Donate"; //捐献
        public const string S2C_Love_Donate = "S2C_Love_Donate";

        //public const string C2S_Project_NewProjectDonateInfo = "C2S_Project_NewProjectDonateInfo"; //获取 用户捐献后，公益项目的最新数据   参数  userId 用户id  projectId项目id
        public const string S2C_Project_NewProjectDonateInfo = "S2C_Project_NewProjectDonateInfo"; //主动推送
        #endregion

        #region Honor_
        public const string C2S_Honor_GetHonorRecordList = "C2S_Honor_GetHonorRecordList"; //获取荣誉展厅 捐献记录
        public const string S2C_Honor_GetHonorRecordList = "S2C_Honor_GetHonorRecordList";

        public const string C2S_Honor_GetHonorRank = "C2S_Honor_GetHonorRank"; //获取荣誉展厅 排名
        public const string S2C_Honor_GetHonorRank = "S2C_Honor_GetHonorRank";

        public const string C2S_Honor_GetHonormedalInfo = "C2S_Honor_GetHonormedalInfo"; //获取荣誉展厅 勋章
        public const string S2C_Honor_GetHonormedalInfo = "S2C_Honor_GetHonormedalInfo";
        #endregion

        #region Game
        public const string C2S_Game_GetGameDXGScore = "C2S_Game_GetGameDXGScore"; //获取合成大西瓜分数
        public const string S2C_Game_GetGameDXGScore = "S2C_Game_GetGameDXGScore";

        public const string C2S_Game_GetGameDXGRankList = "C2S_Game_GetGameDXGRankList"; //获取合成大西瓜排行榜
        public const string S2C_Game_GetGameDXGRankList = "S2C_Game_GetGameDXGRankList";

        public const string C2S_Game_SaveGameDXGScore = "C2S_Game_SaveGameDXGScore"; //上传合成大西瓜分数
        public const string S2C_Game_SaveGameDXGScore = "S2C_Game_SaveGameDXGScore";
        #endregion

        #region GameB
        public const string C2S_Game_Jump_Enter_Homepage = "C2S_Game_Jump_Enter_Homepage";//获取用户游戏数据
        public const string S2C_Game_Jump_Enter_Homepage = "S2C_Game_Jump_Enter_Homepage";

        public const string C2S_Game_Jump_Get_Role_Theme = "C2S_Game_Jump_Get_Role_Theme";//获取用户 角色 或 主题信息
        public const string S2C_Game_Jump_Get_Role_Theme = "S2C_Game_Jump_Get_Role_Theme";

        public const string C2S_Game_Jump_Unlock_Role_Theme = "C2S_Game_Jump_Unlock_Role_Theme";//解锁  角色  或 主题
        public const string S2C_Game_Jump_Unlock_Role_Theme = "S2C_Game_Jump_Unlock_Role_Theme";

        public const string C2S_Game_Jump_Check_Unlock_Condition = "C2S_Game_Jump_Check_Unlock_Condition";
        public const string S2C_Game_Jump_Check_Unlock_Condition = "S2C_Game_Jump_Check_Unlock_Condition";

        public const string C2S_Game_Jump_Get_Score_Rank = "C2S_Game_Jump_Get_Score_Rank";//获取排行榜信息（按最高分）
        public const string S2C_Game_Jump_Get_Score_Rank = "S2C_Game_Jump_Get_Score_Rank";

        public const string C2S_Game_Jump_Upload_Score = "C2S_Game_Jump_Upload_Score";//上传用户游戏得分
        public const string S2C_Game_Jump_Upload_Score = "S2C_Game_Jump_Upload_Score";
        #endregion

        #region Shop
        public const string C2S_Shop_GetShopInfoList = "C2S_Shop_GetShopInfoList";//获取兑换商城商品列表
        public const string S2C_Shop_GetShopInfoList = "S2C_Shop_GetShopInfoList";

        public const string C2S_Shop_GetShopRecordList = "C2S_Shop_GetShopRecordList";//获取兑换记录列表
        public const string S2C_Shop_GetShopRecordList = "S2C_Shop_GetShopRecordList";

        public const string C2S_Shop_exchange = "C2S_Shop_exchange";//兑换
        public const string S2C_Shop_exchange = "S2C_Shop_exchange";
        #endregion

        #region Lucky
        public const string C2S_Lucky_LuckyHouseDraw = "C2S_Lucky_LuckyHouseDraw"; //获取抽奖结果
        public const string S2C_Lucky_LuckyHouseDraw = "S2C_Lucky_LuckyHouseDraw";
        #endregion

        #region BagCard
        public const string C2S_BagCard_UserCard = "C2S_BagCard_UserCard"; //使用道具卡
        public const string S2C_BagCard_UserCard = "S2C_BagCard_UserCard";
        #endregion

        #region Friend
        public const string C2S_Friend_ViewPersonalInfo = "C2S_Friend_ViewPersonalInfo"; //查看个人信息
        public const string S2C_Friend_ViewPersonalInfo = "S2C_Friend_ViewPersonalInfo";

        public const string C2S_User_ViewRealInfo = "C2S_User_ViewRealInfo"; //查看用户认证信息
        public const string S2C_User_ViewRealInfo = "S2C_User_ViewRealInfo";

        public const string C2S_Friend_ViewFriendInfo = "C2S_Friend_ViewFriendInfo"; //查看别人信息
        public const string S2C_Friend_ViewFriendInfo = "S2C_Friend_ViewFriendInfo";

        public const string C2S_Attention_UserInfo = "C2S_Attention_UserInfo"; //查看关注用户个人资料
        public const string S2C_Attention_UserInfo = "S2C_Attention_UserInfo";

        public const string C2S_Friend_CompilePersonalInfo = "C2S_Friend_CompilePersonalInfo"; //编辑个人信息
        public const string S2C_Friend_CompilePersonalInfo = "S2C_Friend_CompilePersonalInfo";

        public const string C2S_Friend_FollowToUser = "C2S_Friend_FollowToUser";  //关注他人
        public const string S2C_Friend_FollowToUser = "S2C_Friend_FollowToUser";

        public const string C2S_Friend_UnFollowToUser = "C2S_Friend_UnFollowToUser";  //取消关注
        public const string S2C_Friend_UnFollowToUser = "S2C_Friend_UnFollowToUser";

        public const string C2S_Friend_AttentionList = "C2S_Friend_AttentionList"; //关注我的用户列表
        public const string S2C_Friend_AttentionList = "S2C_Friend_AttentionList";

        public const string C2S_Friend_GetBlackListUserInfoList = "C2S_Friend_GetBlackListUserInfoList"; //黑名单列表
        public const string S2C_Friend_GetBlackListUserInfoList = "S2C_Friend_GetBlackListUserInfoList";

        public const string C2S_Friend_ChatRelationList = "C2S_Friend_ChatRelationList"; //获取聊天关系列表
        public const string S2C_Friend_ChatRelationList = "S2C_Friend_ChatRelationList";

        public const string C2S_Friend_PullInOrPullOutBlackList = "C2S_Friend_PullInOrPullOutBlackList"; //拉黑
        public const string S2C_Friend_PullInOrPullOutBlackList = "S2C_Friend_PullInOrPullOutBlackList";

        public const string C2S_Friend_ApplyViewUserRealInfo = "C2S_Friend_ApplyViewUserRealInfo"; //申请查看用户认证信息
        public const string S2C_Friend_ApplyViewUserRealInfo = "S2C_Friend_ApplyViewUserRealInfo";

        public const string C2S_Friend_ProcessViewUserRealInfoApply = "C2S_Friend_ProcessViewUserRealInfoApply"; //处理接收到的认证信息查看申请
        public const string S2C_Friend_ProcessViewUserRealInfoApply = "S2C_Friend_ProcessViewUserRealInfoApply";

        public const string C2S_Friend_ConfirmUserRealInfoApply = "C2S_Friend_ConfirmUserRealInfoApply"; //确认认证信息申请处理结果
        public const string S2C_Friend_ConfirmUserRealInfoApply = "S2C_Friend_ConfirmUserRealInfoApply";

        public const string C2S_Friend_MatchUserPool = "C2S_Friend_MatchUserPool";//获取匹配用户池 参数userId
        public const string S2C_Friend_MatchUserPool = "S2C_Friend_MatchUserPool";

        public const string C2S_Friend_MatchUserForChat = "C2S_Friend_MatchUserForChat";//匹配单个用户  参数userId
        public const string S2C_Friend_MatchUserForChat = "S2C_Friend_MatchUserForChat";

        public const string C2S_Friend_AddChatRelation = "C2S_Friend_AddChatRelation";//添加聊天关系   参数userId  friendId
        public const string S2C_Friend_AddChatRelation = "S2C_Friend_AddChatRelation";//添加聊天关系

        public const string C2S_Friend_RemoveChatRelation = "C2S_Friend_RemoveChatRelation";//删除聊天关系   参数userId  friendId
        public const string S2C_Friend_RemoveChatRelation = "S2C_Friend_RemoveChatRelation";

        public const string C2S_Friend_ViewChatRecord = "C2S_Friend_ViewChatRecord"; //查看聊天记录  参数userId  friendId
        public const string S2C_Friend_ViewChatRecord = "S2C_Friend_ViewChatRecord";

        public const string C2S_Attention_OpenPrivateLetter = "C2S_Attention_OpenPrivateLetter";// 关注列表 打开私信聊天窗口会添加好友关系
        public const string S2C_Attention_OpenPrivateLetter = "S2C_Attention_OpenPrivateLetter";

        public const string C2S_Friend_SendMessage = "C2S_Friend_SendMessage"; //发送消息
        public const string S2C_Friend_SendMessage = "S2C_Friend_SendMessage";

        public const string S2C_Friend_NewFriendList = "S2C_Friend_NewFriendList";//监听新增加的聊天好友

        public const string C2S_Friend_UpdateChatMessageState = "C2S_Friend_UpdateChatMessageState"; //修改聊天消息状态       参数userId 用户自己的id  friendId 好友id
        public const string S2C_Friend_UpdateChatMessageState = "S2C_Friend_UpdateChatMessageState";

        public const string C2S_Friend_GetUnreadMessageNum = "C2S_Friend_GetUnreadMessageNum";//查看好友给自己发送的未读消息数量    参数userId 用户自己的id  friendId 好友id
        public const string S2C_Friend_GetUnreadMessageNum = "S2C_Friend_GetUnreadMessageNum";

        public const string C2S_Friend_UpdateRemark = "C2S_Friend_UpdateRemark"; // 修改好友备注
        public const string S2C_Friend_UpdateRemark = "S2C_Friend_UpdateRemark"; // 修改好友备注

        public const string C2S_Friend_AddRandomUserRelation = "C2S_Friend_AddRandomUserRelation"; // 添加匹配聊天关系
        public const string S2C_Friend_AddRandomUserRelation = "S2C_Friend_AddRandomUserRelation"; // 添加匹配聊天关系
        #endregion

        #region 用户行为统计
        public const string C2S_Project_UserBehaviorStatistics = "C2S_Project_UserBehaviorStatistics";//用户行为统计
        #endregion
    }

    //定义请求接口方法 回调数据供DataMgr使用
    public class NetMgr : Singleton<NetMgr>
    {
        string local_Address = "ws://10.24.11.205:8888/ws/server";

        string test_Address = "ws://game-star-test.juaiyouxuan.com/ws/server";

        string pre_Address = "ws://game-star-pre.juaiyouxuan.com/ws/server";//预发布环境地址
        //string pre_Address = "ws://192.168.100.216:32705/ws/server";//预发布环境地址

        string master_Address = "ws://game-star.juaiyouxuan.com/ws/server";//正式环境地址

        public WebSocket webSocket;//open send close

        private bool IsNetworkError = false;

        private bool SrartReConnect = false;

        private bool LockReConnect = false;

        //本机网络检测
        public bool NoNetwork()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)//网络无法访问
            {
                webSocket = null;
                debug.Log_Red("网络无法访问");
                if (SrartReConnect)
                {
                    SrartReConnect = false;
                    UIMgr.Instance.Open(UIPath.CommonNetErrorPanel, "本机连接断开 \n 请检查您的网络后尝试重连！", UILayer.Layer5);
                }
                else
                {
                    SrartReConnect = true;
                    Init();
                }
                return true;
            }
            else
            {
                if (IsNetworkError)//网络恢复但未重连
                    ReConnect();
            }
            return IsNetworkError;
        }

        public void Init()
        {
            if (webSocket == null)
            {
                string serverUrl = "";
                switch (GameData.CurrentServer)
                {
                    case ServerType.LocalServer:
                        serverUrl = local_Address;
                        break;
                    case ServerType.TestingServer:
                        serverUrl = test_Address;
                        break;
                    case ServerType.PrePublishingServer:
                        serverUrl = pre_Address;
                        break;
                    case ServerType.OfficialServer:
                        serverUrl = master_Address;
                        break;
                    default:
                        break;
                }
                webSocket = new WebSocket(new Uri(serverUrl));
                debug.Log_Blue("webSocket Uri--" + serverUrl);
                // Subscribe to the WS events
                webSocket.OnOpen += OnOpen;
                webSocket.OnMessage += OnMessageRecv;
                webSocket.OnBinary += OnBinaryRecv;
                webSocket.OnClosed += OnClosed;
                webSocket.OnError += OnError;

                // Start connecting to the server
                webSocket.Open();
            }
        }

        #region WebSocket 回调
        void OnOpen(WebSocket ws)
        {
            debug.Log_Blue("-->WebSocket Open");
            IsNetworkError = false;
            SrartReConnect = false;
            LockReConnect = false;
            UIMgr.Instance.Close(UIPath.CommonNetErrorPanel);

            //NetMgr.Instance.C2S_Main_HomeGetInfo(GameData.userId.ToString());
            //断线重连判断：当首页已经打开，说明不是第一次进入游戏，此时为 断线重连成功的情况
            if (UIMgr.Instance.GetPanelIsExit(UIPath.UIBMainHomePanel))
            {
                debug.Log_Blue("断线重连成功");
                NetMgr.Instance.C2S_Other_SaveSession(GameData.userId.ToString());
                if (DataMgr.Instance.dataBTaskInfoReq.taskId != 0)//有任务完成未通知
                {
                    C2S_Love_TaskUpdateItem();
                }
            }
            else
            {
                UIMgr.Instance.Open(UIPath.UIBMainHomePanel, null, UILayer.Layer3);
            }
        }



        void OnMessageRecv(WebSocket ws, string message)
        {
            //Debug.LogFormat("OnMessageRecv: msg={0}", message);

            OnMessageRecvFact(message);
        }

        void OnBinaryRecv(WebSocket ws, byte[] data)
        {
            Debug.LogFormat("OnBinaryRecv: len={0}", data.Length);//Debug.Log(System.Text.Encoding.ASCII.GetString(data));
        }

        void OnClosed(WebSocket ws, UInt16 code, string message)
        {
            //webSocket.Close();
            webSocket = null;
            LockReConnect = false;
            debug.LogFormat("OnClosed: code={0}, msg={1}", code, message);
            if (SrartReConnect)
            {
                SrartReConnect = false;
                UIMgr.Instance.Open(UIPath.CommonNetErrorPanel, "本机连接断开 \n 请检查您的网络后尝试重连！", UILayer.Layer5);
            }
            else
            {
                ReConnect();
            }
        }

        void OnError(WebSocket ws, string ex)
        {
            //webSocket.Close();
            IsNetworkError = true;
            webSocket = null;
            LockReConnect = false;
            debug.LogFormat("OnError: ex={0}", ex);
            if (SrartReConnect)
            {
                SrartReConnect = false;
                //UIMgr.Instance.Open(UIPath.CommonNetErrorPanel, "服务器连接异常 \n 若您网络通畅，请联系管理员！", UILayer.Layer5);
                UIMgr.Instance.Open(UIPath.CommonNetErrorPanel, "本机连接断开 \n 请检查您的网络后尝试重连！", UILayer.Layer5);
            }
            else
            {
                ReConnect();
            }
        }

        public void ReConnect()
        {
            if (!LockReConnect)
            {
                LockReConnect = true;
                webSocket = null;
                SrartReConnect = true;
                debug.Log_yellow("------>尝试重连");
                Init();
            }
            else
            {
                debug.Log_yellow("------>重连锁定 勿重复");
            }
        }
        #endregion


        /// <summary>
        /// 处理服务器发来消息
        /// </summary>
        /// <param name="message"></param>
        void OnMessageRecvFact(string message)
        {
            //处理业务
            if (message.Contains("#"))
            {
                string[] strArray = message.Split('#');
                string data = strArray[1];

                switch (strArray[0])
                {
                    //Test
                    case WebSocketCode.S2C_Test_Test:
                        S2C_Test_Test(data);
                        break;
                    #region Main
                    case WebSocketCode.S2C_Game_FirstTimePlay:
                        S2C_Game_FirstTimePlay(data);
                        break;
                    case WebSocketCode.S2C_Main_HomeGetInfo:
                        S2C_Main_HomeGetInfo(data);
                        break;
                    case WebSocketCode.S2C_System_Broadcast:
                        S2C_System_Broadcast(data);
                        break;
                    #endregion
                    #region Love
                    case WebSocketCode.S2C_Love_TaskGetList:
                        S2C_Love_TaskGetList(data);
                        break;
                    case WebSocketCode.S2C_Love_TaskUpdateSignIn:
                        S2C_Love_TaskUpdateSignIn(data);
                        break;
                    case WebSocketCode.S2C_Love_TaskUpdateItemReward:
                        S2C_Love_TaskUpdateItemReward(data);
                        break;
                    case WebSocketCode.S2C_Love_TaskUpdateItem:
                        S2C_Love_TaskUpdateItem(data);
                        break;
                    case WebSocketCode.S2C_Init_UserInfo:
                        S2C_Init_UserInfo(data);
                        break;
                    case WebSocketCode.S2C_Love_GetChargeTime:
                        S2C_Love_GetChargeTime(data);
                        break;
                    case WebSocketCode.S2C_Love_Recharge:
                        S2C_Love_Recharge(data);
                        break;
                    case WebSocketCode.S2C_Game_GetServerTime:
                        S2C_Game_GetServerTime(data);
                        break;
                    case WebSocketCode.S2C_Main_GetBagInfo:
                        S2C_Main_GetBagInfo(data);
                        break;
                    case WebSocketCode.S2C_Love_GetDonateList:
                        S2C_Love_GetDonateList(data);
                        break;
                    case WebSocketCode.S2C_Love_DonateDetailsInfo:
                        S2C_Love_DonateDetailsInfo(data);
                        break;
                    case WebSocketCode.S2C_Project_NewProjectDonateInfo:
                        S2C_Project_NewProjectDonateInfo(data);
                        break;
                    #endregion
                    #region Honor_
                    case WebSocketCode.S2C_Love_Donate:
                        S2C_Love_Donate(data);
                        break;
                    case WebSocketCode.S2C_Honor_GetHonorRecordList:
                        S2C_Honor_GetHonorRecordList(data);
                        break;
                    case WebSocketCode.S2C_Honor_GetHonorRank:
                        S2C_Honor_GetHonorRank(data);
                        break;
                    case WebSocketCode.S2C_Honor_GetHonormedalInfo:
                        S2C_Honor_GetHonormedalInfo(data);
                        break;
                    #endregion
                    case WebSocketCode.S2C_Update_UpdateGameData:
                        S2C_Update_UpdateGameData(data);
                        break;
                    case WebSocketCode.S2C_Update_UpdateBagData:
                        S2C_Update_UpdateBagData(data);
                        break;
                    case WebSocketCode.S2C_Activity_PrizeList:
                        S2C_Activity_PrizeList(data);
                        break;
                    case WebSocketCode.S2C_User_Get_Red_Packet:
                        S2C_User_Get_Red_Packet(data);
                        break;
                    case WebSocketCode.S2C_Get_Newest_Red_List:
                        S2C_Get_Newest_Red_List(data);
                        break;
                    #region Game_DXG
                    case WebSocketCode.S2C_Game_GetGameDXGScore:
                        S2C_Game_GetGameDXGScore(data);
                        break;
                    case WebSocketCode.S2C_Game_GetGameDXGRankList:
                        S2C_Game_GetGameDXGRankList(data);
                        break;
                    case WebSocketCode.S2C_Game_SaveGameDXGScore:
                        S2C_Game_SaveGameDXGScore(data);
                        break;
                    case WebSocketCode.S2C_Shop_GetShopInfoList:
                        S2C_Shop_GetShopInfoList(data);
                        break;
                    case WebSocketCode.S2C_Shop_GetShopRecordList:
                        S2C_Shop_GetShopRecordList(data);
                        break;
                    case WebSocketCode.S2C_Shop_exchange:
                        S2C_Shop_exchange(data);
                        break;
                    #endregion

                    #region GameJump
                    case WebSocketCode.S2C_Game_Jump_Enter_Homepage:
                        S2C_Game_Jump_Enter_Homepage(data);
                        break;
                    case WebSocketCode.S2C_Game_Jump_Get_Role_Theme:
                        S2C_Game_Jump_Get_Role_Theme(data);
                        break;
                    case WebSocketCode.S2C_Game_Jump_Unlock_Role_Theme:
                        S2C_Game_Jump_Unlock_Role_Theme(data);
                        break;
                    case WebSocketCode.S2C_Game_Jump_Check_Unlock_Condition:
                        S2C_Game_Jump_Check_Unlock_Condition(data);
                        break;
                    case WebSocketCode.S2C_Game_Jump_Get_Score_Rank:
                        S2C_Game_Jump_Get_Score_Rank(data);
                        break;
                    case WebSocketCode.S2C_Game_Jump_Upload_Score:
                        S2C_Game_Jump_Upload_Score(data);
                        break;
                    #endregion
                    #region Luck
                    case WebSocketCode.S2C_Lucky_LuckyHouseDraw:
                        S2C_Lucky_LuckyHouseDraw(data);
                        break;
                    #endregion
                    #region BagCard
                    case WebSocketCode.S2C_BagCard_UserCard:
                        S2C_BagCard_UserCard(data);
                        break;
                    #endregion
                    #region Friend
                    case WebSocketCode.S2C_Friend_ViewPersonalInfo:
                        S2C_Friend_ViewPersonalInfo(data);
                        break;
                    case WebSocketCode.S2C_User_ViewRealInfo:
                        S2C_User_ViewRealInfo(data);
                        break;
                    case WebSocketCode.S2C_Friend_ViewFriendInfo:
                        S2C_Friend_ViewFriendInfo(data);
                        break;
                    case WebSocketCode.S2C_Attention_UserInfo:
                        S2C_Attention_UserInfo(data);
                        break;
                    case WebSocketCode.S2C_Friend_FollowToUser:
                        S2C_Friend_FollowToUser(data);
                        break;
                    case WebSocketCode.S2C_Friend_UnFollowToUser:
                        S2C_Friend_UnFollowToUser(data);
                        break;
                    case WebSocketCode.S2C_Friend_AttentionList:
                        S2C_Friend_AttentionList(data);
                        break;
                    case WebSocketCode.S2C_Friend_GetBlackListUserInfoList:
                        S2C_Friend_BlackListInfo(data);
                        break;
                    case WebSocketCode.S2C_Friend_ChatRelationList:
                        S2C_Friend_ChatRelationList(data);
                        break;
                    case WebSocketCode.S2C_Friend_PullInOrPullOutBlackList:
                        S2C_Friend_PullInOrPullOutBlackList(data);
                        break;
                    case WebSocketCode.S2C_Friend_ApplyViewUserRealInfo:
                        S2C_Friend_ApplyViewUserRealInfo(data);
                        break;
                    case WebSocketCode.S2C_Friend_ProcessViewUserRealInfoApply:
                        S2C_Friend_ProcessViewUserRealInfoApply(data);
                        break;
                    case WebSocketCode.S2C_Friend_ConfirmUserRealInfoApply:
                        S2C_Friend_ConfirmUserRealInfoApply(data);
                        break;
                    case WebSocketCode.S2C_Friend_CompilePersonalInfo:
                        S2C_Friend_CompilePersonalInfo(data);
                        break;
                    case WebSocketCode.S2C_Friend_MatchUserPool:
                        S2C_Friend_MatchUserPool(data);
                        break;
                    case WebSocketCode.S2C_Friend_MatchUserForChat:
                        S2C_Friend_MatchUserForChat(data);
                        break;
                    case WebSocketCode.S2C_Friend_AddChatRelation:
                        S2C_Friend_AddChatRelation(data);
                        break;
                    case WebSocketCode.S2C_Friend_RemoveChatRelation:
                        S2C_Friend_RemoveChatRelation(data);
                        break;
                    case WebSocketCode.S2C_Friend_ViewChatRecord:
                        S2C_Friend_ViewChatRecord(data);
                        break;
                    case WebSocketCode.S2C_Friend_SendMessage:
                        S2C_Friend_SendMessage(data);
                        break;
                    case WebSocketCode.S2C_Attention_OpenPrivateLetter:
                        S2C_Attention_OpenPrivateLetter(data);
                        break;
                    case WebSocketCode.S2C_Friend_NewFriendList:
                        S2C_Friend_NewFriendList(data);
                        break;
                    case WebSocketCode.S2C_Friend_UpdateChatMessageState:
                        S2C_Friend_UpdateChatMessageState(data);
                        break;
                    case WebSocketCode.S2C_Friend_GetUnreadMessageNum:
                        S2C_Friend_GetUnreadMessageNum(data);
                        break;
                    case WebSocketCode.S2C_Friend_UpdateRemark:
                        S2C_Friend_UpdateRemark(data);
                        break;
                    case WebSocketCode.S2C_Friend_AddRandomUserRelation:
                        S2C_Friend_AddRandomUserRelation(data);
                        break;
                        #endregion
                }
            }
        }

        #region 服务器-》客户端
        private void S2C_Test_Test(string sdata)//JUI数据绑定后 数据改变UI自动刷新
        {
            DataTest data = JsonMapper.ToObject<DataTest>(sdata);

            /* 注意： JUI绑定的数据在此逐个赋值 */
            DataMgr.Instance.dataTest.Id = data.Id;
            DataMgr.Instance.dataTest.Name = data.Name;
            DataMgr.Instance.dataTest.Age = data.Age;
        }

        #region Main_
        /// <summary>
        /// 第一次登录
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_FirstTimePlay(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_FirstTimePlay + ": " + sdata);
            if (sdata != "null")
            {
                DataBFirstToUnity data = JsonMapper.ToObject<DataBFirstToUnity>(sdata);
                DataMgr.Instance.dataBFirstToUnity = data;
            }
            else
            {
                DataMgr.Instance.dataBFirstToUnity = new DataBFirstToUnity() { userId = 67 };//75 46 67
            }

            MsgCenter.Call(null, MsgCode.S2C_Game_FirstTimePlay, null);
            NetMgr.Instance.C2S_System_Broadcast();
        }

        /// <summary>
        /// 获取主页信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Main_HomeGetInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Main_HomeGetInfo + ": " + sdata);
            DataBMainHome data = JsonMapper.ToObject<DataBMainHome>(sdata);
            DataMgr.Instance.dataBMainHome = data;
            MsgCenter.Call(null, MsgCode.S2C_Main_HomeGetInfo, null);
        }

        /// <summary>
        /// 系统广播公告
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_System_Broadcast(string sdata)
        {
            Log.Print(MsgCode.S2C_Main_System_Broadcast + ": " + sdata);
            DataBroadcast data = JsonMapper.ToObject<DataBroadcast>(sdata);
            DataMgr.Instance.dataBroadcast = data;
            MsgCenter.Call(null, MsgCode.S2C_Main_System_Broadcast, null);
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_GetServerTime(string sdata)
        {
            DataBServerTime data = JsonMapper.ToObject<DataBServerTime>(sdata);
            Log.Print(MsgCode.S2C_Game_GetServerTime + ": " + data.serverTime);
            DataMgr.Instance.dataServerTime = data;
            MsgCenter.Call(null, MsgCode.S2C_Game_GetServerTime, data.serverTime);//服务器时间传回
            //根据服务器时间 广播 是否切换夜间模式
            DateTime dt = Convert.ToDateTime(data.serverTime);
            //早上包含6点 晚上包含18点
            if (dt.Hour <= 6 && dt.Hour >= 0 || dt.Hour <= 24 && dt.Hour >= 18)
            {
                //debug.Log("晚上");
                MsgCenter.Call(null, MsgCode.DayNightUpt, "night");
            }
            else if (dt.Hour > 6 && dt.Hour < 18)
            {
                //debug.Log("白天");
                MsgCenter.Call(null, MsgCode.DayNightUpt, "day");
            }
        }

        /// <summary>
        /// 获取背包数据
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Main_GetBagInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Main_GetBagInfo + ": " + sdata);
            DataBag data = JsonMapper.ToObject<DataBag>(sdata);
            DataMgr.Instance.dataBag = data;
            MsgCenter.Call(null, MsgCode.S2C_Main_GetBagInfo, null);
        }

        #endregion

        #region UpdateData_
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Update_UpdateGameData(string sdata)
        {
            Log.Print(MsgCode.S2C_Update_UpdateGameData + ": " + sdata);
            DataBUpdataData data = JsonMapper.ToObject<DataBUpdataData>(sdata);
            DataMgr.Instance.dataBUpdataData = data;
            MsgCenter.Call(null, MsgCode.S2C_Update_UpdateGameData, null);
        }
        /// <summary>
        /// 推送背包数据
        /// </summary>
        /// <param name="sdata"></param>
        public void S2C_Update_UpdateBagData(string sdata)
        {
            Log.Print(MsgCode.S2C_Update_UpdateBagData + ": " + sdata);
            DataBag data = JsonMapper.ToObject<DataBag>(sdata);
            DataMgr.Instance.dataBUpdateBagData = data;
            MsgCenter.Call(null, MsgCode.S2C_Update_UpdateBagData, null);
        }
        #endregion

        #region Love_
        /// <summary>
        /// 获取任务（列表）
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_TaskGetList(string sdata)
        {
            if (string.IsNullOrEmpty(sdata) || sdata.Equals("null")) return;
            Log.Print(MsgCode.S2C_Love_TaskGetList + ": " + sdata);
            DataBLoveTask data = JsonMapper.ToObject<DataBLoveTask>(sdata);
            DataMgr.Instance.dataBLoveTask = data;
            MsgCenter.Call(null, MsgCode.S2C_Love_TaskGetList, null);
        }

        /// <summary>
        /// 更新签到
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_TaskUpdateSignIn(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_TaskUpdateSignIn + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Love_TaskUpdateSignIn, null);
        }
        /// <summary>
        /// 领取任务奖励
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_TaskUpdateItemReward(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_TaskUpdateItemReward + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Love_TaskUpdateItemReward, null);
        }
        /// <summary>
        /// 更新任务进度状态
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_TaskUpdateItem(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_TaskUpdateItem + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            GameData.taskInfo = null;
            DataMgr.Instance.dataBTaskInfoReq = new DataBTaskInfoReq();//断网导致任务未完成以此判断
            MsgCenter.Call(null, MsgCode.S2C_Love_TaskUpdateItem, null);
            C2S_Love_TaskGetList(GameData.userId.ToString());//刷新任务列表
        }

        private void S2C_Init_UserInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Init_UserInfo + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Init_UserInfo, null);
        }

        /// <summary>
        /// 首页数据中充能时间
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_GetChargeTime(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_GetChargeTime + ": " + sdata);
            DataBChargeTime data = JsonMapper.ToObject<DataBChargeTime>(sdata);
            DataMgr.Instance.dataBChargeTime = data;
            MsgCenter.Call(null, MsgCode.S2C_Love_GetChargeTime, null);
        }

        /// <summary>
        /// 充能
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_Recharge(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_Recharge + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Love_Recharge, null);
        }

        #endregion

        #region Hope_
        /// <summary>
        /// 获取捐献列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_GetDonateList(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_GetDonateList + ": " + sdata);
            DataBBenefitProjectListRes data = JsonMapper.ToObject<DataBBenefitProjectListRes>(sdata);
            DataMgr.Instance.dataBBenefitProjectListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Love_GetDonateList, null);
        }

        /// <summary>
        /// 获取捐献项目详情
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_DonateDetailsInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_DonateDetailsInfo + ": " + sdata);
            DataBProjectInfoRes data = JsonMapper.ToObject<DataBProjectInfoRes>(sdata);
            DataMgr.Instance.dataBProjectInfoRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Love_DonateDetailsInfo, null);
        }

        /// <summary>
        /// 捐献
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Love_Donate(string sdata)
        {
            Log.Print(MsgCode.S2C_Love_Donate + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Love_Donate, null);
        }

        /// <summary>
        /// 捐献后反馈最新数据
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Project_NewProjectDonateInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Project_NewProjectDonateInfo + ": " + sdata);
            NewProjectDonateInfo data = JsonMapper.ToObject<NewProjectDonateInfo>(sdata);
            DataMgr.Instance.newProjectDonateInfo = data;
            MsgCenter.Call(null, MsgCode.S2C_Project_NewProjectDonateInfo, null);
        }
        #endregion

        #region Honor_
        /// <summary>
        /// 获取荣誉大厅 捐献记录
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Honor_GetHonorRecordList(string sdata)
        {
            Log.Print(MsgCode.S2C_Honor_GetHonorRecordList + ": " + sdata);
            DataBDonateRecordListRes data = JsonMapper.ToObject<DataBDonateRecordListRes>(sdata);
            DataMgr.Instance.dataBDonateRecordListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Honor_GetHonorRecordList, null);
        }

        /// <summary>
        /// 获取荣誉大厅 排名
        /// </summary>
        private void S2C_Honor_GetHonorRank(string sdata)
        {
            Log.Print(MsgCode.S2C_Honor_GetHonorRank + ": " + sdata);
            DataBDonateRankInfoRes data = JsonMapper.ToObject<DataBDonateRankInfoRes>(sdata);
            DataMgr.Instance.dataBDonateRankInfoRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Honor_GetHonorRank, null);
        }

        /// <summary>
        /// 获取荣誉勋章
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Honor_GetHonormedalInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Honor_GetHonormedalInfo + ": " + sdata);
            DataBHonorMadelListRes data = JsonMapper.ToObject<DataBHonorMadelListRes>(sdata);
            DataMgr.Instance.dataBHonorMadelListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Honor_GetHonormedalInfo, null);
        }
        #endregion

        #region Game_DXG

        /// <summary>
        /// 获取合成大西瓜分数
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_GetGameDXGScore(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_GetGameDXGScore + ": " + sdata);
            DataBUserGameScoreRes data = JsonMapper.ToObject<DataBUserGameScoreRes>(sdata);
            DataMgr.Instance.dataBUserGameScoreRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Game_GetGameDXGScore, null);
        }

        /// <summary>
        /// 获取合成大西瓜排行
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_GetGameDXGRankList(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_GetGameDXGRankList + ": " + sdata);
            DataBScoreRankRes data = JsonMapper.ToObject<DataBScoreRankRes>(sdata);
            DataMgr.Instance.dataBScoreRankRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Game_GetGameDXGRankList, null);
        }

        /// <summary>
        /// 上传合成大西瓜分数
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_SaveGameDXGScore(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_SaveGameDXGScore + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_SaveGameDXGScore, null);
        }

        /// <summary>
        /// 跳跳乐游戏信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_Jump_Enter_Homepage(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Enter_Homepage + ": " + sdata);
            DataJumpGame data = JsonMapper.ToObject<DataJumpGame>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_Jump_Enter_Homepage, data);
        }

        /// <summary>
        /// 用户解锁主题和角色信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_Jump_Get_Role_Theme(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Get_Role_Theme + ": " + sdata);
            DataJumpGameUnlocked dataJumpGameUnlocked = JsonMapper.ToObject<DataJumpGameUnlocked>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_Jump_Get_Role_Theme, dataJumpGameUnlocked);
        }

        /// <summary>
        /// 点击新解锁角色或主题
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_Jump_Unlock_Role_Theme(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Unlock_Role_Theme + ": " + sdata);
            DataUnLockProgress dataJumpGameUnlocked = JsonMapper.ToObject<DataUnLockProgress>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_Jump_Unlock_Role_Theme, dataJumpGameUnlocked);
        }

        private void S2C_Game_Jump_Check_Unlock_Condition(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Check_Unlock_Condition + ": " + sdata);
            DataUnLockProgress data = JsonMapper.ToObject<DataUnLockProgress>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_Jump_Check_Unlock_Condition, data);
        }
        /// <summary>
        /// 获取跳跳乐排行榜
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_Jump_Get_Score_Rank(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Get_Score_Rank + ": " + sdata);
            DataMgr.Instance.dataJumpScoreRank = JsonMapper.ToObject<DataScoreRank>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Game_Jump_Get_Score_Rank, null);
        }

        /// <summary>
        /// 上传游戏数据
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Game_Jump_Upload_Score(string sdata)
        {
            Log.Print(MsgCode.S2C_Game_Jump_Upload_Score + ": " + sdata);
        }

        #endregion

        #region Activity
        /// <summary>
        /// 获取活动排行榜单
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Activity_PrizeList(string sdata)
        {
            Log.Print(MsgCode.S2C_Activity_PrizeList + ": " + sdata);
            DataBActivity data = JsonMapper.ToObject<DataBActivity>(sdata);
            DataMgr.Instance.DataBActivity = data;
            MsgCenter.Call(null, MsgCode.S2C_Activity_PrizeList, null);
        }

        //获取红包
        private void S2C_User_Get_Red_Packet(string sdata)
        {
            Log.Print(MsgCode.S2C_User_Get_Red_Packet + ": " + sdata);
            DataRedPackage data = JsonMapper.ToObject<DataRedPackage>(sdata);
            DataMgr.Instance.DataRedPackage = data;
            //MsgCenter.Call(null, MsgCode.S2C_User_Get_Red_Packet, null);
            UIMgr.Instance.GetRedPackageCallBack();
        }

        //获取红包记录
        private void S2C_Get_Newest_Red_List(string sdata)
        {
            Log.Print(MsgCode.S2C_Get_Newest_Red_List + ": " + sdata);
            DataUserRedRecordList data = JsonMapper.ToObject<DataUserRedRecordList>(sdata);
            DataMgr.Instance.DataUserRedRecordList = data;
            MsgCenter.Call(null, MsgCode.S2C_Get_Newest_Red_List, sdata);
        }

        #endregion
        #region shop

        /// <summary>
        /// 获取兑换商城商品信息列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Shop_GetShopInfoList(string sdata)
        {
            Log.Print(MsgCode.S2C_Shop_GetShopInfoList + ": " + sdata);
            DataBShopListRes data = JsonMapper.ToObject<DataBShopListRes>(sdata);
            DataMgr.Instance.dataBShopListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Shop_GetShopInfoList, null);
        }

        /// <summary>
        /// 获取兑换记录列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Shop_GetShopRecordList(string sdata)
        {
            Log.Print(MsgCode.S2C_Shop_GetShopRecordList + ": " + sdata);
            DataBShopRecordListRes data = JsonMapper.ToObject<DataBShopRecordListRes>(sdata);
            DataMgr.Instance.dataBShopRecordListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Shop_GetShopRecordList, null);
        }

        /// <summary>
        /// 兑换
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Shop_exchange(string sdata)
        {
            Log.Print(MsgCode.S2C_Shop_exchange + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Shop_exchange, data.msg);
        }
        #endregion

        #region Lucky
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Lucky_LuckyHouseDraw(string sdata)
        {
            Log.Print(MsgCode.S2C_Lucky_LuckyHouseDraw + ": " + sdata);
            DataBLuck data = JsonMapper.ToObject<DataBLuck>(sdata);
            DataMgr.Instance.dataBLuck = data;
            MsgCenter.Call(null, MsgCode.S2C_Lucky_LuckyHouseDraw, null);
        }
        #endregion

        #region BagCard
        /// <summary>
        /// 使用道具卡
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_BagCard_UserCard(string sdata)
        {
            Log.Print(MsgCode.S2C_BagCard_UserCard + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_BagCard_UserCard, null);
        }
        #endregion

        #region Friend
        /// <summary>
        /// 查看我的信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ViewPersonalInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ViewPersonalInfo + ": " + sdata);
            DataBViewPersonalInfoRes data = JsonMapper.ToObject<DataBViewPersonalInfoRes>(sdata);
            DataMgr.Instance.dataBViewPersonalInfoRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_ViewPersonalInfo, null);
        }

        /// <summary>
        /// 查看我的认证信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_User_ViewRealInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_User_ViewRealInfo + ": " + sdata);
            if (sdata == null) return;
            DataToViewRealInfo data = JsonMapper.ToObject<DataToViewRealInfo>(sdata);
            if (data != null && data.realInfo != null)
            {
                MsgCenter.Call(null, MsgCode.S2C_User_ViewRealInfo, data.realInfo);
            }

        }

        /// <summary>
        /// 查看别人信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ViewFriendInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ViewFriendInfo + ": " + sdata);
            DataBViewPersonalInfoRes data = JsonMapper.ToObject<DataBViewPersonalInfoRes>(sdata);
            DataMgr.Instance.dataBViewFriendInfoRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_ViewFriendInfo, null);
        }

        /// <summary>
        /// 查看关注用户个人资料
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Attention_UserInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Attention_UserInfo + ": " + sdata);
            DataBViewAttentionInfoRes data = JsonMapper.ToObject<DataBViewAttentionInfoRes>(sdata);
            DataMgr.Instance.dataBViewAttentionInfoRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Attention_UserInfo, null);
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_FollowToUser(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_FollowToUser + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_FollowToUser, null);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_UnFollowToUser(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_UnFollowToUser + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_UnFollowToUser, null);
        }

        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_AttentionList(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_AttentionList + ": " + sdata);
            AttentionListRes data = JsonMapper.ToObject<AttentionListRes>(sdata);
            DataMgr.Instance.attentionListRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_AttentionList, null);
        }
        /// <summary>
        /// 黑名单列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_BlackListInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_BlackListUserInfoList + ": " + sdata);
            DataUserBlackList data = JsonMapper.ToObject<DataUserBlackList>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_BlackListUserInfoList, data);
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ChatRelationList(string sdata)
        {
            debug.Log_yellow(WebSocketCode.S2C_Friend_ChatRelationList + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff"));

            Log.Print(MsgCode.S2C_Friend_ChatRelationList + ": " + sdata);
            DataFriendList data = JsonMapper.ToObject<DataFriendList>(sdata);
            DataMgr.Instance.dataFriendList = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_ChatRelationList, null);
        }

        /// <summary>
        /// 编辑资料
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_CompilePersonalInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_CompilePersonalInfo + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_CompilePersonalInfo, null);
        }

        /// <summary>
        /// 好友拉黑
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_PullInOrPullOutBlackList(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_PullInOrPullOutBlackList + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            //MsgCenter.Call(null, MsgCode.S2C_Friend_PullInOrPullOutBlackList, null);
            Instance.C2S_GetBlackList();//刷新黑名单列表
            GameData.ResetFriendListPage = false;
            Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());//刷新好友列表
            Instance.C2S_Friend_AttentionList(GameData.userId.ToString());//刷新关注列
        }

        /// <summary>
        /// 申请查看用户认证信息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ApplyViewUserRealInfo(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ApplyViewUserRealInfo + ": " + sdata);
            ExchangeInfoMessageData data = JsonMapper.ToObject<ExchangeInfoMessageData>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_ApplyViewUserRealInfo, data);
        }

        /// <summary>
        /// 处理接收到的认证信息查看申请
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ProcessViewUserRealInfoApply(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ProcessViewUserRealInfoApply + ": " + sdata);
            ExchangeInfoMessageData data = JsonMapper.ToObject<ExchangeInfoMessageData>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, data);
        }

        /// <summary>
        /// 确认认证信息申请处理结果
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ConfirmUserRealInfoApply(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ConfirmUserRealInfoApply + ": " + sdata);
            ExchangeInfoMessageData data = JsonMapper.ToObject<ExchangeInfoMessageData>(sdata);
            //MsgCenter.Call(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, data);
        }

        /// <summary>
        /// 获取匹配用户池
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_MatchUserPool(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_MatchUserPool + ": " + sdata);
            DataRandomUserFriendDataList data = JsonMapper.ToObject<DataRandomUserFriendDataList>(sdata);
            DataMgr.Instance.dataRandomUserFriendDataList = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_MatchUserPool, null);
        }

        /// <summary>
        /// 匹配个人
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_MatchUserForChat(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_MatchUserForChat + ": " + sdata);
            DataRandomUser data = JsonMapper.ToObject<DataRandomUser>(sdata);
            DataMgr.Instance.dataRandomUser = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_MatchUserForChat, null);
        }

        /// <summary>
        /// 添加聊天关系
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_AddChatRelation(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_AddChatRelation + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_AddChatRelation, null);
        }

        /// <summary>
        /// 添加匹配聊天关系
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_AddRandomUserRelation(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_AddRandomUserRelation + ": " + sdata);
            AddChatRelationRes data = JsonMapper.ToObject<AddChatRelationRes>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_AddRandomUserRelation, null);
        }

        /// <summary>
        /// 删除聊天关系
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_RemoveChatRelation(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_RemoveChatRelation + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Friend_RemoveChatRelation, null);
        }

        /// <summary>
        /// 获取聊天记录 
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_ViewChatRecord(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_ViewChatRecord + ": " + sdata);
            DataFriendViewChatRecord data = JsonMapper.ToObject<DataFriendViewChatRecord>(sdata);
            DataMgr.Instance.dataFriendViewChatRecord = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_ViewChatRecord, null);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_SendMessage(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_SendMessage + ": " + sdata);
            ChatMessages data = JsonMapper.ToObject<ChatMessages>(sdata);
            DataMgr.Instance.dataSendMessage = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_SendMessage, null);
        }

        /// <summary>
        /// 聊天窗口打开回调
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Attention_OpenPrivateLetter(string sdata)
        {
            //debug.Log_yellow(MsgCode.S2C_Attention_OpenPrivateLetter + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff") + "---: " + sdata);
            Log.Print(MsgCode.S2C_Attention_OpenPrivateLetter + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff") + "---: " + sdata);
            ChatPanelData data = JsonMapper.ToObject<ChatPanelData>(sdata);
            MsgCenter.Call(null, MsgCode.S2C_Attention_OpenPrivateLetter, data);
        }

        /// <summary>
        /// 增加新的好友聊天关系
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_NewFriendList(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_NewFriendList + ": " + sdata);
            UserFriend data = JsonMapper.ToObject<UserFriend>(sdata);
            DataMgr.Instance.newUserFriend = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_NewFriendList, null);
        }

        /// <summary>
        /// 修改聊天消息状态
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_UpdateChatMessageState(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_UpdateChatMessageState + ": " + sdata);
            DataCommon data = JsonMapper.ToObject<DataCommon>(sdata);
            //MsgCenter.Call(null, MsgCode.S2C_Friend_UpdateChatMessageState, null);
        }

        /// <summary>
        /// 查看好友给自己发送的未读消息数量
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_GetUnreadMessageNum(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_GetUnreadMessageNum + ": " + sdata);
            GetUnreadMessageNum data = JsonMapper.ToObject<GetUnreadMessageNum>(sdata);
            DataMgr.Instance.getUnreadMessageNumRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_GetUnreadMessageNum, null);
        }

        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="sdata"></param>
        private void S2C_Friend_UpdateRemark(string sdata)
        {
            Log.Print(MsgCode.S2C_Friend_UpdateRemark + ": " + sdata);
            UpdateRemarkRes data = JsonMapper.ToObject<UpdateRemarkRes>(sdata);
            DataMgr.Instance.updateRemarkRes = data;
            MsgCenter.Call(null, MsgCode.S2C_Friend_UpdateRemark, null);
        }
        #endregion
        #endregion

        #region 客户端-》服务器
        public void C2S_Test_Test()//JUI数据绑定后 数据改变UI自动刷新
        {
            if (NoNetwork()) return;
            //Data 为了即时显示
            DataMgr.Instance.dataTest.Id++;
            DataMgr.Instance.dataTest.Name = "Jack";
            //Net 为了发送数据给后端
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Test_Test);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataTest).ToString());
            webSocket.Send(sb.ToString());
        }


        #region Other_
        public void C2S_Other_SaveSession(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Other_SaveSession);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Other_SaveSession);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region Main_
        public void C2S_Game_FirstTimePlay()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Game_FirstTimePlay + JsonMapper.ToJson(DataMgr.Instance.dataBFirstToServer).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_FirstTimePlay);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBFirstToServer).ToString());
            webSocket.Send(sb.ToString());
        }

        public void C2S_Main_HomeGetInfo(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Main_HomeGetInfo + ":" + uId);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Main_HomeGetInfo);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取广播信息
        /// </summary>
        /// <param name="type"></param>
        public void C2S_System_Broadcast()
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_System_Broadcast);
            sb.Append("#");
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取服务器时间,心跳
        /// </summary>
        public void C2S_Game_GetServerTime()
        {
            //if (webSocket == null) return;
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Game_GetServerTime);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_GetServerTime);
            sb.Append("#");
            sb.Append(GameData.userId.ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 背包
        /// </summary>
        /// <param name="userId"></param>
        public void C2S_Main_GetBagInfo(string userId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Main_GetBagInfo);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Main_GetBagInfo);
            sb.Append("#");
            sb.Append(userId);
            webSocket.Send(sb.ToString());
        }
        #endregion

        /// <summary>
        /// 获取活动排行榜数据
        /// </summary>
        public void C2S_Activity_GetRecard()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Activity_PrizeList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Activity_PrizeList);
            sb.Append("#");
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 红包
        /// </summary>
        public void C2S_User_Get_Red_Packet(string redQuota, string dateTime)
        {
            if (NoNetwork()) return;

            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_User_Get_Red_Packet);
            var param = new
            {
                userId = GameData.userId,
                redQuota = redQuota,
                getTime = dateTime
            };
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param));

            Log.Print(sb.ToString());

            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 红包记录
        /// </summary>
        public void C2S_Get_Newest_Red_List()
        {
            if (NoNetwork()) return;

            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Get_Newest_Red_List);
            var param = new
            {
                userId = GameData.userId,
            };
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param));

            Log.Print(sb.ToString());

            webSocket.Send(sb.ToString());
        }

        #region Love_
        /// <summary>
        /// 拉取任务列表
        /// </summary>
        public void C2S_Love_TaskGetList(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_TaskGetList + "uId: " + uId);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_TaskGetList);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 更新签到
        /// </summary>
        public void C2S_Love_TaskUpdateSignIn()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_TaskUpdateSignIn + JsonMapper.ToJson(DataMgr.Instance.dataBSignInItemReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_TaskUpdateSignIn);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBSignInItemReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 领取任务奖励
        /// </summary>
        public void C2S_Love_TaskUpdateItemReward()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_TaskUpdateItemReward + JsonMapper.ToJson(DataMgr.Instance.DataBReceiveReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_TaskUpdateItemReward);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.DataBReceiveReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        public void C2S_Love_TaskUpdateItem()
        {
            if (NoNetwork())
            {
                debug.Log_Blue("网络异常-任务无法完成");
                return;
            }
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_TaskUpdateItem);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBTaskInfoReq).ToString());
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void C2S_Init_UserInfo(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Init_UserInfo);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Init_UserInfo);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取充能时间
        /// </summary>
        public void C2S_Love_GetChargeTime(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_GetChargeTime);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_GetChargeTime);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 充能
        /// </summary>
        public void C2S_Love_Recharge(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_Recharge);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_Recharge);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取捐献列表
        /// </summary>
        public void C2S_Love_GetDonateList()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_GetDonateList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_GetDonateList);
            sb.Append("#");
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取捐献详情
        /// </summary>
        public void C2S_Love_DonateDetailsInfo()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_DonateDetailsInfo + JsonMapper.ToJson(DataMgr.Instance.dataBProjectInfoReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_DonateDetailsInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBProjectInfoReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 捐献
        /// </summary>
        public void C2S_Love_Donate()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Love_Donate + JsonMapper.ToJson(DataMgr.Instance.dataBDonateReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Love_Donate);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBDonateReq).ToString());
            webSocket.Send(sb.ToString());
        }
        /// <summary>
        /// 获取 用户捐献后，公益项目的最新数据
        /// </summary>
        //public void C2S_Project_NewProjectDonateInfo()
        //{
        //    if (NoNetwork()) return;
        //    StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Project_NewProjectDonateInfo);
        //    sb.Append("#");
        //    sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataNewDataInfo).ToString());
        //    webSocket.Send(sb.ToString());
        //}
        #endregion

        #region Honor_
        /// <summary>
        /// 获取荣誉捐献记录
        /// </summary>
        public void C2S_Honor_GetHonorRecordList(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Honor_GetHonorRecordList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Honor_GetHonorRecordList);
            sb.Append("#");
            sb.Append(uId);
            debug.Log_Blue(sb.ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取荣誉排名
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Honor_GetHonorRank(RankingType rankingType = RankingType.Day)
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Honor_GetHonorRank);
            var param = new { userId = GameData.userId, rankType = (int)rankingType };
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param));
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取荣誉勋章列表
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Honor_GetHonormedalInfo(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Honor_GetHonormedalInfo);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Honor_GetHonormedalInfo);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region Game_DXG
        /// <summary>
        /// 获取合成大西瓜分数
        /// </summary>
        public void C2S_Game_GetGameDXGScore()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Game_GetGameDXGScore);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_GetGameDXGScore);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBGetGameDXGScoreReq).ToString());
            debug.Log_Blue("--->> " + sb.ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取合成大西瓜排行榜
        /// </summary>
        public void C2S_Game_GetGameDXGRankList(RankingType rankingType = RankingType.All)
        {
            if (NoNetwork()) return;
            DataMgr.Instance.dataBGetGameDXGScoreReq.rankType = rankingType;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_GetGameDXGRankList);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBGetGameDXGScoreReq).ToString());
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 上传合成大西瓜分数
        /// </summary>
        public void C2S_Game_SaveGameDXGScore()
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_SaveGameDXGScore);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBGameDXG_SaveScoreReq).ToString());
            Log.Print(sb.ToString());
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region Game_Jump
        public void C2S_Game_Jump_Enter_Homepage()
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Enter_Homepage);
            sb.Append("#");
            var param = new { userId = GameData.userId };
            sb.Append(JsonMapper.ToJson(param));
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        public void C2S_Game_Jump_Get_Role_Theme()
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Get_Role_Theme);
            sb.Append("#");
            var param = new { userId = GameData.userId };
            sb.Append(JsonMapper.ToJson(param));
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        //解锁主题或角色
        public void C2S_Game_Jump_Unlock_Role_Theme(bool isCharacter, int id)
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Unlock_Role_Theme);
            sb.Append("#");
            if (isCharacter)
            {
                var param = new { userId = GameData.userId, roleId = id };
                sb.Append(JsonMapper.ToJson(param).ToString());
            }
            else
            {
                var param = new { userId = GameData.userId, themeId = id };
                sb.Append(JsonMapper.ToJson(param).ToString());
            }
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }


        //查看未解锁的主题或角色
        public void C2S_Game_Jump_Check_Unlock_Condition(bool isCharacter, int id)
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Check_Unlock_Condition);
            sb.Append("#");
            if (isCharacter)
            {
                var param = new { userId = GameData.userId, roleId = id };
                sb.Append(JsonMapper.ToJson(param).ToString());
            }
            else
            {
                var param = new { userId = GameData.userId, themeId = id };
                sb.Append(JsonMapper.ToJson(param).ToString());
            }
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        //跳跳乐排行榜
        public void C2S_Game_Jump_Get_Score_Rank(RankingType rankingType = RankingType.All)
        {
            if (NoNetwork()) return;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Get_Score_Rank);
            sb.Append("#");
            var param = new { userId = GameData.userId, rankType = (int)rankingType };
            sb.Append(JsonMapper.ToJson(param));
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        //上传本局游戏数据
        public void C2S_Game_Jump_Upload_Score()
        {
            if (NoNetwork()) return;
            DataMgr.Instance.dataJumpGameRecord.userId = GameData.userId;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Game_Jump_Upload_Score);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataJumpGameRecord).ToString());
            Log.Print(sb);

            webSocket.Send(sb.ToString());
        }
        #endregion

        #region Shop
        /// <summary>
        /// 获取兑换商城商品信息列表
        /// </summary>
        public void C2S_Shop_GetShopInfoList()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Shop_GetShopInfoList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Shop_GetShopInfoList);
            sb.Append("#");
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取兑换记录列表
        /// </summary>
        public void C2S_Shop_GetShopRecordList()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Shop_GetShopRecordList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Shop_GetShopRecordList);
            sb.Append("#");
            sb.Append(GameData.userId.ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 兑换
        /// </summary>
        public void C2S_Shop_exchange()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Shop_exchange);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Shop_exchange);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBShopExchangeReq).ToString());
            webSocket.Send(sb.ToString());
        }
        #endregion


        #region Lucky
        /// <summary>
        /// 获取幸运转盘抽奖结果
        /// </summary>
        public void C2S_Lucky_LuckyHouseDraw(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Lucky_LuckyHouseDraw);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Lucky_LuckyHouseDraw);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region BagCard
        /// <summary>
        /// 使用道具卡
        /// </summary>
        public void C2S_BagCard_UserCard()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_BagCard_UserCard);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_BagCard_UserCard);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBUseBagCard).ToString());
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region Friend
        /// <summary>
        /// 查看个人信息
        /// </summary>
        public void C2S_Friend_ViewPersonalInfo()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_ViewPersonalInfo + "  " + JsonMapper.ToJson(DataMgr.Instance.viewPersonalInfoReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ViewPersonalInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.viewPersonalInfoReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取用户认证信息
        /// </summary>
        public void C2S_User_ViewRealInfo(long uId)
        {
            if (NoNetwork()) return;
            var param = JsonMapper.ToJson(new ViewPersonalRealInfoReq() { userId = GameData.userId, friendId = uId });
            Log.Print(WebSocketCode.C2S_User_ViewRealInfo + "  " + param);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_User_ViewRealInfo);
            sb.Append("#");
            sb.Append(param);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 查看别人信息
        /// </summary>
        public void C2S_Friend_ViewFriendInfo()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_ViewFriendInfo);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ViewFriendInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.viewPersonalInfoReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 查看关注用户个人资料
        /// </summary>
        public void C2S_Attention_UserInfo()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Attention_UserInfo);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Attention_UserInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.attentionUserInfoReq).ToString());
            webSocket.Send(sb.ToString());
        }
        /// <summary>
        /// 关注
        /// </summary>
        public void C2S_Friend_FollowToUser()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_FollowToUser);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_FollowToUser);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.followToUserReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        public void C2S_Friend_UnFollowToUser()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_UnFollowToUser);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_UnFollowToUser);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.UnfollowToUserReq).ToString());
            webSocket.Send(sb.ToString());
        }
        /// <summary>
        /// 关注用户列表
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Friend_AttentionList(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_AttentionList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_AttentionList);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 黑名单列表
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_GetBlackList()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_GetBlackListUserInfoList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_GetBlackListUserInfoList);
            sb.Append("#");
            sb.Append(GameData.userId.ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取好友列表 
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Friend_ChatRelationList(string uId)
        {
            if (NoNetwork()) return;
            debug.Log_yellow(WebSocketCode.C2S_Friend_ChatRelationList + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff"));

            Log.Print(WebSocketCode.C2S_Friend_ChatRelationList);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ChatRelationList);
            sb.Append("#");
            sb.Append(uId);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 编辑个人资料
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Friend_CompilePersonalInfo()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_CompilePersonalInfo + " " + JsonMapper.ToJson(DataMgr.Instance.dataEditorReq).ToString());
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_CompilePersonalInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataEditorReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 拉黑好友
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <param name="blackList">黑名单状态  5拉入  6移出</param>
        public void C2S_Friend_PullInOrPullOutBlackList(long userId, long friendId, int blackList)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_PullInOrPullOutBlackList);
            DataBlockReq dataBlock = new DataBlockReq() { userId = userId, friendId = friendId, blackList = blackList };
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_PullInOrPullOutBlackList);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(dataBlock).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 申请查看用户认证信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        public void C2S_Friend_ApplyViewUserRealInfo(long userId, long friendId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_ApplyViewUserRealInfo);
            var param = new { userId, friendId };
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ApplyViewUserRealInfo);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 处理接收到的认证信息查看申请
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <param name="processState">1同意2拒绝</param>
        public void C2S_Friend_ProcessViewUserRealInfoApply(long userId, long friendId, int processState)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_ProcessViewUserRealInfoApply);
            var param = new { userId, friendId, processState };
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ProcessViewUserRealInfoApply);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 确认认证信息申请处理结果
        /// </summary>
        public void C2S_Friend_ConfirmUserRealInfoApply(long userId, long friendId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_ConfirmUserRealInfoApply);
            var param = new { userId, friendId };
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ConfirmUserRealInfoApply);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取匹配用户池
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Friend_MatchUserPool()
        {
            if (NoNetwork()) return;

            DataMgr.Instance.matchingConditionUserReq.userId = GameData.userId;
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_MatchUserPool);
            sb.Append("#");
            //sb.Append(uId);
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.matchingConditionUserReq).ToString());
            Log.Print(sb);
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 匹配单个
        /// </summary>
        /// <param name="uId"></param>
        public void C2S_Friend_MatchUserForChat(string uId)
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_MatchUserForChat);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_MatchUserForChat);
            sb.Append("#");
            //sb.Append(uId);
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.matchingConditionUserReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 添加聊天关系
        /// </summary>
        public void C2S_Friend_AddChatRelation()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_AddChatRelation);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_AddChatRelation);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataAddChatRelationReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 添加匹配聊天关系
        /// </summary>
        public void C2S_Friend_AddRandomUserRelation()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_AddRandomUserRelation);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_AddRandomUserRelation);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.addChatRelationReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 删除聊天关系
        /// </summary>
        public void C2S_Friend_RemoveChatRelation()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_RemoveChatRelation);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_RemoveChatRelation);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataRemoveChatRelationReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 获取聊天记录
        /// </summary>
        public void C2S_Friend_ViewChatRecord()
        {
            if (NoNetwork()) return;

            //debug.Log_yellow(WebSocketCode.C2S_Friend_ViewChatRecord + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff"));

            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_ViewChatRecord);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataViewChatRecordReq).ToString());
            webSocket.Send(sb.ToString());
            Log.Print(WebSocketCode.C2S_Friend_ViewChatRecord + ":" + sb.ToString());
        }

        /// <summary>
        ///关注列表 打开私信聊天窗口
        /// </summary>
        public void C2S_Attention_OpenPrivateLetter()
        {
            if (NoNetwork()) return;
            var param = new
            {
                userId = GameData.userId,
                attentionId = GameData.curFriendId
            };
            //debug.Log_yellow(WebSocketCode.C2S_Attention_OpenPrivateLetter + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff"));
            Log.Print(WebSocketCode.C2S_Attention_OpenPrivateLetter + "--Time:" + DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd h:mm:ss.fff"));
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Attention_OpenPrivateLetter);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(param).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void C2S_Friend_SendMessage()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_SendMessage);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_SendMessage);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataSendMessageReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 修改聊天消息未读状态
        /// </summary>
        public void C2S_Friend_UpdateChatMessageState()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_UpdateChatMessageState);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_UpdateChatMessageState);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.updateChatMessageStateReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 查看好友给自己发送的未读消息数量
        /// </summary>
        public void C2S_Friend_GetUnreadMessageNum()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_GetUnreadMessageNum);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_GetUnreadMessageNum);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.getUnreadMessageNumReq).ToString());
            webSocket.Send(sb.ToString());
        }

        /// <summary>
        /// 修改备注
        /// </summary>
        public void C2S_Friend_UpdateRemark()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Friend_UpdateRemark);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Friend_UpdateRemark);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.updateRemarkReq).ToString());
            webSocket.Send(sb.ToString());
        }
        #endregion

        #region 用户行为统计
        public void C2S_Project_UserBehaviorStatistics()
        {
            if (NoNetwork()) return;
            Log.Print(WebSocketCode.C2S_Project_UserBehaviorStatistics);
            StringBuilder sb = new StringBuilder(WebSocketCode.C2S_Project_UserBehaviorStatistics);
            sb.Append("#");
            sb.Append(JsonMapper.ToJson(DataMgr.Instance.dataBProject).ToString());
            webSocket.Send(sb.ToString());
        }

        #endregion

        #endregion

        #region 下载图片
        /// <summary>
        /// 加载图片
        /// </summary>
        //加载图片的队列
        int ImgNum = 0;
        public void DownLoadImgTwo(Action<Texture> callBack, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                debug.Log_Red("ImgUrl————————————NULL");
                return;
            }
            WebRequestHelper.GetTexture(url.Trim(), r =>
            {
                callBack.Invoke(r);
                ImgNum++;
                if (ImgNum > 20)
                {
                    ImgNum = 0;
                    //GC.Collect();
                    UnityWebRequest.ClearCookieCache();
                    Resources.UnloadUnusedAssets();
                }
            });
        }

        public void DownLoadImg(Action<Sprite> callBack, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                debug.Log_Red("ImgUrl————————————NULL");
                return;
            }
            WebRequestHelper.GetTexture(url.Trim(), r =>
            {
                if (r == null)
                {
                    debug.Log_Red("加载图片失败--" + url);
                    return;
                }
                Sprite sp = Sprite.Create(r, new Rect(0, 0, r.width, r.height), new Vector2(0.5f, 0.5f));
                callBack.Invoke(sp);
                ImgNum++;
                if (ImgNum > 20)
                {
                    ImgNum = 0;
                    //GC.Collect();
                    UnityWebRequest.ClearCookieCache();
                    Resources.UnloadUnusedAssets();
                }
            });
        }

        private Dictionary<string, Sprite> cacheHeadSprite = new Dictionary<string, Sprite>();
        /// <summary>
        /// 非正方形图片处理为正方形，下载图片失败使用一张默认图片
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="url"></param>
        public void DownLoadHeadImg(Action<Sprite> callBack, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                debug.Log_Red("ImgUrl————————————NULL");
                url = GameData.DefaultHeadImgUrl;
            }
            if (cacheHeadSprite != null && cacheHeadSprite.ContainsKey(url))
            {
                Sprite sp = cacheHeadSprite[url];
                //Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callBack.Invoke(sp);
                return;
            }
            WebRequestHelper.GetTexture(url.Trim(), r =>
            {
                if (r == null)
                {
                    WebRequestHelper.GetTexture(GameData.DefaultHeadImgUrl.Trim(), r =>
                   {
                       Sprite sp = Sprite.Create(r, new Rect(0, 0, r.width, r.height), new Vector2(0.5f, 0.5f));

                       if (cacheHeadSprite.ContainsKey(url))
                       {
                           cacheHeadSprite[url] = sp;
                       }
                       else
                       {
                           cacheHeadSprite.Add(url, sp);
                       }
                       callBack.Invoke(sp);
                       ImgNum++;
                       if (ImgNum > 20)
                       {
                           ImgNum = 0;
                           //GC.Collect();
                           UnityWebRequest.ClearCookieCache();
                           Resources.UnloadUnusedAssets();
                       }
                   });
                }
                else
                {
                    r = GameTools.GetTextureResizer(r);
                    Sprite sp = Sprite.Create(r, new Rect(0, 0, r.width, r.height), new Vector2(0.5f, 0.5f));

                    if (cacheHeadSprite.ContainsKey(url))
                    {
                        cacheHeadSprite[url] = sp;
                    }
                    else
                    {
                        cacheHeadSprite.Add(url, sp);
                    }
                    //Sprite sp = Sprite.Create(r, new Rect(0, 0, r.width, r.height), new Vector2(0.5f, 0.5f));
                    callBack.Invoke(sp);
                    ImgNum++;
                    if (ImgNum > 20)
                    {
                        debug.Log_Blue("ImgNum---" + ImgNum);
                        ImgNum = 0;
                        //GC.Collect();
                        UnityWebRequest.ClearCookieCache();
                        Resources.UnloadUnusedAssets();
                    }
                }
            });
        }

        #endregion
    }
}

/*
消息格式：
前缀_模块名_业务名#JsonData

前缀：S2C_ C2S_

模块名：Donate_ Honor_ Love_ Friend_ Game_ Shop_ Lucky_

业务名：

例子：C2S_Test_Test#
{
"id":1,
"age":20,
"name":"jack"
}
*/


