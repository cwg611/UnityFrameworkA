using System;
using System.Collections.Generic;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public enum PopWinType
    {
        Common = 0,
        ExchangeRealInfo = 1,
        RequestLove = 2,
        IntimacyPop = 3,

        WithoutIcon = 10,//无图标弹窗
    }
    /// <summary>
    /// 弹窗公用页面 UILayer=5，destroy=false
    /// </summary>
    public class CommonPopWinPanel : BasePanel
    {
        private static CommonPopWinPanel instance;

        private ShowPop showPopOne;
        private ShowPop showPopTwo;
        //private ShowPop showPopThree;


        public static CommonPopWinPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (CommonPopWinPanel)UIMgr.Instance.Open(typeof(CommonPopWinPanel).Name, layer: UILayer.Layer5);
                }
                return instance;
            }
        }

        public override void InitPanel(object o)
        {
            showPopOne = GameTools.GetByName(transform, "ShowPopOne").AddComponent<ShowPop>();
            showPopOne.InitView();
            showPopTwo = GameTools.GetByName(transform, "ShowPopTwo").AddComponent<ShowPop>();
            showPopTwo.InitView();
        }


        /// <summary>
        /// 双按钮提示框
        /// </summary>
        public void ShowPopOne(string message, string title = "", string ok = "确认", string cancle = "取消",
            Action okAction = null, Action cancleAction = null, bool open = false, string messageColor = "FFFFFF", PopWinType popWinType = PopWinType.WithoutIcon)
        {
            if (showPopOne != null)
            {
                showPopOne.gameObject.SetActive(true);
            }
            showPopOne.ShowMessage(message, title,ok,cancle, okAction: okAction, cancleAction: cancleAction, open: open, popWinType: popWinType);
        }

        /// <summary>
        /// 单按钮提示框
        /// </summary>
        public void ShowPopTwo(string message, string title = "", string ok = "我知道了",
            Action okAction = null, bool open = false, string messageColor = "FFFFFF", PopWinType popWinType = PopWinType.WithoutIcon)
        {
            if (showPopTwo != null)
            {
                showPopTwo.gameObject.SetActive(true);
            }
            showPopTwo.ShowMessage(message, title,ok, okAction: okAction, open: open, popWinType: popWinType);
        }
    }
}
