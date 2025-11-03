
using System.Collections.Generic;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    /// <summary>
    /// 推送玩家信息
    /// </summary>
    public class DataBUpdataData
    {
        public int curEnergy { get; set; }
        public int donateLoveNum { get; set; }
        public int curLoveNum { get; set; }
        public int accumulativeGetLoveNum { get; set; }
        public string curLoveMoney { get; set; } //当前爱心币

        public int allUnreadChatMessageNum { get; set; } //当前未读消息数量

        public int isHatching { get; set; } //是否在孵化中  1：正在孵化 0:没有孵化

        public int canDraw { get; set; }  //当前是否可抽奖  1可以 0不可

        public int freeMatchTime { get; set; }    //每天免费匹配次数 默认10  使用完24点恢复

        ////当日累计从游戏获取爱心数量
        public string todayTotalGetLoveMoneyFromGame { get; set; }
        //背包数据
        public List<PrizeStore> prizeStoreList { get; set; }
    }

}
