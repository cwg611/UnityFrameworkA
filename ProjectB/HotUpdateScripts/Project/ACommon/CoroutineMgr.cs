using System.Collections;
using UnityEngine;


namespace HotUpdateScripts.Project.ACommon
{
    class CoroutineMgr : MonoBehaviour
    {
        public static CoroutineMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("CoroutineMgr").AddComponent<CoroutineMgr>();
                }
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }
        private static CoroutineMgr _instance;


        //在其他脚本把协程传进来开启
        //开启协程
        public Coroutine Coroutine_Start(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        //停止某个协程
        public void Coroutine_Stop(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        //停止所有协程
        public void Coroutine_StopAll()
        {
            StopAllCoroutines();
        }
    }
}
