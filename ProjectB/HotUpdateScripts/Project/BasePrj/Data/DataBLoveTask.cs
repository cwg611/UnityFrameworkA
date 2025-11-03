using JEngine.Core;
using System.Collections.Generic;

//任务接口 数据实体
namespace HotUpdateScripts.Project.BasePrj.Data
{

    public class DataBLoveTask
    {
        public List<DataBMainTaskItem> taskList { get; set; }

        public string signInTime { get; set; }

        public int signInDayNum { get; set; }

        public List<DataBMainTaskSignInRewardItem> signInRewardList { get; set; }
    }

    /// <summary>
    /// 任务列表
    /// </summary>
    public class DataBMainTaskItem
    {
        public int id { get; set; }

        public long userId { get; set; }

        public int taskId { get; set; }

        public int taskType { get; set; }

        public string taskTitle { get; set; }

        public string taskDesc { get; set; }

        public string taskRewardDescription { get; set; }

        public string taskGotoShop { get; set; }

        public string taskGotoSubject { get; set; }

        public string taskGotoProduct { get; set; }

        public int taskBrowseDuration { get; set; }

        public int taskTargetProgre { get; set; }

        public int taskRewardId { get; set; }

        public int taskRewardType { get; set; }

        public string taskRewardNumber { get; set; }

        #region Need To Bind Data
        /// <summary>
        /// 任务进度(当前)
        /// </summary>
        private int _taskCurProgress;
        public int taskCurProgress
        {
            get
            {
                return _taskCurProgress;
            }
            set
            {
                _taskCurProgress = value;
                if (BindableCurProgress != null)
                {
                    BindableCurProgress.Value = value;
                }
                else
                {
                    BindableCurProgress = new BindableProperty<int>(value);
                }
            }
        }

        public BindableProperty<int> BindableCurProgress = new BindableProperty<int>(0);


        /// <summary>
        /// 任务状态（当前）
        /// </summary>
        private int _taskStatus;
        public int taskStatus
        {
            get
            {
                return _taskStatus;
            }
            set
            {
                _taskStatus = value;
                if (BindableStatus != null)
                {
                    BindableStatus.Value = value;
                }
                else
                {
                    BindableStatus = new BindableProperty<int>(value);
                }
            }
        }

        public BindableProperty<int> BindableStatus = new BindableProperty<int>(0);
        #endregion

    }

    public class DataBMainTaskSignInRewardItem
    {
        public int id { get; set; }

        public int signInDayNum { get; set; }

        public int signInReward { get; set; }
    }

    //签到时发给服务器
    public class DataBSignInItemReq
    {
        public long userId { get; set; }

        public int taskId { get; set; }

        public int continuousSignInDayNum { get; set; }
    }

    //领取奖励
    public class DataBReceiveReq
    {
        public long userId { get; set; }
        public int taskId { get; set; }
    }

    /// <summary>
    /// 更新任务状态  上传服务器
    /// </summary>
    public class DataBTaskInfoReq
    {
        //用户id
        public long userId { get; set; }

        //任务信息主键ID，标识一个任务
        public int taskId { get; set; }

        //任务完成状态， 0 未完成  1 已完成待领取 2 已完成已领取
        public int taskStatus { get; set; }

        //连续签到天数，断掉从0重新开始计算
        public int continuousSignInDayNum { get; set; }

        //任务当前进度
        public int taskCurProgress { get; set; }
    }
}

