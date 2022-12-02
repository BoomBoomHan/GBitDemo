///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameplayStatics
{
	public static void PauseGame(bool pauseMusic = true, UnityEvent onPause = null)
	{
		GameState.PauseGame(pauseMusic, onPause);
	}

	public static void ResumeGame(UnityEvent onResume = null)
	{
		GameState.ResumeGame(onResume);
	}
}
