using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RevertEvent : CustomMusicEvent
{
	private float time;
	
    /*public RevertEvent(MusicProperty music, int section, float beatInSection, int targetSection, float targetBeat)
    :base(music.CalculateBeginTimeFromEditor(section, beatInSection), Time.deltaTime, 0)
    {
	    time = music.ToTime(targetSection - 1, targetBeat - 1.0f);
    }*/

    public RevertEvent(MusicProperty music, RevertEventCreator creator)
    :base(music, creator)
    {
	    time = music.ToTime(creator.TargetSection - 1, creator.TargetBeatInSection - 1.0f);
    }

    public override void BeginPlay()
    {
	    base.BeginPlay();
	    
	    GameModeBase.Get<BroGameModeBase>().Player.RevertTo(time);
    }

    public override void OnDestroy()
    {
	    
    }
}

public class RevertEventCreator : CustomEventCreator
{
	public int TargetSection;

	public float TargetBeatInSection;
	
	public RevertEventCreator(int section, float beatInSection, int targetSection, float targetBeatInSection)
	:base(section, beatInSection, Time.deltaTime)
	{
		TargetSection = targetSection;
		TargetBeatInSection = targetBeatInSection;
	}
	
	public override string EventType { get => "Revert"; }
}