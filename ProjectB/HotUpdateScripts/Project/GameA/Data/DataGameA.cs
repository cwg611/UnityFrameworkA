using HotUpdateScripts.Project.BasePrj.Data;
using System;
using System.Collections.Generic;

namespace HotUpdateScripts.Project.Game.GameA.Data
{
    /// <summary>
    /// 十二生肖
    /// </summary>
    public class DataGameA
    {
        //主键id
        public int id { get; set; }
        //用户id
        public long userId { get; set; }
        //排行名次
        public int rowNum { get; set; }
        //用户昵称
        public String nickName { get; set; }
        //用户头像地址
        public String headImgUrl { get; set; }
        //游戏得分
        public int gameScore { get; set; }
        //游戏名称
        public String gameName { get; set; }
    }

    /// <summary>
    /// 获取游戏分数
    /// </summary>
    public class DataBUserGameScoreRes
    {
        public DataGameA userGameScore { get; set; }
    }

    /// <summary>
    /// 获取排行榜
    /// </summary>
    public class DataBScoreRankRes
    {
        public List<DataGameA> scoreRankList { get; set; }
        //用户id
        public DataGameA userGameInfo { get; set; }
    }

    /// <summary>
    /// 获取分数  及  获取排行榜
    /// </summary>
    public class DataBGetGameDXGScoreReq
    {
        public long userId { get; set; }
        public String gameName { get; set; }

        public RankingType rankType { get; set; }
    }

    /// <summary>
    /// 上传分数
    /// </summary>
    public class DataBGameDXG_SaveScoreReq
    {
        public long userId { get; set; }
        public String gameName { get; set; }
        public int gameScore { get; set; }
        public string loveMoney { get; set; }
    }



    //跳跳乐数据
    public class DataJumpGame
    {
        //public int userId;
        public int topScore { get; set; }
        public bool hasNewUnlock { get; set; }

        //最近一次从游戏获取爱心币时间
        public string lastTimeGetLoveMoneyFromGame { get; set; }
        ////当日累计从游戏获取爱心数量
        public string todayTotalGetLoveMoneyFromGame { get; set; }
    }

    public class DataJumpGameUnlocked
    {
        public List<int> unLockedRolesId { get; set; }//已解锁角色id，
        public List<int> toLockedRolesId { get; set; }//达到解锁条件没有解锁的角色id
        public List<int> LockedRolesId { get; set; }//未解锁角色

        public List<int> unLockedThemesId { get; set; }//已解锁主题id，
        public List<int> toLockedThemesId { get; set; }//达到解锁条件没有解锁的主题id
        public List<int> LockedThemesId { get; set; } //未解锁主题
    }

    public class DataUnLockProgress
    {
        public bool unlockSuccess;

        public int unlockConditionNumber;
        public int userHave;
        public string unlockConditionDescription;
    }

    //查看解锁状态
    public class DataGameBUnLockedState
    {
        public bool isCharacter;
        public int id;
        public bool isUnLocked;
    }

    public class DataJumpGameRecord
    {
        public long userId;
        public int gameScoreOfThisBureau { get; set; }//本局游戏得分
        public int jumpTimesOfThisBureau { get; set; }//本局跳跃次数
        public int perfectJumpTimesOfThisBureau { get; set; }//本局完美跳跃次数
        public string gameOverTimeOfThisBureau { get; set; }//本局游戏结束时间
        public string loveMoney { get; set; }//本局获得金币
    }

    public class DataScoreRank
    {
        public List<DataScoreRankItem> scoreRank { get; set; }

        public DataScoreRankItem rankByUserId { get; set; }
    }

    public class DataScoreRankItem
    {
        public int rowNum { get; set; }
        public string headImgUrl { get; set; }
        public string nickName { get; set; }
        public int topScore { get; set; }
    }
}
