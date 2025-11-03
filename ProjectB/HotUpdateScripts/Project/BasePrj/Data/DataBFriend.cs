using System;
using System.Collections.Generic;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    public class DataBFriend
    {
        public long userId { get; set; }
        public int friendId { get; set; }
    }

    public enum UIEditPanelType
    {
        PersonalInfoPanel,
        ExpertInfoPanel
    }

    /// <summary>
    /// 查看我的信息
    /// </summary>
    public class DataBViewPersonalInfoRes
    {
        public UserFriendData userFriendData { get; set; }
        //我关注数量
        public int meAttentionNum { get; set; }
        //关注我的数量
        public int attentionToMeNum { get; set; }
    }

    public class DataBViewAttentionInfoRes
    {
        public UserFriendData userInfo { get; set; }
    }

    public class ExchangeInfoMessageData
    {
        public int applicantId { get; set; }
        public int processId { get; set; }
        public int processState { get; set; }

    }

    //交友信息
    public class UserFriendData
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }

        //用户是否在线  1在线 0不在线
        public int isOnline { get; set; }

        //是否被关注  1被关注  0 没有被关注
        public int isBeFollowed { get; set; }
        //备注
        public string remark { get; set; }


        //用户昵称
        public String nickName { get; set; }
        //用户头像地址
        public String headImgUrl { get; set; }
        //用户个性签名
        public String personalizedSignature { get; set; }

        //用户性别 1男 2女
        public int gender { get; set; }

        //个人介绍_______________________________________________________________________
        //用户年龄
        public int age { get; set; }
        // 用户身高
        public int height { get; set; }
        // 用户体重
        public int weight { get; set; }
        // 用户学历
        public int education { get; set; }
        // 用户是否购车
        public int house { get; set; }
        // 用户是否购房
        public int car { get; set; }

        // 用户生肖
        public string zodiac { get; set; }
        //星座
        public string constellation { get; set; }
        //用户所在省份
        public string province { get; set; }
        //用户所在 市
        public string city { get; set; }

        // 用户性格
        public String character { get; set; }
        // 用户外形
        public String appearance { get; set; }
        //我的标签
        public String mineLabel { get; set; }



        //择偶意向_______________________________________________________________________
        // 期望用户年龄  expect
        public int expectAge { get; set; }
        // 期望用户身高
        public int expectHeight { get; set; }
        // 期望用户体重
        public int expectWeight { get; set; }
        // 期望用户学历
        public int expectEducation { get; set; }
        // 期望用户是否购车
        public int expectHouse { get; set; }
        // 期望用户是否购房
        public int expectCar { get; set; }
        // 期望用户性格
        public string expectCharacter { get; set; }
        // 期望用户外形
        public string expectAppearance { get; set; }
        //期望的标签
        public string expectLabel { get; set; }
    }

    public class DataToViewRealInfo
    {
        public UserRealInfo realInfo;
    }
    //认证信息
    public class UserRealInfo
    {
        //是否允许查看认证信息 1 允许 2不允许
        public int allowViewRealInfo { get; set; }
        // 主键id
        public int id { get; set; }
        // 用户id
        public long userId { get; set; }
        // 工号
        public String jobNo { get; set; }
        // 姓名
        public String userName { get; set; }
        // 手机号
        public String tel { get; set; }
        // 性别 1 男 2女
        public int sex { get; set; }
        // 籍贯
        public string address { get; set; }
        // 职务名称
        public string dutyName { get; set; }
        // 毕业院校
        public string school { get; set; }
        // 学历
        public string education { get; set; }
        // 婚姻状况
        public string marry { get; set; }
        // 职等
        public string grade { get; set; }
        // 头像
        public string pic { get; set; }
        // 生日
        public string birthday { get; set; }
        // 在职状态（1：在职）
        public int entryState { get; set; }
    }

    /// <summary>
    /// 关注  取消关注
    /// </summary>
    public class FollowToUserReq
    {
        public long userId { get; set; } //用户的id

        public long toAttentionUser { get; set; } //被关注用户的id
    }

    /// <summary>
    /// 匹配筛选条件
    /// </summary>
    public class MatchingConditionUserReq
    {
        public long userId { get; set; } //用户的id
        public int minAge { get; set; }
        public int maxAge { get; set; }

        public string city { get; set; }
    }

    /// <summary>
    /// 关注我的用户列表
    /// </summary>
    public class AttentionListRes
    {
        public List<AttentionToMeList> meAttentionList { get; set; }

        public List<AttentionToMeList> attentionToMeList { get; set; }
    }

    public class AttentionToMeList
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //用户性别  1男 0女
        public int gender { get; set; }
        //用户年龄
        public int age { get; set; }
        //用户昵称
        public String nickName { get; set; }
        //用户头像地址
        public String headImgUrl { get; set; }
        //用户个性签名
        public String personalizedSignature { get; set; }
        //用户所在地区
        //public String region { get; set; }
        //我的标签
        //public String mineLabel { get; set; }
        //期望的标签
        //public String expectLabel { get; set; }
        //用户是否在线  1在线 0不在线
        public int isOnline { get; set; }
        //用户星座
        public String constellation { get; set; }
    }

    /// <summary>
    /// 获取好友列表
    /// </summary>
    public class DataFriendList
    {
        public List<UserFriend> userFriendList { get; set; }
    }

    public class DataUserBlackList
    {
        public List<UserFriend> blackList { get; set; }
    }

    public class UserFriend
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //用户性别  1男 0女
        public int gender { get; set; }
        //用户年龄
        public int age { get; set; }
        //用户昵称
        public String nickName { get; set; }
        //用户头像地址
        public String headImgUrl { get; set; }
        //用户个性签名
        public String personalizedSignature { get; set; }
        //用户所在地区
        public String region { get; set; }
        //我的标签
        public String mineLabel { get; set; }
        //期望的标签
        public String expectLabel { get; set; }
        //用户是否在线  1在线 0不在线
        public int isOnline { get; set; }
        //用户星座
        public string constellation { get; set; }
        public int messageType { get; set; }
        //最后一条消息
        public string lastMessage { get; set; }
        //最后一条消息时间
        public string lastMessageSendTime { get; set; }
        //未读消息条数
        public int unreadChatMessageNum { get; set; }
        public string remark { get; set; }
        //亲密度 一条消息为1,100可以申请查看对方认证信息 300可以申请建立恋爱关系
        public int intimacy { get; set; }
        //可以申请交换
        public bool AllowApplyExchangeInfo
        {
            get { return allowViewRealInfo != 1 && intimacy >= 100&& applyState!=3||(applyState==4&&processState!=0); }
        }
        //可以申请建立
        public bool AllowApplyExchange
        {
            get { return allowViewRealInfo != 1 && intimacy >= 100; }
        }
        //是否允许查看认证信息 1 允许 2不允许
        public int allowViewRealInfo { get; set; }

        // 申请处理状态     3待处理 4 处理完成 默认3 当申请人进行最后确认后,状态变为4
        public int applyState { get; set; }
        // 申请处理结果     1同意申请   2拒绝申请
        public int processState { get; set; }

        // 申请查看认证信息的用户id
        public int applicantId { get; set; }
        public bool isApplicat
        {
            get { return applicantId == GameData.userId; }
        }

        public bool repeat { get; set; }//重复申请
    }

    public class DataBlockReq
    {
        public long userId { get; set; }
        public long friendId { get; set; }
        public int blackList { get; set; }
    }

    /// <summary>
    /// 编辑资料
    /// </summary>
    public class DataEditorReq
    {
        //用户id
        public long userId { get; set; }

        //个人介绍
        public int age { get; set; }//用户年龄
        public int height { get; set; }//身高
        public int weight { get; set; }//体重
        public int education { get; set; }//学历
        public int house { get; set; }//购房
        public int car { get; set; }//购车
        public string zodiac { get; set; }//生肖
        public string constellation { get; set; }//星座
        public string province { get; set; }//地区省
        public string city { get; set; }//地区市
        public string character { get; set; }//性格
        public string appearance { get; set; }//外形
        //我的标签
        public String mineLabel { get; set; }

        //期望
        public int expectAge { get; set; }
        public int expectHeight { get; set; }
        public int expectWeight { get; set; }
        public int expectEducation { get; set; }
        public int expectHouse { get; set; }
        public int expectCar { get; set; }
        public string expectCharacter { get; set; }
        public string expectAppearance { get; set; }
        public string expectLabel { get; set; }


        //用户性别  1男 0女
        //public int gender { get; set; }
        //用户头像地址
        //public string headImgUrl { get; set; }
        //用户个性签名
        //public string personalizedSignature { get; set; }
        //用户所在地区
        //public string region { get; set; }
        //用户是否在线  1在线 0不在线
        //public int isOnline { get; set; }
    }

    /// <summary>
    /// 匹配池 200条信息
    /// </summary>
    public class DataRandomUserFriendDataList
    {
        public List<UserFriend> randomUserFriendDataList { get; set; }
    }

    /// <summary>
    ///  单个
    /// </summary>
    public class DataRandomUser
    {
        public UserFriend randomUser { get; set; }
    }

    /// <summary>
    /// 添加或者删除聊天关系
    /// </summary>
    public class DataChatRelationReq
    {
        public long userId { get; set; }
        public long friendId { get; set; }
    }

    /// <summary>
    /// 聊天记录
    /// </summary>
    public class DataFriendViewChatRecord
    {
        public int blackList { get ; set; }//用户是否黑名单 5在 6不在
        public List<ChatMessages> chatMessages { get; set; }
    }

    public class ChatMessages
    {
        // 消息发出人
        public long fromUserId { get; set; }
        // 消息接收人
        public long toUserId { get; set; }
        // 发送时间
        public string sendTime { get; set; }
        //消息类型：1：图文混排  2：表情图片
        public MessageType messageType { get; set; }
        // 消息内容
        public string messageInfo { get; set; }
    }

    public class ChatPanelData
    {
        //亲密度 一条消息为1,100可以申请查看对方认证信息 300可以申请建立恋爱关系
        public int intimacy { get; set; }
        //可以申请交换
        public bool AllowApplyExchangeInfo
        {
            get { return allowViewRealInfo != 1 && intimacy >= 100 && applyState != 3 || (applyState == 4 && processState != 0); }
        }
        //可以申请建立
        public bool AllowApplyExchange
        {
            get { return allowViewRealInfo != 1 && intimacy >= 100; }
        }
        //是否允许查看认证信息 1 允许 2不允许
        public int allowViewRealInfo { get; set; }

        // 申请处理状态     3待处理 4 处理完成 默认3 当申请人进行最后确认后,状态变为4
        public int applyState { get; set; }
        // 申请处理结果     1同意申请   2拒绝申请
        public int processState { get; set; }

        // 申请查看认证信息的用户id
        public int applicantId { get; set; }
        public bool isApplicat
        {
            get { return applicantId == GameData.userId; }
        }
    }

    /// <summary>
    /// 查看个人资料传参
    /// </summary>
    public class ViewPersonalInfoReq
    {
        public long userId { get; set; }
        public long viewUserId { get; set; }
    }

    /// <summary>
    /// 查看认证资料传参
    /// </summary>
    public class ViewPersonalRealInfoReq
    {
        public long userId { get; set; }
        public long friendId { get; set; }
    }

    /// <summary>
    /// 查看关注用户个人资料
    /// </summary>
    public class AttentionUserInfoReq
    {
        public long userId { get; set; }
    }

    /// <summary>
    /// 修改聊天消息状态
    /// </summary>
    public class UpdateChatMessageStateReq
    {
        public long userId { get; set; }
        public long friendId { get; set; }
    }

    /// <summary>
    /// 查看好友给自己发送的未读消息数量
    /// </summary>
    public class GetUnreadMessageNum
    {
        public long fromUserId { get; set; }
        public int UnreadMessageNum { get; set; }
    }

    //修改好友备注传参
    public class UpdateRemark
    {
        public long userId { get; set; }
        public long friendId { get; set; }
        public string remark { get; set; }
    }
    /// <summary>
    /// 修改备注返回
    /// </summary>
    public class UpdateRemarkRes
    {
        public bool msg { get; set; }
    }

    /// <summary>
    /// 添加匹配聊天关系
    /// </summary>
    public class AddChatRelationReq
    {
        public long userId { get; set; }
        public long randomUserId { get; set; }
    }

    /// <summary>
    /// 添加匹配聊天关系回调
    /// </summary>
    public class AddChatRelationRes
    {
        public bool msg { get; set; }
    }

}
