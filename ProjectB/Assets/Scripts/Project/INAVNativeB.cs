using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JEngine.Core;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

/// <summary>
/// ProjectB 原生交互
/// </summary>
public class INAVNativeB : MonoBehaviour
{

    AndroidJavaObject androidJavaObject;

    public Text Text;
    void appendToText(string line)
    {
        Text.text += line + "\n";
    }


    private void Awake()
    {
        UnityEngine.Object.DontDestroyOnLoad(this.gameObject);

#if UNITY_ANDROID&&!UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivityB");
        androidJavaObject = jc.GetStatic<AndroidJavaObject>("instance");
#endif


        MsgINAVLocalAndHotupt.Instance.AddEventListener(MsgINAVLocalAndHotuptCode.HotuptToLocal, UnityToNative);
    }



    //主动调原生的方法
    public void UnityToNative(object data)
    {
        appendToText("热更给本地的数据（UnityToNative）:" + data);

#if UNITY_ANDROID&&!UNITY_EDITOR
        try
        {
            androidJavaObject.Call(AndroidAPI.UnityToNative, data.ToString());
        }
        catch (Exception e)
        {
            Log.PrintError(e.Message);
        }
#elif UNITY_IOS&&!UNITY_EDITOR
        IOSAPI.UnityToNative(data.ToString());
#endif
    }


    //被原生调的方法
    void NativeToUnity(string data)
    {
        appendToText("原生给本地的数据（NativeToUnity）:" + data);

        MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.LocalToHotupt, data);
    }


}


public class AndroidAPI
{
    public const string UnityToNative = "UnityToNative";
}

public class IOSAPI
{
#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
    public static extern void UnityToNative(string data);
#endif
}




#region 本地<->热更 跨域交互 消息系统

public enum MsgINAVLocalAndHotuptCode
{
    HotuptToLocal,//热更广播 -> 本地监听 
    LocalToHotupt,//本地广播 -> 热更监听 相当于原来_Back
}


public class MsgINAVLocalAndHotupt : MsgBaseAPI<MsgINAVLocalAndHotupt, object, MsgINAVLocalAndHotuptCode>
{
    private static MsgINAVLocalAndHotupt instance;
    public static MsgINAVLocalAndHotupt Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MsgINAVLocalAndHotupt();
            }
            return instance;
        }
    }
}
#endregion