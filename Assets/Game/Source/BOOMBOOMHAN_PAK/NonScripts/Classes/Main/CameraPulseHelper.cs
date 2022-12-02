using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraPulseHelper
{
	private static Animator mainCameraAnimator;

	public static Animator MainCameraAnimator
	{
		get
		{
			if (!mainCameraAnimator)
			{
				mainCameraAnimator = Camera.main.GetComponent<Animator>();
			}

			return mainCameraAnimator;
		}
	}

	public static void Pulse(float duration)
	{
		MainCameraAnimator.speed = 1.0f / duration;
		MainCameraAnimator.SetTrigger("Pulse");
	}
}
