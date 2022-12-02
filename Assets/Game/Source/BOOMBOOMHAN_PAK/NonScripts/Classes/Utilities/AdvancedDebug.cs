using UnityEngine;

public class AdvancedDebug
{
	public static void Log(object obj)
	{
#if DEBUG
		Debug.Log(obj);
#endif
	}
	
	public static void LogWarning(object obj)
	{
#if DEBUG
		Debug.LogWarning(obj);
#endif
	}
	
	public static void LogError(object obj)
	{
#if DEBUG
		Debug.LogError(obj);
#endif
	}
		
}