///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsPingRefresher : MonoBehaviour
{

	[SerializeField]
	private float refreshFrequency;

	[SerializeField]
	private TMP_Text displayTxt;

	private Timer refreshTimer;

	public FpsPingRefresher()
	{
		refreshFrequency = 1f;
	}

	private void Start()
	{
		TimerManager manager = TimerManager.GetTimerManager();
		refreshTimer = manager.SetTimer(Refresh, refreshFrequency, 0f, 0);
	}

	private void Update()
	{
		
	}

	private void OnDisable()
	{
		TimerManager.GetTimerManager().ClearTimer(refreshTimer);
	}

	void Refresh()
	{
		displayTxt.SetText($"FPS : {GameModeBase.Instance.Fps}	{GameModeBase.Instance.GamePing}ms");
	}
}
