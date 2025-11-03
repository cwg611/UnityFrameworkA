using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My.Msg;
using JEngine.Core;

namespace My.UI.Panel
{
    public class E_GamePanel : BasePanel
    {
        public Button backBtn;
        public Text text;
        int n = 1;

        public override void InitPanel(object o)
        {
            text = transform.GetComponentInChildren<Text>();
            
            backBtn = transform.GetComponentInChildren<Button>();
            backBtn.onClick.AddListener(()=> {

                text.text = n++.ToString();
                UIMgr.Instance.Close(this.name, false);
                UIMgr.Instance.Open("E_StartPanel");
  
            });

            Debug.Log("Game 初始");


            this.RegisterMsg(MsgCode.Test, (s) => {

                Log.Print("game:"+s);
            });
        }
        public override void ReleasePanel()
        {
            Debug.Log("Game 释放");            
        }


    }
}

