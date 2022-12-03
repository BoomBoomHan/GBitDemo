using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatCharacter : Character2D
{
	IntVector2D location;

	private Player playerConfig;

	public IntVector2D Location
	{
		get { return location; }
		set { location = value; }
	}

	public EPlayerTeam Team
	{
		get { return playerConfig.PlayerTeam; }
		set { playerConfig.PlayerTeam = value; }
	}

	public UnityEvent<IntVector2D> MoveBegin;

	public UnityEvent<IntVector2D> MoveEnd;

	private MatrixSystem matrixSystem;

	public MatCharacter()
	{
		playerConfig = new Player();
	}

	protected override void Awake()
	{
		base.Awake();

		MoveBegin = new UnityEvent<IntVector2D>();
		MoveEnd = new UnityEvent<IntVector2D>();

		MoveBegin.AddListener(OnMoveBegin);
		MoveEnd.AddListener(OnMoveEnd);

		matrixSystem = GameModeBase.Get<MatGameModeBase>().MatSystem;
	}

	[Obsolete]
	public void Move(Vector2 delta, float duration)
	{
		MoveBegin.Invoke(location);
		var dt = transform.DOMove(transform.position + (Vector3)delta, duration);
		dt.onComplete += EndMove;
	}

	private IntVector2D endLocation;

	public void MoveTo(IntVector2D matrixLocation, float duration)
	{
		endLocation = matrixLocation;
		MoveBegin.Invoke(location);
		var dt = transform.DOMove((Vector2)matrixSystem[endLocation], duration);
		dt.onComplete += EndMove;
	}

	private void EndMove()
	{
		//AdvancedDebug.Log("EndMove");
		location = endLocation;
		MoveEnd.Invoke(location);
	}

	private void OnMoveBegin(IntVector2D vec)
	{

	}

	private void OnMoveEnd(IntVector2D vec)
	{
		//AdvancedDebug.Log("OnMoveEnd");
	}
}
