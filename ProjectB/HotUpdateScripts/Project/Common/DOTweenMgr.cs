using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

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

    }
}
