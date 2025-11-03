using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 封装一些工具 不同模块以XX打头区分
    /// </summary>
    public class GameTools : Singleton<GameTools>
    {

        #region UITools

        //立即强制重建布局 解决 ContentSizeFitter 无法实时刷新问题

        /// <summary>
        /// 刷新布局 单个
        /// </summary>
        /// <param name="rect"></param>
        public void UIRebuildLayoutImdSig(RectTransform rect)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }

        /// <summary>
        /// 刷新布局 /* 父物体所有包含ContentSizeFitter组件的子物体 */
        /// </summary>
        /// <param name="trans"> 父物体 </param>
        public void UIRebuildLayoutImdMut(Transform trans)
        {
            ContentSizeFitter[] SSFArr = trans.GetComponentsInChildren<ContentSizeFitter>();
            for (int i = SSFArr.Length - 1; i > 0; i--)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(SSFArr[i].GetComponent<RectTransform>());
            }
        }

        #endregion

        #region TimeTools

        // 获取时间戳Timestamp  

        /// <summary>
        /// 根据DateTime 获取时间戳Timestamp 
        /// </summary>
        /// <param name="dt"> DateTime </param>
        /// <returns></returns>
        public static long TmGetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = (int)(dt - dateStart).TotalSeconds;
            return timeStamp;
        }

        /// <summary>
        /// 根据DateTime 获取时间戳Timestamp 
        /// </summary>
        /// <param name="dt"> DateTime </param>
        /// <returns></returns>
        public static long TmGetTimeStamp2(DateTime dt)
        {
            long timeStamp = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return timeStamp;
        }

        //时间戳Timestamp转换成日期
        public static DateTime TmGetDateTime(int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }

        // 时间戳Timestamp转换成日期
        public static DateTime TmGetDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return dtStart.Add(toNow);
        }

        //将秒数转化为时分秒  00:00:00
        public static string TmTimeStampToTime(int duration)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;

            if (duration > 60)
            {
                minute = duration / 60;
                second = duration % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            /*if (hour > 0)
            {
                return hour + "小时" + minute + "分";
            }
            else
            {
                return string.Format("{0:D2}分{1:D2}秒", minute, second);
            }*/
            return String.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
        }

        #endregion

    }
}
