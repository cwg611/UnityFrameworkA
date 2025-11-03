namespace HotUpdateScripts.Project.BasePrj.Data
{
    //原生传给Unity
    public class DataBNativeToUnity
    {
        public string token { get; set; }
        public string type { get; set; }
        public string taskId { get; set; }
        public bool taskState { get; set; } //完成状态
    }

    //Unity传给原生
    public class DataBUnityToNative
    {
        public string type { get; set; }
        public string taskId { get; set; }
        public string subjectId { get; set; }
        public string shopId { get; set; }
        public string productId { get; set; }
        public string duration { get; set; }
        public bool isFinish { get; set; }
    }

    public class DataBLogin
    {
        public string type { get; set; }
    }
}