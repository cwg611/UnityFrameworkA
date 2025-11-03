using System;
using System.Collections.Generic;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    public class DataBHonor
    {

    }

    /// <summary>
    /// 获取荣誉展厅 捐献记录
    /// </summary>
    public class DataBDonateRecordListRes
    {
        public List<DonateRecord> userDonateRecordList { get; set; }
    }
    public class DonateRecord
    {
        //用户游戏信息主键id
        public int id { get; set; }
        //用户Id
        public long userId { get; set; }
        
        public string nickName { get; set; }
        //公益项目id
        public int projectId { get; set; }
        //捐献爱心数量
        public int donateNum { get; set; }
        //捐献时间
        public String donateTime { get; set; }
        //获取爱心币数量
        public int getLoveMoneyNum { get; set; }
        //公益项目标题
        public String projectTitle { get; set; }
    }

    /// <summary>
    /// 获取荣誉大厅捐献排行
    /// </summary>
    public class DataBDonateRankInfoRes
    {
        public List<DonateLoveRankListItem> donateLoveRankList { get; set; }
        public RankByUserId rankByUserId { get; set; }
        public List<HonorMedal> honorMedalList { get; set; }
        public List<UserMedalRecord> userHighestMedalList { get; set; }
    }

    public class DonateLoveRankListItem
    {
        public int accumulativeGetLoveNum { get; set; }

        public int continuousSignInDayNum { get; set; }

        public int curEnergy { get; set; }

        public string curLoveMoney { get; set; }

        public int curLoveNum { get; set; }

        public int donateLoveNum { get; set; }

        public int gender { get; set; }

        public string headImgUrl { get; set; }

        public int id { get; set; }

        public string lastChargePastTime { get; set; }

        public string lastChargeTime { get; set; }

        public int maxEnergy { get; set; }

        public string nickName { get; set; }

        public int novicesReward { get; set; }

        public int rowNum { get; set; }

        public string signInTime { get; set; }

        public int time { get; set; }

        public long userId { get; set; }
    }

    public class RankByUserId
    {
        public int accumulativeGetLoveNum { get; set; }

        public int continuousSignInDayNum { get; set; }

        public int curEnergy { get; set; }

        public string curLoveMoney { get; set; }

        public int curLoveNum { get; set; }

        public int donateLoveNum { get; set; }

        public int gender { get; set; }

        public string headImgUrl { get; set; }

        public int id { get; set; }

        public string lastChargePastTime { get; set; }

        public string lastChargeTime { get; set; }

        public int maxEnergy { get; set; }

        public string nickName { get; set; }

        public int novicesReward { get; set; }

        public int rowNum { get; set; }

        public string signInTime { get; set; }

        public int time { get; set; }

        public long userId { get; set; }
    }

    /// <summary>
    /// 获取荣誉勋章列表
    /// </summary>
    public class DataBHonorMadelListRes
    {
        public List<HonorMedal> honorMedalList { get; set; }
        public List<UserMedalRecord> userMedalRecordListByUserId { get; set; }
    }

    public class HonorMedal
    {
        //勋章id
        public int medalId { get; set; }
        //解锁勋章数量
        public int unlockNum { get; set; }
        //勋章标题
        public String medalTitle { get; set; }
        //勋章描述
        public String medalDesc { get; set; }
        //该勋章已获取数量
        public int gainNum { get; set; }
    }

    public class UserMedalRecord
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //荣誉勋章id
        public int medalId { get; set; }
        //获取时间
        public String getTime { get; set; }
        //荣誉勋章id
        public int medalState { get; set; }
        // 获取该勋章名次
        public int gainNum { get; set; }
    }


}
