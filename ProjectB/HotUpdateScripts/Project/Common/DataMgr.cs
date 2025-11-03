using HotUpdateScripts.Project.GameA.Data;
using JEngine.Core;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 1.实例化数据 DataProxy
    /// 2.处理NetMgr接收到的数据(主要是监听数据) 反序列化
    /// </summary>
    public class DataMgr : Singleton<DataMgr>
    {
        public DataTest dataPerson = new DataTest();

        public void SCTestReceive(string sdata)
        {
            DataTest data = JsonMapper.ToObject<DataTest>(sdata);

            /* 注意： JUI绑定的数据在此逐个赋值 */
            dataPerson.Id = data.Id;
            dataPerson.Name = data.Name;
            dataPerson.Age = data.Age;

        }
    }

}
