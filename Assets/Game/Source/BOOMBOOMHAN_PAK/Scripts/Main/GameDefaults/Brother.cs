using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Brother : Character2D
{
	[SerializeField]
	private TrackPoint[] trackPoints;

	private int currentPointIndex;

	private float runDuration;

	public UnityEvent<int> WhenRun;

	public UnityEvent<float> WhenStopRunning;

	public UnityEvent<int> WhenInjure;

	public int CurrentPointIndex
	{
		get => currentPointIndex;
	}

	public Brother()
	{
		runDuration = 0.1f;
		WhenRun = new UnityEvent<int>();
		WhenStopRunning = new UnityEvent<float>();
		WhenInjure = new UnityEvent<int>();
	}
	
	protected override void Awake()
	{
		base.Awake();
		currentPointIndex = 0;
		
		WhenInjure.AddListener((arg0 => Debug.LogError($"Injured:{arg0} times.")));
	}

	protected override void Start()
	{
		base.Start();

		trackPoints = TrackSystem.Instance.Tracks;
		SetPositionByTrack(0);
	}

	public virtual bool Run(int deltaTrack, bool withBlend = true)
	{
		var nIndex = deltaTrack + currentPointIndex;
		if (nIndex < 0 || nIndex > 3)
		{
			return false;
		}

		WhenRun.Invoke(nIndex);
		if (withBlend)
		{
			SetPositionByTrackWithBlend(nIndex, runDuration);
			TimerManager.GetTimerManager().SetTimer((() => WhenStopRunning.Invoke(runDuration)), runDuration, 0.0f, 1L);
		}
		else
		{
			SetPositionByTrack(nIndex);
			WhenStopRunning.Invoke(0.0f);
		}
		return true;
	}

	public void SetPositionByTrack(int index)
	{
		Debug.Assert(index >= 0 && index <= 3);
		
		currentPointIndex = index;
		transform.position = trackPoints[currentPointIndex].transform.position;
	}

	public void SetPositionByTrackWithBlend(int index, float blendDuration)
	{
		Debug.Assert(index >= 0 && index <= 3);
		
		currentPointIndex = index;
		Vector2 target = trackPoints[currentPointIndex].transform.position;
		transform.DOMove(target, blendDuration, false);
	}

	public void Injure(int arg)
	{
		WhenInjure.Invoke(arg);
	}
}
