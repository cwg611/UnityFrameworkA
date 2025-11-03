using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    //-----时间戳都用long------//
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

        public long serverTimeStamp = 0; //服务器时间戳

        private float timePoint = 0;
        private float localTimeOne = 0; //获取服务器前的时间
        private float localTimeTwo = 0; //获取服务器后的时间
        private float timer = 1;

        void Update()
        {
            timer -= Time.deltaTime / Time.timeScale;
            if (timer <= 0)
            {
                serverTimeStamp += 1;
                timePoint += 1;
                timer = 1;
            }
            if (timePoint == 0) 
                GetServerTime();
            if (timePoint > 30) 
                timePoint = 0;
        }

        //获取服务器的时间
        public float GetServerTime()
        {
            //请求时
            localTimeOne = Time.time;

            //请求后
            localTimeTwo = Time.time;
            return serverTimeStamp + (localTimeTwo - localTimeOne) / 2;
        }
    }
}
