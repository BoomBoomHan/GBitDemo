using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ChangeBackgroundColorEvent : CustomMusicEvent
{
	private static SpriteRenderer backfroundRenderer;

	public static SpriteRenderer BackgroundRenderer
	{
		get
		{
			if (!backfroundRenderer)
			{
				backfroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
			}

			return backfroundRenderer;
		}
	}
	
	private Color targetColor;
	
    /*public ChangeBackgroundColorEvent(float bTime, float lSpan, Color targetColor)
    :base(bTime, lSpan, 0)
    {
	    this.targetColor = targetColor;
	    Debug.Assert(BackgroundRenderer != null);
    }*/

    public ChangeBackgroundColorEvent(MusicProperty music, ChangeBackgroundColorEventCreator creator)
    :base(music, creator)
    //this(music.CalculateBeginTimeFromEditor(creator.BeginSection, creator.BeginBeat), music.ToDuration(creator.BeatDuration), creator.TargetColor)
    {
	    targetColor = creator.TargetColor;
    }

    public override void BeginPlay()
    {
	    base.BeginPlay();

	    BackgroundRenderer.DOColor(targetColor, lifeSpan);
    }

    public override void OnDestroy()
    {
	    
    }
}

[Serializable]
public class ChangeBackgroundColorEventCreator : CustomEventCreator
{
	public Color TargetColor;

	public ChangeBackgroundColorEventCreator(int section, float beatInSection, float beatDuration, Color targetColor)
	:base(section, beatInSection, beatDuration)
	{
		TargetColor = targetColor;
	}

	public override string EventType
	{
		get => "ChangeBackgroundColor";
	}
}