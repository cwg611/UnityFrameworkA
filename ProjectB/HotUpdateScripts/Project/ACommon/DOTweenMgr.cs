using JEngine.Core;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.Common
{
    public class DOTweenMgr : Singleton<DOTweenMgr>
    {
        /// <summary>
        /// 物体位移
        /// </summary>
        /// <param name="obj">物体GameObject</param>
        /// <param name="startPos">初始位置</param>
        /// <param name="endPos">目标位置</param>
        /// <param name="time">移动时间</param>
        /// <param name="func">完成后的回调</param>
        public void MovePos(GameObject obj, Vector3 startPos, Vector3 endPos, float time, Action func = null)
        {
            obj.transform.GetComponent<RectTransform>().localPosition = startPos;
            Tweener tweener = obj.transform.GetComponent<RectTransform>().DOLocalMove(endPos, time);
            tweener.Restart();
            if (func != null)
            {
                tweener.OnComplete(() => func());
            }
        }
        /// <summary>
        /// 透明度动画
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="alpha"></param>
        /// <param name="timer"></param>
        public void DoFade(GameObject obj, float alpha, float timer)
        {
            Image image = obj.transform.GetComponent<Image>();
            image.DOFade(alpha, timer);

        }

        public void DoFadeQ(GameObject obj, float alpha, float timer)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            float _alpha = canvasGroup.alpha;
            // 创建一个 Tweener 对象， 另 number的值在 5 秒内变化到 100
            Tween t = DOTween.To(() => _alpha, x => _alpha = x, alpha, timer);
            // 给执行 t 变化时，每帧回调一次 UpdateTween 方法
            t.OnUpdate(() => { canvasGroup.alpha = _alpha; });
        }

        /// <summary>
        /// 物体缩放
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="m_scale"></param>
        /// <param name="timer"></param>
        /// <param name="func"></param>
        public void DoScale(GameObject obj, float m_scale, float timer, Action func = null)
        {
            Sequence mySequence = DOTween.Sequence();
            Tweener doscale1 = obj.transform.DOScale(new Vector3(m_scale, m_scale, m_scale), timer).SetEase(Ease.InOutSine);
            mySequence.Append(doscale1);
            mySequence.OnComplete(() =>
            {
                obj.transform.DOScale(new Vector3(1, 1, 1), 0.05f);
            });
        }

        public void DoScale(GameObject obj, Vector3 startScale, Vector3 endScale, float time, Action func = null)
        {
            obj.transform.GetComponent<RectTransform>().localScale = startScale;
            Tweener tweener = obj.transform.GetComponent<RectTransform>().DOScale(endScale, time);
            tweener.Restart();
            if (func != null)
            {
                tweener.OnComplete(() => func());
            }
        }

        /// <summary>
        /// 按钮动画
        /// </summary>
        /// <param name="obj"></param>
        public void BtnClickAni(GameObject obj, float timer)
        {
            Sequence mySequence = DOTween.Sequence();
            Tweener doscale1 = obj.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), timer).SetEase(Ease.InOutSine);
            Tweener doscale12 = obj.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), timer).SetEase(Ease.InOutSine);
            mySequence.Append(doscale1);
            //mySequence.AppendInterval(.02f);
            mySequence.Append(doscale12);
            mySequence.OnComplete(() =>
            {
                obj.transform.DOScale(new Vector3(1, 1, 1), 0.05f);
            });
        }


        /// <summary>
        /// 打开弹窗
        /// </summary>
        /// <param name="obj"></param>
        public void OpenWindowScale(GameObject obj, float timer)
        {
            obj.transform.localScale = new Vector3(0.3f, .3f, .3f);
            Sequence mySequence = DOTween.Sequence();
            Tweener doscale1 = obj.transform.DOScale(new Vector3(1f, 1f, 1f), timer);
            mySequence.Append(doscale1);
        }

        /// <summary>
        /// 关闭弹窗
        /// </summary>
        /// <param name="obj"></param>
        public void CloseWindowScale(GameObject obj, float timer, Action act)
        {
            Sequence mySequence = DOTween.Sequence();
            Tweener doscale1 = obj.transform.DOScale(new Vector3(0, 0, 0), timer);
            mySequence.Append(doscale1);
            mySequence.OnComplete(() =>
            {
                if (act != null)
                {
                    act();
                }
            });
        }

    }
}
