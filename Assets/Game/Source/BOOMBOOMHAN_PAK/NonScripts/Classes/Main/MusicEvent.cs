using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MusicEvent
{
	protected float beginTime;

	protected float lifeSpan;

	protected int trackIndex;

	public float BeginTime
	{
		get => beginTime;
		set => beginTime = value;
	}

	public float LifeSpan
	{
		get => lifeSpan;
	}

	public int TrackIndex
	{
		get => trackIndex;
	}

	public MusicEvent(float bTime, float lSpan, int tIndex)
	{
		beginTime = bTime;
		lifeSpan = lSpan;
		trackIndex = tIndex;
	}

	public virtual void BeginPlay()
	{
		TimerManager.GetTimerManager().SetTimer(OnDestroy, 0.0f, lifeSpan, 1L);
	}
	public abstract void OnDestroy();
}

public class StartEvent : MusicEvent
{
	public StartEvent()
	:base(0.0f, Time.deltaTime, 0)
	{
		
	}
	
	public override void BeginPlay()
	{
		base.BeginPlay();
	}

	public override void OnDestroy()
	{
		
	}
}