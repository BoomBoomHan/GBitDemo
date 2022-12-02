using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddBombEvent : MusicEvent
{
	private Bomb bomb;

	public AddBombEvent(MusicProperty music, AddBombEventCreator creator)
		: base(music.CalculateBeginTimeFromEditor(creator.ApproachSection, (music.ToBeat(creator.ApproachSection - 1, 
		creator.ApproachBeat - creator.BeatLength - 1.0f) == 0.0f ? 1.05f : creator.ApproachBeat), creator.BeatLength),
			music.ToDuration(creator.BeatLength), creator.TrackOrder - 1)
	{
		
	}
	
	public override void BeginPlay()
	{
		base.BeginPlay();

		var broGameModeBase = GameModeBase.Instance as BroGameModeBase;
		bomb = broGameModeBase.GetBombsPool()
			.ApplyBomb(new BombSpawnParameters((TrackSystem.Instance.SpawnX - TrackSystem.Instance.BoomX) / lifeSpan,
				lifeSpan, trackIndex));
		//Debug.LogWarning(player.Beat);
		//bomb = GameModeBase.Instantiate<GameObject>(BombFinder.Instance.BombResource).GetComponent<Bomb>();
		//bomb.SetSpeed(0.0f);
	}

	public override void OnDestroy()
	{
		//Debug.LogWarning(player.Beat);
		//GameModeBase.Destroy(bomb.gameObject);
		var broGameModeBase = GameModeBase.Get<BroGameModeBase>();
		broGameModeBase.HandleWhenBombArrives(bomb, trackIndex);
	}
}

[Serializable]
public struct AddBombEventCreator
{
	public int ApproachSection;
	public float ApproachBeat;
	public float BeatLength;
	public int TrackOrder;
}