using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using My.UI;
using JEngine.Core;
using System.Text;

namespace My.UI
{

    /// <summary>
    /// 启动脚本
    /// </summary>
    public class E_UIInit : MonoBehaviour
    {

        private void Awake()
        {
            //UI面板 初始化
            //UIMgr.Instance.Init();
        }

        private void Start()
        {
            UIMgr.Instance.Open("E_StartPanel", null, UILayer.Layer3);            
        }

    }

}

