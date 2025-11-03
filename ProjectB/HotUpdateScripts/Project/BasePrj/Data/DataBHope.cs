using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    public class DataBHope
    {

    }

    /// <summary>
    /// 捐献项目列表
    /// </summary>
    public class DataBBenefitProjectListRes
    {
        public List<PublicBenefitProject> publicBenefitProjectList { get; set; }
    }
    public class PublicBenefitProject
    {
        //公益项目id
        public int projectId { get; set; }
        //公益项目标题
        public String projectTitle { get; set; }
        //今日捐献数量
        public int todayDonateNum { get; set; }
        //今日捐献目标
        public int todayDonateTarget { get; set; }

        //项目累计获得捐献数量
        public int projectTotal { get; set; }
        //项目进展
        public String projectProgress { get; set; }
        //项目背景
        public String projectBackground { get; set; }
        //项目介绍图片地址
        public String projectIntroduceImg { get; set; }
        //项目介绍标题与文字
        public String projectIntroduceWord { get; set; }
        //项目介绍标题
        public String projectIntroduceTitle { get; set; }
        //项目封面
        public string projectCover { get; set; }

    }

    /// <summary>
    /// 项目详情
    /// </summary>
    public class DataBProjectInfoRes
    {
        public string projectTitle { get; set; }
        public int todayDonateNum { get; set; }
        public int todayDonateTotal { get; set; }
        public int donateTotal { get; set; }
        public int userDonateSumForProject { get; set; } //我的捐献数量
        public List<UserDonateRecord> lastTenDonateRecordList { get; set; } //最近捐献的用户
        public string projectIntroduceWord { get; set; }
        public string projectIntroduceImg { get; set; }
        public string projectIntroduceTitle { get; set; }   //@  的位置放标题   ^ 位置放图片
        public List<ProjectProgress> projectProgressList { get; set; }  //项目进展
        public string projectCover { get; set; }               //项目封面
        public string projectVideo { get; set; }  //视频链接
        public string projectDonateFinishPicture { get; set; } //该项目捐献成功后的图片地址
    }

    public class UserDonateRecord
    {
        //用户游戏信息主键id
        public int id { get; set; }
        //用户Id
        public long userId { get; set; }
        //公益项目id
        public int projectId { get; set; }
        //公益项目标题
        public String projectTitle { get; set; }
        //捐献爱心数量
        public int donateNum { get; set; }
        //捐献时间
        public String donateTime { get; set; }
        //获取爱心币数量
        public int getLoveMoneyNum { get; set; }
        public string nickName { get; set; }
        public string headImgUrl { get; set; }
    }


    public class ProjectProgress
    {
        //公益项目id
        public int projectId { get; set; }
        //公益项目进展标题
        public String projectProgressTitle { get; set; }
        //公益项目进展文字
        public String projectProgressWord { get; set; }
        //公益项目进展图片
        public String projectProgressImg { get; set; }
        //公益项目进展变更时间
        public String projectProgressTime { get; set; }

    }


    public class DataBProjectInfoReq
    {
        public int projectId { get; set; }
        public long userId { get; set; }
    }

    /// <summary>
    /// 捐献
    /// </summary>
    public class DataBDonateReq
    {
        public int projectId { get; set; }
        public long userId { get; set; }
        public int donateNum { get; set; }
    }

    /// <summary>
    /// 获取 用户捐献后，公益项目的最新数据
    /// </summary>
    public class NewProjectDonateInfo
    {
        //1.获取  用户对某个项目第几次捐献（捐献过后这是第几次）
        public int userDonateSumForProject { get; set; }
        //2.当前项目已获得爱心数
        public int projectTotal { get; set; }
        //3.该项目帮助对象
        public string projectHelpObject { get; set; }
        //4.该项目被捐献次数
        public int donateNum { get; set; }
        //项目标题
        public string projectTitle { get; set; }
        //项目图片
        public string projectIntroduceImg { get; set; }
        public string projectDonateFinishPicture { get; set; } //该项目捐献成功后的图片地址

    }
}
