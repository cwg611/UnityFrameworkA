using My.Msg;
using System;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    public class TimeMgr : MonoBehaviour
    {
        public static TimeMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("TimeMgr").AddComponent<TimeMgr>();
                }
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }
        private static TimeMgr _instance;

        public int serverTime; //服务器时间戳
        private float timePoint = 0;
        private float localTimeOne = 0; //获取服务器前的时间
        private float localTimeTwo = 0; //获取服务器后的时间
        private float timer = 1;
        public bool isSuccess = false; //第一次是否获取服务器时间成功
        public bool isUpdateSuccess = false; //刷新服务器时间

        void Awake()
        {
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_GetServerTime, S2C_Game_GetServerTimeCallBack);
        }

        void Update()
        {
            timer -= Time.deltaTime / Time.timeScale;
            if (timer <= 0)
            {
                serverTime += 1;
                timePoint += 1;
                timer = 1;
            }
            if (timePoint == 30)
            {
                GetServerTime();
                timePoint = 0;
            }
        }

        void Destroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_GetServerTime, S2C_Game_GetServerTimeCallBack);
        }

        void S2C_Game_GetServerTimeCallBack(object o)
        {
            localTimeTwo = Time.time;
            serverTime = (int)(localTimeTwo - localTimeOne) / 2 + (int)GameTools.TmGetTimeStamp(DateTime.Parse(DataMgr.Instance.dataServerTime.serverTime));
            isSuccess = true;
            isUpdateSuccess = true;
        }

        //获取服务器的时间
        public void GetServerTime()
        {
            isUpdateSuccess = false;
            //请求时
            localTimeOne = Time.time;
            NetMgr.Instance.C2S_Game_GetServerTime();
        }


        public int getServerTime()
        {
            return serverTime;
        }

        //游戏切换回前台时 
        public void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Debug.Log("从后台唤醒游戏");
                if (NetMgr.Instance.NoNetwork())
                {
                    NetMgr.Instance.ReConnect();
                }
            }
            else
            {


            }
        }
    }
}
