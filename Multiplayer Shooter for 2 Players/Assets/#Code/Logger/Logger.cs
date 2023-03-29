using UnityEngine;

public static class Logger
{
    public static void LogMessage(object message, Object sender) => Debug.Log(message, sender);
    public static void LogWarning(object message, Object sender) => Debug.LogWarning(message, sender);
    public static void LogError(object message, Object sender) => Debug.LogError(message, sender);
}
