namespace My.Msg
{

    public enum MsgCode
    {
        OneFingerRotateTransfer,//单指划船传递

        DayNightUpt,//首页UI（白夜）主题更新


        Test,

        S2C_Update_UpdateGameData,//更新数据
        S2C_Update_UpdateBagData,//更新背包数据

        S2C_Game_FirstTimePlay,//第一次登录

        S2C_Main_HomeGetInfo,   //获取首页数据

        S2C_Main_System_Broadcast,   //系统广播公告

        S2C_Love_TaskGetList,  //获取任务列表

        S2C_Love_TaskUpdateSignIn, //签到

        S2C_Love_TaskUpdateItemReward, //领取任务奖励

        S2C_Love_TaskUpdateItem, //更新任务状态

        S2C_Init_UserInfo,//数据重置

        S2C_Love_GetChargeTime, //首页数据中充能时间

        S2C_Love_Recharge, //充能

        S2C_Game_GetServerTime, //服务器时间

        S2C_Get_Newest_Red_List,//红包记录

        S2C_Activity_PrizeList,//活动

        S2C_User_Get_Red_Packet,//领取红包

        S2C_Main_GetBagInfo, //背包

        S2C_Love_GetDonateList,//捐献列表

        S2C_Love_DonateDetailsInfo,//捐献项目详情

        S2C_Love_Donate,//捐献 

        S2C_Project_NewProjectDonateInfo,//捐献后反馈

        S2C_Honor_GetHonorRecordList,//获取荣誉展厅 捐献记录

        S2C_Honor_GetHonorRank, //获取荣誉展厅 排名

        S2C_Honor_GetHonormedalInfo, //获取荣誉展厅 勋章

        S2C_Game_GetGameDXGScore,//获取合成大西瓜分数

        S2C_Game_GetGameDXGRankList,//获取合成大西瓜排行榜

        S2C_Game_SaveGameDXGScore,//上传合成大西瓜分数

        S2C_Game_Jump_Enter_Homepage,//跳跳乐游戏数据

        S2C_Game_Jump_Get_Role_Theme,

        S2C_Game_Jump_Unlock_Role_Theme,//新解锁

        S2C_Game_Jump_Check_Unlock_Condition,//查看未解锁

        S2C_Game_Jump_Get_Score_Rank,//排行榜

        S2C_Game_Jump_Upload_Score,

        S2C_Shop_GetShopInfoList,//获取兑换商店列表

        S2C_Shop_GetShopRecordList, //获取兑换记录列表

        S2C_Shop_exchange,//兑换

        S2C_Lucky_LuckyHouseDraw, //抽奖结果

        S2C_BagCard_UserCard,//使用道具卡

        S2C_Friend_ViewPersonalInfo, //查看个人信息

        S2C_User_ViewRealInfo, //查看用户认证信息

        S2C_Friend_ViewFriendInfo,//查看别人信息

        S2C_Attention_UserInfo,

        S2C_Friend_FollowToUser,//关注

        S2C_Friend_UnFollowToUser,//取消关注

        S2C_Friend_AttentionList,//关注列表

        S2C_Friend_ChatRelationList,//获取好友列表

        S2C_Friend_CompilePersonalInfo,//编辑资料

        S2C_Friend_PullInOrPullOutBlackList,//拉黑好友

        S2C_Friend_BlackListUserInfoList,//黑名单列表

        S2C_Friend_ApplyViewUserRealInfo,//申请查看用户认证信息

        S2C_Friend_ProcessViewUserRealInfoApply,//处理接收到的认证信息查看申请

        S2C_Friend_ConfirmUserRealInfoApply,//确认认证信息申请处理结果

        S2C_Friend_MatchUserPool, //获取匹配池

        S2C_Friend_MatchUserForChat,//匹配个人

        S2C_Friend_AddChatRelation, //添加聊天关系

        S2C_Friend_RemoveChatRelation,//删除聊天关系

        S2C_Friend_ViewChatRecord,//获取聊天记录

        S2C_Friend_SendMessage,//接收消息

        S2C_Attention_OpenPrivateLetter,//打开聊天窗口

        S2C_Friend_NewFriendList,//新增加聊天好友

        S2C_Friend_UpdateChatMessageState, //修改聊天消息状态

        S2C_Friend_GetUnreadMessageNum,//查看好友给自己发送的未读消息数量

        S2C_Friend_UpdateRemark,

        S2C_Friend_AddRandomUserRelation,//添加匹配聊天关系

        //跳跳乐事件
        GameB_SwitchCharacter,

        GameB_SwitchTheme,
    }
}