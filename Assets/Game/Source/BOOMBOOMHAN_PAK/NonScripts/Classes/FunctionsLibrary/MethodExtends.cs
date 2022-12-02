using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodExtends
{
	public static bool NearlyZero(this Vector2 vec)
	{
		return vec.magnitude <= BasicDatas.FloatTolerance * 10f;
	}

	public static float Angle(this Vector2 vec)
	{
		float rad = Mathf.Atan2(vec.y, vec.x);
		return rad * Mathf.Rad2Deg;
	}

	public static bool IsInjurable(this GameObject go, out IInjurable injury)
	{
		injury = go.GetComponent(typeof(IInjurable)) as IInjurable;
		return injury;
	}

	public static bool IsInjurable(this Component comp, out IInjurable injury)
	{
		injury = comp.GetComponent(typeof(IInjurable)) as IInjurable;
		return injury;
	}

	public static TimerManager GetTimerManager(this MonoBehaviour monoBehaviour)
	{
		if (monoBehaviour.enabled)
		{
			return TimerManager.GetTimerManager();
		}

		return null;
	}
}
