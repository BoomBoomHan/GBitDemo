using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class ShowTextEvent : CustomMusicEvent
{
	private static TMP_Text txt;

	public static TMP_Text Txt
	{
		get
		{
			if (!txt)
			{
				var ui = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Main/UI/TextUi"));
				var canvas = ui.GetComponent<Canvas>();
				canvas.worldCamera = Camera.main;
				canvas.renderMode = RenderMode.ScreenSpaceCamera;
				canvas.renderMode = RenderMode.WorldSpace;
				txt = ui.GetComponentInChildren<TMP_Text>();
			}

			return txt;
		}
	}
	
	private Color targetColor;

	private float fadeDuration;

	private string sentence;

	public ShowTextEvent(MusicProperty music, ShowTextEventCreator creator)
    :base(music, creator)
    {
	    targetColor = creator.TargetColor;
	    fadeDuration = music.ToDuration(creator.FadeBeatDuration);
	    sentence = creator.Sentence;
    }

    public override void BeginPlay()
    {
	    base.BeginPlay();

	    Debug.Assert(lifeSpan > 2.0f * fadeDuration);

	    Txt.text = sentence;
	    Txt.color = Color.clear;
	    if (fadeDuration <= 0.0f)
	    {
		    Txt.color = targetColor;
		    TimerManager.GetTimerManager().SetTimer((() => Txt.color = Color.clear), 0.0f, lifeSpan, 1L);
	    }
	    else
	    {
		    Txt.DOColor(targetColor, fadeDuration);
		    TimerManager.GetTimerManager().SetTimer((() => Txt.DOColor(Color.clear, fadeDuration)), 0.0f, lifeSpan - fadeDuration, 1L);
	    }
    }

    public override void OnDestroy()
    {
	    Txt.color = Color.clear;
    }
}

public class ShowTextEventCreator : CustomEventCreator
{
	public Color TargetColor;

	public float FadeBeatDuration;

	public string Sentence;

	public ShowTextEventCreator(int section, float beatInSection, float beatDuration, Color targetColor, string sentence, float fadeBeatDuration = -1.0f)
	:base(section, beatInSection, beatDuration)
	{
		ShowTextEvent.Txt.text = "";//本意为创建
		
		TargetColor = targetColor;
		FadeBeatDuration = fadeBeatDuration;
		Sentence = sentence;
	}
	
	public override string EventType { get => "ShowText"; }
}