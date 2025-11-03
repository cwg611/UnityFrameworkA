using System;
using UnityEngine.UI;
using JEngine.Core;
using UnityEngine;
using HotUpdateScripts.Project.Common;

namespace My.UI.Panel
{
    public class UILoadingPanel : BasePanel
    {
        private Image loadImg;
        private RectTransform loadImgRect;
        private GameObject LoadingBg;
        public static Action<bool> SetLoadingActive;

        public override void InitPanel(object o)
        {
            loadImg = GameTools.GetByName(transform, "Loading").GetComponent<Image>();
            loadImgRect = loadImg.GetComponent<RectTransform>();
            LoadingBg = GameTools.GetByName(transform, "LoadingBg");
            SetLoadingActive = setLoaingImg;
            //适配图片
            //ScreenFit.Instance.MatchScreenByImgWidth(LoadingBg.GetComponent<RectTransform>());
        }

        private void Update()
        {
            if (LoadingBg.activeSelf && JResource.LoadSceneProgress >= 0 && JResource.LoadSceneProgress < 1)
            {
                loadImgRect.sizeDelta = new Vector2(JResource.LoadSceneProgress * 540, 56);
            }
            else
            {
                loadImgRect.sizeDelta = new Vector2(540, 56);
            }
        }

        private void setLoaingImg(bool isDisPlay)
        {
            LoadingBg.SetActive(isDisPlay);
        }
    }
}
