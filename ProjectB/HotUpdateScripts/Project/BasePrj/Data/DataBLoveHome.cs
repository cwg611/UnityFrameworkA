using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.BasePrj.Data
{
   public class DataBLoveHome
    {
    }
    //充能时间
    public class DataBChargeTime
    {
        public int curEnergy { get; set; }
        public string lastChargeTime { get; set; }   //上次充能时间
        public string lastChargePastTime { get; set; } //结束时间
    }
}
