using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatPlayerState : PlayerState
{
	MatCharacter mc;

	bool isMoving;

	int remainingStepCount;

	public EPlayerTeam Team;

	public int RemainingStepCount
	{
		get => remainingStepCount;
		set => remainingStepCount = value;
	}

	protected override void Awake()
	{
		base.Awake();

		isMoving = false;
		remainingStepCount = 0;
	}

	protected override void Start()
	{
		base.Start();

		/*var gmb = GameModeBase.Get<MatGameModeBase>();

		mc = Team == EPlayerTeam.Blue ? gmb.P1Character : gmb.P2Character;*/
		mc = defaultCharacter as MatCharacter;
		mc.MoveBegin.AddListener(SetMoveState);
		mc.MoveBegin.AddListener(ConsumeStep);
		mc.MoveEnd.AddListener(ResetMoveState);
		mc.MoveEnd.AddListener(CheckIfDetention);
	}

	public void OnRoundBegin()
	{
		remainingStepCount = 5;
		mc.CheckIfDetention(mc.Location);
	}

	public void OnRoundEnd()
	{
		/*if (remainingStepCount > 0)
		{
			mc.CheckIfDetention(mc.Location);
		}*/
	}

	void CheckIfDetention(IntVector2D location)
	{
		if (remainingStepCount > 0)
		{
			mc.CheckIfDetention(location);
		}
	}

	public bool CanMove()
	{
		return IsInRound && !IsMoving;
	}

	public bool IsInRound
	{
		get => remainingStepCount != 0;
	}

	public bool IsMoving
	{
		get => isMoving;
	}

	void SetMoveState(IntVector2D vec)
	{
		//AdvancedDebug.Log(mc.Team.ToString() + "Move");
		isMoving = true;
	}

	void ResetMoveState(IntVector2D vec)
	{
		//AdvancedDebug.Log(mc.Team.ToString() + "EndMove");
		isMoving = false;
	}

	void ConsumeStep(IntVector2D vec)
	{
		remainingStepCount--;
	}
}
