using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatGameModeBase : GameModeBase
{
    private MatCharacter mc0;
	private MatCharacter mc1;

	public MatCharacter P1Character
	{
		get => mc0;
	}
	public MatCharacter P2Character
	{
		get => mc1;
	}

	private MatPlayerController mpc0;
	private MatPlayerController mpc1;

	public MatPlayerController P1Controller
	{
		get => mpc0;
	}
	public MatPlayerController P2Controller
	{
		get => mpc1;
	}

	private MatPlayerState mps0;
	private MatPlayerState mps1;

	public MatPlayerState P1State
	{
		get => mps0;
	}
	public MatPlayerState P2State
	{
		get => mps1;
	}

	private Vector2 p1StartPosition;

	protected override void Awake()
    {
		if (Instance)
		{
			Debug.LogError("Fatal!");
			Debug.LogError("GameModeBase存在多个实例！");
		}

		Instance = this;
		mc0 = Instantiate(defaultCharacter, PlayerStartPosition, Quaternion.identity).GetComponent<MatCharacter>();
		mc1 = Instantiate(defaultCharacter, p1StartPosition, Quaternion.identity).GetComponent<MatCharacter>();

		mpc0 = Instantiate(defaultPlayerController).GetComponent<MatPlayerController>();
		mpc1 = Instantiate(defaultPlayerController).GetComponent<MatPlayerController>();
		InstantiateDefaults(ref defaultGameState);
		mps0 = Instantiate(defaultPlayerState).GetComponent<MatPlayerState>();
		mps1 = Instantiate(defaultPlayerState).GetComponent<MatPlayerState>();

		mc0.Team = EPlayerTeam.Blue;
		mc1.Team = EPlayerTeam.Red;
		mpc1.HorizontalAxisName = "P2Horizontal";
		mpc1.VerticalAxisName = "P2Vertical";

		if (!GameInstance.Instance)
		{
			InstantiateDefaults(ref gameInstance);
		}

		mpc0.BindCharacter(mc0);
		mpc1.BindCharacter(mc1);

		mpc0.BindPlayerState(mps0);
		mpc1.BindPlayerState(mps1);
		mps0.BindCharacter(mc0);
		mps1.BindCharacter(mc1);

		UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
		Application.targetFrameRate = -1;
	}
}
