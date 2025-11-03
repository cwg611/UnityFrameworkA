using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using System.Threading;

namespace HotUpdateScripts.Project.ACommon
{

    /// <summary>
    /// WebRequestHelper 封装的功能
    /// </summary>
    public class WebRequestHelper
    {

        /// <summary>
        /// 请求图片
        /// </summary>
        /// <param name="url">图片地址,like 'http://xxx.png '</param>
        /// <param name="action">请求发起后处理回调结果的委托,处理请求结果的图片</param>
        /// <returns></returns>
        public static void GetTexture(string url, Action<Texture2D> actionResult)
        {
            //CoroutineMgr.Start(_GetTexture(url, actionResult));

            CoroutineMgr.Instance.StartCoroutine(_GetTexture(url, actionResult));
        }

        /// <summary>
        /// 请求图片
        /// </summary>
        /// <param name="url">图片地址,like 'http://xxx.png '</param>
        /// <param name="action">请求发起后处理回调结果的委托,处理请求结果的图片</param>
        /// <returns></returns>
        static IEnumerator _GetTexture(string url, Action<Texture2D> actionResult)
        {
            UnityWebRequest uwr = new UnityWebRequest(url);
            DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
            uwr.downloadHandler = downloadTexture;
            yield return uwr.SendWebRequest();
            Texture2D t = null;
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                t = downloadTexture.texture;
                //DTX压缩  只支持PC端，移动端只能减小图片大小
                /*
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    t.Compress(false);
                }
                debug.Log_Red("修改后运行时占用内存：" + (UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(t) / 1024) + "KB\n图片分辨率" + t.width + "x" + t.height);
                */
                uwr.Dispose();
            }
            if (actionResult != null)
            {
                actionResult(t);
            }
        }

        internal static UnityWebRequest GetTexture(string url)
        {
            throw new NotImplementedException();
        }
    }

}