///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Developer
{
	public static bool InDevelopment
	{
		get
		{
#if UNITY_EDITOR || DEBUG
			return true;
#else
			return false;
#endif
		}
	}

	public static bool InTest
	{
		get
		{
			if (Slot.TryLoadGameFromNative(out Slot slot))
			{
				return true;
			}
			return false;
		}
	}

	public static bool InDemo
	{
		get
		{
			return true;
		}
	}
}
