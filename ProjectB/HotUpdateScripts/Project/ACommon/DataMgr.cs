using HotUpdateScripts.Project.BasePrj.Data;
using JEngine.Core;
using HotUpdateScripts.Project.Game.GameA.Data;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 数据代理 DataProxy
    /// </summary>
    public class DataMgr : Singleton<DataMgr>
    {
        public DataTest dataTest = new DataTest();

        public DataBServerTime dataServerTime = new DataBServerTime();

        #region Main_
        public DataBFirstToUnity dataBFirstToUnity = new DataBFirstToUnity();
        public DataBMainHome dataBMainHome = new DataBMainHome();
        public DataBroadcast dataBroadcast = new DataBroadcast();
        public DataBChargeTime dataBChargeTime = new DataBChargeTime();
        public DataBag dataBag = new DataBag();

        public DataBActivity DataBActivity = new DataBActivity();
        public DataRedPackage DataRedPackage = new DataRedPackage();
        public DataUserRedRecordList DataUserRedRecordList = new DataUserRedRecordList();
        #endregion

        #region Love
        //任务数据
        public DataBLoveTask dataBLoveTask = new DataBLoveTask();
        //签到
        public DataBSignInItemReq dataBSignInItemReq = new DataBSignInItemReq();
        //领取任务奖励
        public DataBReceiveReq DataBReceiveReq = new DataBReceiveReq();
        //更新任务状态
        public DataBTaskInfoReq dataBTaskInfoReq = new DataBTaskInfoReq();

        public DataBMainTaskItem GetTaskItemByType(int type)
        {
            if (dataBLoveTask != null && dataBLoveTask.taskList != null)
            {
                var taskList = dataBLoveTask.taskList;
                for (int i = 0; i < taskList.Count; i++)
                {
                    if (taskList[i].taskType == type)
                    {
                        return taskList[i];
                    }
                }
            }
            return null;
        }
        #endregion

        #region Hope_


        #endregion

        #region Honor_
        public DataBDonateRecordListRes dataBDonateRecordListRes = new DataBDonateRecordListRes();
        public DataBDonateRankInfoRes dataBDonateRankInfoRes = new DataBDonateRankInfoRes();
        public DataBHonorMadelListRes dataBHonorMadelListRes = new DataBHonorMadelListRes();
        #endregion

        #region Donate_

        public DataBBenefitProjectListRes dataBBenefitProjectListRes = new DataBBenefitProjectListRes(); //捐献项目列表

        public DataBProjectInfoReq dataBProjectInfoReq = new DataBProjectInfoReq();
        public DataBProjectInfoRes dataBProjectInfoRes = new DataBProjectInfoRes(); //捐献项目详情

        public DataBDonateReq dataBDonateReq = new DataBDonateReq(); //捐献

        public DataBProjectInfoReq dataNewDataInfo = new DataBProjectInfoReq();
        public NewProjectDonateInfo newProjectDonateInfo = new NewProjectDonateInfo();
        #endregion

        #region UpdateData
        public DataBUpdataData dataBUpdataData = new DataBUpdataData();
        public DataBag dataBUpdateBagData = new DataBag();
        #endregion

        #region Game
        public DataBGetGameDXGScoreReq dataBGetGameDXGScoreReq = new DataBGetGameDXGScoreReq();
        public DataBGameDXG_SaveScoreReq dataBGameDXG_SaveScoreReq = new DataBGameDXG_SaveScoreReq();

        public DataBUserGameScoreRes dataBUserGameScoreRes = new DataBUserGameScoreRes();
        public DataBScoreRankRes dataBScoreRankRes = new DataBScoreRankRes();

        public DataJumpGameRecord dataJumpGameRecord = new DataJumpGameRecord();

        public DataGameBUnLockedState dataGameBUnLockedState = new DataGameBUnLockedState();

        public DataScoreRank dataJumpScoreRank = new DataScoreRank();
        #endregion

        #region Shop
        public DataBShopListRes dataBShopListRes = new DataBShopListRes();
        public DataBShopRecordListRes dataBShopRecordListRes = new DataBShopRecordListRes();

        public DataBShopExchangeReq dataBShopExchangeReq = new DataBShopExchangeReq();
        #endregion

        #region Luck
        public DataBLuck dataBLuck = new DataBLuck();
        #endregion

        #region BagCard
        public DataBUseBagCard dataBUseBagCard = new DataBUseBagCard();
        #endregion

        #region Friend
        public DataBViewPersonalInfoRes dataBViewPersonalInfoRes = new DataBViewPersonalInfoRes();
        public DataBViewPersonalInfoRes dataBViewFriendInfoRes = new DataBViewPersonalInfoRes();
        public DataBViewAttentionInfoRes dataBViewAttentionInfoRes = new DataBViewAttentionInfoRes();

        public FollowToUserReq followToUserReq = new FollowToUserReq();
        public FollowToUserReq UnfollowToUserReq = new FollowToUserReq();

        public MatchingConditionUserReq matchingConditionUserReq = new MatchingConditionUserReq() { minAge = 18, maxAge = 60, city = "不限" };

        public AttentionListRes attentionListRes = new AttentionListRes();

        public DataFriendList dataFriendList = new DataFriendList();

        public DataEditorReq dataEditorReq = new DataEditorReq();

        public DataRandomUserFriendDataList dataRandomUserFriendDataList = new DataRandomUserFriendDataList();
        public DataRandomUser dataRandomUser = new DataRandomUser();

        public DataChatRelationReq dataAddChatRelationReq = new DataChatRelationReq();
        public DataChatRelationReq dataRemoveChatRelationReq = new DataChatRelationReq();

        public DataChatRelationReq dataViewChatRecordReq = new DataChatRelationReq();

        public DataFriendViewChatRecord dataFriendViewChatRecord = new DataFriendViewChatRecord();

        public ChatMessages dataSendMessageReq = new ChatMessages(); //发送消息

        public ChatMessages dataSendMessage = new ChatMessages();//接收消息

        public ViewPersonalInfoReq viewPersonalInfoReq = new ViewPersonalInfoReq();//查看个人资料传参
        public AttentionUserInfoReq attentionUserInfoReq = new AttentionUserInfoReq();//查看关注用户资料传参

        public UserFriend newUserFriend = new UserFriend(); //新增加的聊天好友关系

        public UpdateChatMessageStateReq updateChatMessageStateReq = new UpdateChatMessageStateReq();//修改聊天消息状态

        public UpdateChatMessageStateReq getUnreadMessageNumReq = new UpdateChatMessageStateReq(); //查看好友给自己发送的未读消息数量

        public GetUnreadMessageNum getUnreadMessageNumRes = new GetUnreadMessageNum();

        public UpdateRemark updateRemarkReq = new UpdateRemark();

        public UpdateRemarkRes updateRemarkRes = new UpdateRemarkRes();

        public AddChatRelationReq addChatRelationReq = new AddChatRelationReq(); //添加匹配聊天关系

        public DataBFirstToServer dataBFirstToServer = new DataBFirstToServer(); //第一次登录传给服务器
        #endregion

        #region Project
        public DataBProject dataBProject = new DataBProject();

        #endregion
    }

}
