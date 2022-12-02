using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public abstract class CustomMusicEvent : MusicEvent
{
    public CustomMusicEvent(MusicProperty music, CustomEventCreator creator)
    :base(music.CalculateBeginTimeFromEditor(creator.Section, creator.BeatInSection), music.ToDuration(creator.BeatDuration), 0)
    {
        
    }
}

public abstract class CustomEventCreator
{
	public int Section;
	public float BeatInSection;
	public float BeatDuration;

	public CustomEventCreator(int section, float beatInSection, float beatDuration)
	{
		Section = section;
		BeatInSection = beatInSection;
		BeatDuration = beatDuration;
	}

	public abstract string EventType
	{
		get;
	}
}

public delegate CustomMusicEvent CreatorFunc(MusicProperty music, CustomEventCreator creator);

public static class CustomEventLibrary
{
	private static Dictionary<string, CreatorFunc> creatorFuncs;

	static CustomEventLibrary()
	{
		creatorFuncs = new Dictionary<string, CreatorFunc>()
		{
			{"PulseCamera", CreatePCEvent},
			{"ChangeBackgroundColor", CreateCBCEvent},
			{"Revert", CreateREvent},
			{"ShowText", CreateSTEvent},
			{"ShowShadow", CreateSSEvent},
		};
	}

	public static CustomMusicEvent Create(MusicProperty music, CustomEventCreator creator)
	{
		return creatorFuncs[creator.EventType](music, creator);
	}

	/*private static T CreateImpl<T, U>(MusicProperty music, CustomEventCreator creator) where T : CustomMusicEvent
	{
		return new T(music, creator as U);
	}*/

	private static PulseCameraEvent CreatePCEvent(MusicProperty music, CustomEventCreator creator)
	{
		return new PulseCameraEvent(music, creator as PulseCameraEventCreator);
	}

	private static ChangeBackgroundColorEvent CreateCBCEvent(MusicProperty music, CustomEventCreator creator)
	{
		return new ChangeBackgroundColorEvent(music, creator as ChangeBackgroundColorEventCreator);
	}

	private static RevertEvent CreateREvent(MusicProperty music, CustomEventCreator creator)
	{
		return new RevertEvent(music, creator as RevertEventCreator);
	}

	private static ShowTextEvent CreateSTEvent(MusicProperty music, CustomEventCreator creator)
	{
		return new ShowTextEvent(music, creator as ShowTextEventCreator);
	}
	
	private static ShowShadowEvent CreateSSEvent(MusicProperty music, CustomEventCreator creator)
	{
		return new ShowShadowEvent(music, creator as ShowShadowEventCreator);
	}
}