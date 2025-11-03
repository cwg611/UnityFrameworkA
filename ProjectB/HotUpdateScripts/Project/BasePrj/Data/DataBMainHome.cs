using HotUpdateScripts.Project.Game.GameA.Data;
using System.Collections.Generic;

/* 不管服务器返回什么 客户端只接受要用的（只写用到的实体字段） */
namespace HotUpdateScripts.Project.BasePrj.Data
{
    public class DataBMainHome
    {
        public string headImgUrl { get; set; }

        public string nickName { get; set; }

        public long userId { get; set; }


        public int allUnreadChatMessageNum { get; set; }

        public int curLoveNum { get; set; } //当前爱心数

        /// <summary>
        /// 用户累计获取爱心数量
        /// </summary>
        public int accumulativeGetLoveNum { get; set; }

        /// <summary>
        /// 用户捐献爱心数量
        /// </summary>
        public int donateLoveNum { get; set; }

        public string curLoveMoney { get; set; } //当前爱心币
        //public int curLoveMoney { get; set; } //当前爱心币

        public int isHatching { get; set; } //是否在孵化中  1：正在孵化 0:没有孵化

        public int canDraw { get; set; }  //当前是否可抽奖  1可以 0不可

        //每天免费匹配次数 默认10  使用完24点恢复
        public int freeMatchTime { get; set; }
        private bool allowMakingFriends;
        public bool AllowMakingFriends
        {
            get
            {
                allowMakingFriends = false;
                if (!string.IsNullOrEmpty(workNo) && !string.IsNullOrEmpty(marry))
                {
                    if (marry.Equals("未婚")) allowMakingFriends = true;
                }
                return allowMakingFriends;
            }
        }
        //工号信息
        public string workNo { get; set; }
        // 用户婚姻状态
        public string marry { get; set; }
        //最近一次从游戏获取爱心币时间
        //public string lastTimeGetLoveMoneyFromGame { get; set; }
        //////当日累计从游戏获取爱心数量
        //public string todayTotalGetLoveMoneyFromGame { get; set; }

    }

    public class DataCommon
    {
        public string msg { get; set; }
    }

    public class DataRedPackage
    {
        public bool success { get; set; }
        public double redQuota { get; set; }
        public string Message { get; set; }
    }

    public class DataUserRedRecordList
    {
        public List<DataRedPackageRecord> userRedList { get; set; }

    }
    public class DataRedPackageRecord
    {
        public long userId { get; set; }
        public string nickName { get; set; }
        public double redEnvelopesQuota { get; set; }
    }

    //服务器时间
    public class DataBServerTime
    {
        public string serverTime { get; set; }
    }

    //广播信息
    public class DataBroadcast
    {
        public List<DonateRecord> donateList { get; set; }

        public DataGameA gameTop { get; set; }

        public DataGameA topJumpGameScore { get; set; }

        public List<UserConversionRecord> conversionList { get; set; }
    }

    //背包数据
    public class DataBag
    {
        public List<PrizeStore> prizeStoreList { get; set; }
    }

    public class PrizeStore
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //虚拟道具id
        public int productId { get; set; }

        //拥有的虚拟道具数量
        public int productNum { get; set; }

        //虚拟道具名字
        public string productName { get; set; }

        //虚拟道具描述
        public string productDesc { get; set; }
    }

    /// <summary>
    /// 使用道具卡
    /// </summary>
    public class DataBUseBagCard
    {
        //用户Id
        public long userId { get; set; }
        //道具Id
        public int productId { get; set; }
        //使用道具数量
        public int productNum { get; set; }

    }

    /// <summary>
    /// 第一次登录传给服务器
    /// </summary>
    public class DataBFirstToServer
    {
        public string token { get; set; }
    }

    public class DataBFirstToUnity
    {
        public bool msg { get; set; }
        public long userId { get; set; }
    }

}