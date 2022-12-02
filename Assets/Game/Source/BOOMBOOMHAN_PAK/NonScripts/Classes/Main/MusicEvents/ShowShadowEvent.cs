using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShadowEvent : CustomMusicEvent
{
	private static LordBrother brother;

	private static LordBrother lordBrother
	{
		get
		{
			if (!brother)
			{
				brother = GameModeBase.Instance.DefaultCharacter as LordBrother;
				Debug.Assert(brother != null);
			}

			return brother;
		}
	}
	
	private bool visible;
	
    public ShowShadowEvent(MusicProperty music, ShowShadowEventCreator creator)
    :base(music, creator)
    {
	    visible = creator.Visible;
    }

    public override void BeginPlay()
    {
	    base.BeginPlay();

	    
	    if (visible)
	    {
		    lordBrother.DOExpandShadows(lifeSpan);
	    }
	    else
	    {
		    lordBrother.DOHideShadows(lifeSpan);
	    }
    }

    public override void OnDestroy()
    {
	    
    }
}

public class ShowShadowEventCreator : CustomEventCreator
{
	public bool Visible;

	public ShowShadowEventCreator(int section, float beatInSection, float beatDuration, bool visible)
	:base(section, beatInSection, beatDuration)
	{
		Visible = visible;
	}
	
	public override string EventType { get => "ShowShadow"; }
}