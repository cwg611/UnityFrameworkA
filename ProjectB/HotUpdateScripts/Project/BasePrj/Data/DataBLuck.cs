using System;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    /// <summary>
    /// 幸运抽奖结果
    /// </summary>
    public class DataBLuck
    {
       public PrizeInfo prizeInfo { get; set; }
    }

    public class PrizeInfo
    {
        //主键id
        public int id { get; set; }
        //奖品id
        public int prizeId { get; set; }  //1： 爱心*1  2：爱心*2  3：加速卡*1  4：加速卡*2  5：能量卡*3  6：交友卡*1  7:交友卡*3
        //奖品描述
        public String prizeDesc { get; set; }
        //奖品中奖几率
        public int prizeWeight { get; set; }
    }
}
