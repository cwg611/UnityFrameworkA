using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestHTTP.WebSocket;
using UnityEngine;


namespace HotUpdateScripts.Project.Common
{

    /// <summary>
    /// Socket消息码 （客户端 服务器）
    /// </summary>
    public class SocketCode
    {
        //Client -> Server
        public static string cs_TestSend = "cs_Test";

        //Server -> Client
        public static string sc_TestReceive = "sc_Test";
    }

    //定义请求接口方法 回调数据供DataMgr使用
    public class NetMgr : Singleton<NetMgr>
    {
        public static NetMgr Instance { get { return Singleton<NetMgr>.Instance; } }

        string address = "ws://127.0.0.1:7799";
        WebSocket webSocket;//open send close


        public void Init()
        {
            if (webSocket == null)
            {
                webSocket = new WebSocket(new Uri(address));

                // Subscribe to the WS events
                webSocket.OnOpen += OnOpen;
                webSocket.OnMessage += OnMessageRecv;
                webSocket.OnBinary += OnBinaryRecv;
                webSocket.OnClosed += OnClosed;
                webSocket.OnError += OnError;

                // Start connecting to the server
                webSocket.Open();
            }
        }

        #region ws回调
        void OnOpen(WebSocket ws)
        {
            Debug.Log("OnOpen:");
        }

        void OnMessageRecv(WebSocket ws, string message)
        {
            Debug.LogFormat("OnMessageRecv: msg={0}", message);
        }

        void OnBinaryRecv(WebSocket ws, byte[] data)
        {
            Debug.LogFormat("OnBinaryRecv: len={0}", data.Length);//Debug.Log(System.Text.Encoding.ASCII.GetString(data));
        }

        void OnClosed(WebSocket ws, UInt16 code, string message)
        {
            Debug.LogFormat("OnClosed: code={0}, msg={1}", code, message);
        }

        void OnError(WebSocket ws, string ex)
        {
            Debug.LogFormat("OnError: ex={0}",ex);
        }
        #endregion



    }

}
