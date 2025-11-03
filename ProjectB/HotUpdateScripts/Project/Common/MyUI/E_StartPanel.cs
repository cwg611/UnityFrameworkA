using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JEngine.Core;
using My.Msg;

namespace My.UI.Panel
{
    public class E_StartPanel : BasePanel
    {
        public Button startBtn;


        public override void InitPanel(object o)
        {
            startBtn = transform.GetComponentInChildren<Button>();
            startBtn.onClick.AddListener(OnStartBtnClick);
        }
        
        void OnStartBtnClick()
        {
            //UIMgr.Instance.Close("E_StartPanel", false);
            UIMgr.Instance.Open("E_GamePanel", UILayer.Layer3);

            this.Call(MsgCode.Test,"嘻嘻嘻");
        }

        public override void ReleasePanel()
        {
            
        }

    }
}

