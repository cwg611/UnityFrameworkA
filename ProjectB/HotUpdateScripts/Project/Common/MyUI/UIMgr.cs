using JEngine.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace My.UI
{
    public enum UILayer
    {
        Layer1 = 1, Layer2, Layer3, Layer4, Layer5
    }

    /// <summary>
    /// UI管理类
    /// </summary>
    public class UIMgr : Singleton<UIMgr>
    {
        //保存所有UIPanel
        public Dictionary<string, BasePanel> uiDic = new Dictionary<string, BasePanel>();

        //UI的根
        public GameObject uiRoot;

        public GameObject UIRoot
        {
            get
            {
                if (uiRoot == null)
                {
                    /*uiRoot = ResMgr.Instance.InstanceGameObject("UI/" + "Canvas");
                    uiRoot.name = "Canvas";*/

                    GameObject g = JResource.LoadRes<GameObject>("Canvas.prefab", JResource.MatchMode.Prefab);
                    uiRoot = GameObject.Instantiate(g);
                }
                return uiRoot;
            }
        }


        public void Init()
        {
            GameObject.DontDestroyOnLoad(UIMgr.Instance.UIRoot);
        }

        public void Open(string panelName, object o = null, UILayer layer = UILayer.Layer3)
        {
            if (uiDic.ContainsKey(panelName))
            {
                uiDic[panelName].OpenPanel(o, layer);
            }
            else
            {

                /*var _uiPanel = (ResMgr.Instance.InstanceGameObject("UI/" + panelName) as GameObject).transform;
                _uiPanel.name = panelName;
                var panel = _uiPanel.GetComponent(Type.GetType("My.UI.Panel." + panelName)) as BaseUI;
                if (panel == null)
                    panel = _uiPanel.gameObject.AddComponent(Type.GetType("My.UI.Panel." + panelName)) as BaseUI;
                panel.InitPanel(o);
                panel.Open(o, layer);

                uiDic[panelName] = panel;*/


                var _uiPanel = GameObject.Instantiate( JResource.LoadRes<GameObject>(new StringBuilder(panelName).Append(".prefab").ToString(), JResource.MatchMode.Prefab)).transform;
                _uiPanel.name = panelName;

                var panel = _uiPanel.gameObject.AddComponent(Type.GetType("My.UI.Panel." + panelName)) as BasePanel;
                panel.InitPanel(o);//!!!
                panel.OpenPanel(o, layer);

                uiDic[panelName] = panel;
            }
        }

        public void Close(string panelName, bool destroy = true)
        {
            if (uiDic.ContainsKey(panelName))
            {
                uiDic[panelName].ReleasePanel();//!!!
                if (destroy)//销毁
                {
                    if (uiDic[panelName].ClosePanel(true))
                    {
                        uiDic.Remove(panelName);
                    }
                }
                else//隐藏
                {
                    uiDic[panelName].ClosePanel(false);
                }
            }
            else
            {
                Debug.LogError("UI:Close error:" + panelName);
            }
        }

        public void CloseAll(bool destroy = true)
        {
            foreach (var ui in uiDic)
            {
                ui.Value.ClosePanel(destroy);
            }
            if (destroy)
                uiDic.Clear();
        }

        public void AddChild()
        {

        }


        public BasePanel Get(string panelName)
        {
            if (uiDic.ContainsKey(panelName))
            {
                return uiDic[panelName];
            }
            else
            {
                Debug.LogError("UI:Get error:" + panelName);
                return null;
            }
        }


    }
}

