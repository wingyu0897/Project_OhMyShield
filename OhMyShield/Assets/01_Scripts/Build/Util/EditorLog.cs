using UnityEngine;

public static class EditorLog
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message)
	{
		Debug.Log(message);
	}

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message)
	{
		Debug.LogWarning(message);
	}

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message)
	{
		Debug.LogError(message);
	}
}
