using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameOverEvent : MusicEvent
{
    public GameOverEvent(MusicProperty music, GameOverEventCreator creator)
    :base(music.CalculateBeginTimeFromEditor(creator.Section, creator.Beat), Time.deltaTime, 0)
    {
        
    }

    public override void BeginPlay()
    {
	    base.BeginPlay();
	    
	    GameModeBase.Instance.Victory();
    }

    public override void OnDestroy()
    {
	    
    }
}

[Serializable]
public struct GameOverEventCreator
{
	public int Section;
	public float Beat;
}