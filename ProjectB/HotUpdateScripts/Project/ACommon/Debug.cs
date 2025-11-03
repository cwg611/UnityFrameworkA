using System;
using UnityEngine;


public class debug
{
    public static bool EnableLog = true;
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.DrawLine(start, end, color, duration);
        }
    }
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.DrawLine(start, end);
        }
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.DrawLine(start, end, color);
        }
    }
    public static void Log(object message, UnityEngine.Object context)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log(message, context);
        }
    }
    public static void Log(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    //-------------带颜色----------------
    public static void Log_Red(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log("<Color=red>" + message + "</Color>");
        }
    }

    public static void Log_Blue(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log("<Color=blue>" + message + "</Color>");
        }
    }

    public static void Log_purple(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log("<Color=#a900f7>" + message + "</Color>");
        }
    }

    public static void Log_yellow(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.Log("<Color=#fcff00>" + message + "</Color>");
        }
    }

    //---------------------
    public static void LogAssertion(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogAssertion(message);
        }
    }
    public static void LogAssertionFormat(string format, params object[] args)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogAssertionFormat(format, args);
        }
    }
    public static void LogError(object message, UnityEngine.Object context)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogError(message, context);
        }
    }
    public static void LogError(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
    public static void LogErrorFormat(string format, params object[] args)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }
    }
    public static void LogException(Exception exception)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogException(exception);
        }
    }
    public static void LogFormat(string format, params object[] args)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogFormat(format, args);
        }
    }
    public static void LogWarning(object message)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogWarning(message);
        }
    }
    public static void LogWarning(object message, UnityEngine.Object context)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }
    }
    public static void LogWarningFormat(string format, params object[] args)
    {
        if (EnableLog)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }
    }
}
