using System;
using UnityEngine.UI;
using UnityEngine;
using HotUpdateScripts.Project.Common;
using System.Collections;
using DG.Tweening;

namespace My.UI.Panel
{
    /*
     对话框（弹出自动消失）
     */
    public class CommonTipPanel : BasePanel
    {
        private RectTransform tipBg, tip_rect;
        private Text tip;
        public static Action<string> SetTip;
        private CanvasGroup canvasGroup;
        public static Action<string, string, string, string, Action> Act_setTipWindow;
        public static Action<string, string, string> Act_setWindowTip;
        private Coroutine coroutine_setHide;

        public override void InitPanel(object o)
        {
            tipBg = GameTools.GetByName(transform, "tipBg").GetComponent<RectTransform>();
            tip = GameTools.GetByName(transform, "tip").GetComponent<Text>();
            tip_rect = tip.GetComponent<RectTransform>();
            SetTip = setTipView;
            canvasGroup = tipBg.GetComponent<CanvasGroup>();
        }

        private void setTipView(string str)
        {
            canvasGroup.alpha = 0;
            tipBg.gameObject.SetActive(true);
            tip.text = str;
            GameTools.Instance.AdjustWidthSizeByText(tip_rect, tipBg, 80);

            canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutSine);
            if (coroutine_setHide != null)
            {
                StopCoroutine(coroutine_setHide);
            }
            coroutine_setHide = StartCoroutine(setHide());
        }

        IEnumerator setHide()
        {
            yield return new WaitForSeconds(2f);
            canvasGroup.DOFade(0f, 3f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(2f);
            tip.text = "";
            tipBg.gameObject.SetActive(false);
            UIMgr.Instance.Close(UIPath.CommonTipPanel);
        }
    }
}
