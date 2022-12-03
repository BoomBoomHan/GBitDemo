using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatGameState : GameState
{
	MatGameModeBase gmb;

	class PlayerComponentGroup
	{
		public MatCharacter Mc;
		public MatPlayerController Mpc;
		public MatPlayerState Mps;

		public PlayerComponentGroup(MatCharacter mc, MatPlayerController mpc, MatPlayerState mps)
		{
			Mc = mc;
			Mpc = mpc;
			Mps = mps;
		}
	}

	PlayerComponentGroup actPlayer;
	PlayerComponentGroup waitPlayer;

	protected override void Start()
	{
		base.Start();

		gmb = GameModeBase.Get<MatGameModeBase>();
		actPlayer = new PlayerComponentGroup(gmb.P1Character, gmb.P1Controller, gmb.P1State);
		waitPlayer = new PlayerComponentGroup(gmb.P2Character, gmb.P2Controller, gmb.P2State);

		actPlayer.Mc.MoveBegin.AddListener(JudgeIfRoundEnd);
		waitPlayer.Mc.MoveBegin.AddListener(JudgeIfRoundEnd);

		OnRoundBegin();
	}

	void OnRoundBegin()
	{
		actPlayer.Mpc.EnableInput();
		actPlayer.Mps.OnRoundBegin();
	}

	async void JudgeIfRoundEnd(IntVector2D vec)
	{
		await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime));
		//AdvancedDebug.LogWarning($"{gmb.P1State.RemainingStepCount}	{gmb.P2State.RemainingStepCount}	{actPlayer.Mc == gmb.P1Character}");
		if (actPlayer.Mps.RemainingStepCount == 0)
		{
			OnRoundEnd();
		}
	}

	async void OnRoundEnd()
	{
		AdvancedDebug.LogWarning($"{actPlayer.Mc.Team} RoundEnd");
		actPlayer.Mps.OnRoundEnd();
		actPlayer.Mpc.DisableInput();

		await CallSpecicalEvent();
		NewRound();
	}

	int roundCount = 0;
	bool hasWall = true;

	async UniTask CallSpecicalEvent()
	{
		roundCount++;
		if (roundCount == 6)
		{
			hasWall = false;
		}
		await UniTask.Delay(1000);
	}

	void NewRound()
	{
		PlayerComponentGroup temp = actPlayer;
		actPlayer = waitPlayer;
		waitPlayer = temp;
		OnRoundBegin();
	}

	public bool DoesBlockExist()
	{
		return hasWall;
	}
}
