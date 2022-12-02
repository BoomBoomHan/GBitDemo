using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PulseCameraEvent : CustomMusicEvent
{
	public PulseCameraEvent(MusicProperty music, PulseCameraEventCreator creator)
    :base(music, creator)
    {
	    
    }

	public override void BeginPlay()
    {
	    base.BeginPlay();
	    CameraPulseHelper.Pulse(lifeSpan);
    }

    public override void OnDestroy()
    {
	    
    }
}

public class PulseCameraEventCreator : CustomEventCreator
{
	public PulseCameraEventCreator(int section, float beatInSection, float beatDuration)
	:base(section, beatInSection, beatDuration)
	{
		
	}
	
	public override string EventType
	{
		get => "PulseCamera";
	}
}