using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBroGameModeBase : BroGameModeBase
{
	private new LordBrother lordBrother;

	protected override void Awake()
	{
		base.Awake();
		
		lordBrother = DefaultCharacter as LordBrother;
		HitEvent.AddListener(PulseIfPerfect);
	}

	public override void HandleWhenBombArrives(Bomb bomb, int bombTrackIndex)
	{
		base.HandleWhenBombArrives(bomb, bombTrackIndex);
		
	}

	protected override bool ShouldInjureBrother(int broTrackIndex, int bombTrackIndex)
	{
		if (!lordBrother.ShowShadows)
		{
			return base.ShouldInjureBrother(broTrackIndex, bombTrackIndex);
		}
		return Math.Abs(broTrackIndex - bombTrackIndex) <= 1;
	}

	void PulseIfPerfect(BombHitResult result)
	{
		if (!result.HasInjure)
		{
			CameraPulseHelper.Pulse(Music.ToDuration(0.25f));
		}
	}
}
