using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace My.UI
{
    //
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
        private GameObject uiRoot;

        public GameObject UIRoot
        {
            get
            {
                if (uiRoot == null)
                {
                    //GameObject g = JResource.LoadRes<GameObject>("UI/Canvas.prefab", JResource.MatchMode.Prefab);
                    //uiRoot = GameObject.Instantiate(g);
                    uiRoot = GameObject.Find("Canvas");
                    debug.Log_Blue("可以找到Canvas");
                }
                return uiRoot;
            }
        }


        public void Init()
        {
            GameObject.DontDestroyOnLoad(this.UIRoot);
        }

        public BasePanel Open(string panelName, object o = null, UILayer layer = UILayer.Layer3)
        {
            debug.Log_yellow("Open Panel: " + panelName);
            if (uiDic.ContainsKey(panelName))
            {
                uiDic[panelName].OpenPanel(o, layer);
                return uiDic[panelName];
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


                var _uiPanel = GameObject.Instantiate(JResource.LoadRes<GameObject>("UI/" + new StringBuilder(panelName).Append(".prefab").ToString(), JResource.MatchMode.Prefab)).transform;
                _uiPanel.name = panelName;

                var panel = _uiPanel.gameObject.AddComponent(Type.GetType("My.UI.Panel." + panelName)) as BasePanel;
                panel.InitPanel(o);//!!!
                panel.OpenPanel(o, layer);

                uiDic[panelName] = panel;
                return panel;
            }
        }

        public void Close(string panelName, bool destroy = true)
        {
            if (uiDic.ContainsKey(panelName))
            {
                if (destroy)//销毁
                {
                    uiDic[panelName].ReleasePanel();//!!!
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
                //debug.Log_Red("UI:Close error:" + panelName);
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
                //Debug.LogError("UI:Get error:" + panelName);
                return null;
            }
        }

        public bool GetPanelIsExit(string panelName)
        {
            bool isExit = false;
            if (uiDic.ContainsKey(panelName))
            {
                isExit = true;
            }
            return isExit;
        }


        public Action OnCloseRedPackagePanel;
        //红包方案1
        float[] resultArrayOne = new float[] { 0.1f, 0.2f, 0.5f, 0.8f, 1f, 1.5f, 2f };
        int[] ratioArrayOne = new int[] { 300, 300, 250, 100, 30, 15, 5 };
        //红包方案2
        float[] resultArrayTwo = new float[] { 0.2f, 0.3f, 0.5f, 0.8f, 1f, 1.5f, 2f, 3f, 5, 8 };
        int[] ratioArrayTwo = new int[] { 200, 300, 350, 100, 25, 15, 4, 3, 2, 1 };
        public void OpenRedPackageActivityPanel(string uiPanel, bool isActivity = true)
        {
            OnCloseRedPackagePanel = () => { Open(uiPanel, null, UILayer.Layer3); };

            int randomA = UnityEngine.Random.Range(0, 100);
            int randomB = UnityEngine.Random.Range(1, 6);
            debug.Log_Blue(randomA);
            debug.Log_Blue(randomB);
            if (randomA < randomB && DateTime.Now.Year == 2022 && DateTime.Now.Month == 11)
            {
                var serverTime = GameTools.TmGetDateTime(TimeMgr.Instance.serverTime);
                switch (serverTime.Day)
                {
                    //方案一
                    case 2:
                    case 8:
                        float result1 = resultArrayOne[GameTools.ProbabilityRandomRumber(ratioArrayOne)];
                        NetMgr.Instance.C2S_User_Get_Red_Packet(result1.ToString(), serverTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    //方案二
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 10:
                        float result2 = resultArrayTwo[GameTools.ProbabilityRandomRumber(ratioArrayTwo)];
                        NetMgr.Instance.C2S_User_Get_Red_Packet(result2.ToString(), serverTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    default:
                        OnCloseRedPackagePanel?.Invoke();
                        OnCloseRedPackagePanel = null;
                        break;
                }
            }
            else
            {
                OnCloseRedPackagePanel?.Invoke();
                OnCloseRedPackagePanel = null;
            }
        }

        public void GetRedPackageCallBack()
        {
            if (DataMgr.Instance.DataRedPackage != null && DataMgr.Instance.DataRedPackage.success)
            {
                UIMgr.Instance.Open(UIPath.UIBRedPackagePanel);
            }
            else
            {
                OnCloseRedPackagePanel?.Invoke();
                OnCloseRedPackagePanel = null;
            }
        }
    }
}

