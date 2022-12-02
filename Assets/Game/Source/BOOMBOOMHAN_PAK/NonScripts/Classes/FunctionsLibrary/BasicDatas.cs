using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasicDatas
{
	public static float FloatTolerance = 1e-3f;

	public static double DoubleTolrance = 1e-5;

	public static bool NearlyZero(this Single number)
	{
		return Mathf.Abs(number) <= FloatTolerance;
	}
	
	public static bool NearlyZero(this Double number)
	{
		return Math.Abs(number) <= DoubleTolrance;
	}

	public static bool IsInEditor
	{
		get =>
#if UNITY_EDITOR
			true;
#else
			false;
#endif

	}
}
