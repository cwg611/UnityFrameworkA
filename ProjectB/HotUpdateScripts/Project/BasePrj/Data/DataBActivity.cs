using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    /// <summary>
    /// 活动排行榜
    /// </summary>
    public class DataBActivity
    {
        public List<ActivityReward> friendList { get; set; }
        public List<ActivityReward> honorList { get; set; }
        public List<ActivityReward> composeGameList { get; set; }
        public List<ActivityReward> jumpGameList { get; set; }
    }
    /// <summary>
    /// 排行奖励
    /// </summary>
    public class ActivityReward
    {
        //用户昵称
        public String nickName { get; set; }
        //奖品名称
        public String productName { get; set; }
        //奖项名称
        public String prizeName { get; set; }
        //奖项公布时间
        public String activityTime { get; set; }
        //分组名称
        public String groupName { get; set; }
    }
}
