using JEngine.Core;
using JEngine.AntiCheat;
using JEngine.Examples;
using JEngine.Net;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using HotUpdateScripts.Project;
using System;
using HotUpdateScripts.Project.Common;

namespace HotUpdateScripts
{

    public class Program
    {

        public void RunGame()
        {

            //本地 热更 交互 消息系统 示例

            //GameObject.Find("INAVNativeB").gameObject.transform.GetChild(0).gameObject.SetActive(true);


            //监听 
            //MsgINAVLocalAndHotupt.Instance.AddEventListener(MsgINAVLocalAndHotuptCode.LocalToHotupt, (o) =>
            //{
            //    debug.Log(o.ToString());
            //});

            ////广播 
            //MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal,"八个雅鹿");






            /*            MsgINAVHotUpt.Instance.AddEventListener(MsgINAVHotUptCode.INAV, (o) =>
                        {

                        });

                        MsgINAVHotUpt.Instance.RemoveEventListener(MsgINAVHotUptCode.INAV, (o) =>
                        {

                        });*/
        }


    }
}
