using System;
using System.Collections.Generic;
using UnityEngine;


namespace My.Msg
{
    /* 这个消息系统 热更工程内用 可热更
     * 另个消息系统 热更监听本地用 不可热更（解决了泛型跨域继承（适配器）但ILR不能类型转换） 
     */
    public static class MsgCenter
    {
        static Dictionary<MsgCode, Action<object>> msgDic = new Dictionary<MsgCode, Action<object>>();
        static Dictionary<MsgCode, Func<object, object>> funcDic = new Dictionary<MsgCode, Func<object, object>>();

        public static void RegisterMsg(this object o, MsgCode msg, Action<object> action)
        {
            if (msgDic.ContainsKey(msg))
            {
                msgDic[msg] += action;
            }
            else
            {
                msgDic[msg] = action;

            }
        }

        public static void UnRegisterMsg(this object o, MsgCode msg, Action<object> action)
        {
            if (msgDic.ContainsKey(msg))
            {
                msgDic[msg] -= action;
                if (msgDic[msg] == null)
                {
                    msgDic.Remove(msg);
                }
            }
        }

        public static void Call(this object o, MsgCode msg, object data)
        {
            if (msgDic.ContainsKey(msg))
            {
                msgDic[msg](data);
            }
            else
            {
                Debug.Log("Msg:<color=red>[Error Try Catch] CallFunction Failure </color>:" + msg);
                //throw new Exception();
            }
        }

        public static object CallF(this object o, MsgCode msg, object data)
        {
            if (funcDic.ContainsKey(msg))
            {
                return funcDic[msg](data);
            }
            else
            {
                Debug.Log("Msg:<color=red>[Error] CallFunction Failure </color>:" + msg);
            }
            return null;
        }

        public static void UnRegisterFunc(this object o, MsgCode msg, Func<object, object> action)
        {
            if (funcDic.ContainsKey(msg))
            {
                funcDic[msg] -= action;
                if (funcDic[msg] == null)
                {
                    funcDic.Remove(msg);
                }
            }
        }

        public static void RegisterFunc(this object o, MsgCode msg, Func<object, object> action)
        {
            if (funcDic.ContainsKey(msg))
            {
                funcDic[msg] += action;
            }
            else
            {
                funcDic[msg] = action;
            }
        }
    }
}