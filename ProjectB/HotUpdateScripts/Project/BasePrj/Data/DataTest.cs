using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.Project.BasePrj.Data
{

    /* 测试示例数据 */

    [System.Serializable]
    [global::ProtoBuf.ProtoContract()]
    public class DataTest
    {

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                if (BindableId != null)
                {
                    BindableId.Value = value;
                }
                else
                {
                    BindableId = new BindableProperty<int>(value);
                }
            }
        }

        public BindableProperty<int> BindableId = new BindableProperty<int>(0);


        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                if (BindableAge != null)
                {
                    BindableAge.Value = value;
                }
                else
                {
                    BindableAge = new BindableProperty<int>(value);
                }
            }
        }
        public BindableProperty<int> BindableAge = new BindableProperty<int>(0);

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

    }

}












/*[System.Serializable]
[global::ProtoBuf.ProtoContract()]
*//* 测试数据 *//*
public class DataTestContent
{
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sex { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string head { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<int> contactIdList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> pictureList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    private string date1;
    public string date
    {
        get
        {
            return date1;
        }
        set
        {
            date1 = value;
        }
    }
}

public class DataTest
{
    /// <summary>
    /// 
    /// </summary>
    public DataTestContent User { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool ok { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string msg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    //public string sql:generate|cache|execute|maxExecute { get; set; }
    /// <summary>
    /// 
    /// </summary>
    //public string depth:count|max { get; set; }
    /// <summary>
    /// 
    /// </summary>
    //public string time:start|duration|end { get; set; }
}*/