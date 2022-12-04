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

	public bool HasSupplies
	{
		get;
		private set;
	}

	public UnityEvent<IntVector2D> MoveBegin;

	public UnityEvent<IntVector2D> MoveEnd;

	private MatrixSystem matrixSystem;

	public int Hp
	{
		get;
		private set;
	}

	public int MaxHp
	{
		get => (int)playerConfig.InitiaHP;
	}

	public float HpPercentage
	{
		get => (float)Hp / MaxHp;
	}

	public MatCharacter()
	{
		playerConfig = new Player();
		HasSupplies = false;
	}

	public static Vector3 GetVerticalDir(Vector3 _dir)
	{
		//��_dir.x,_dir.z���루����1����ֱ����_dir.x * �� + _dir.z * 1 = 0
		if (_dir.x == 0)
		{
			return new Vector3(1, 0, 0);
		}
		else
		{
			return new Vector3(-_dir.z / _dir.x, 0, 1).normalized;
		}
	}

	protected override void Awake()
	{
		base.Awake();

		Hp = MaxHp;

		MoveBegin = new UnityEvent<IntVector2D>();
		MoveEnd = new UnityEvent<IntVector2D>();

		MoveBegin.AddListener(OnMoveBegin);
		MoveEnd.AddListener(OnMoveEnd);

		matrixSystem = GameModeBase.Get<MatGameModeBase>().MatSystem;

		var camera = Camera.main;

		var rotator = camera.transform.rotation.eulerAngles;
		AdvancedDebug.LogWarning(rotator);
	}

	protected override void Update()
	{
		base.Update();

		Vector3 target = transform.position;
		target.z = MatrixSystem.ZLocation;
		transform.position = target;
	}

	private IntVector2D endLocation;

	public void MoveTo(IntVector2D matrixLocation, float duration)
	{
		endLocation = matrixLocation;
		MoveBegin.Invoke(location);
		var dt = transform.DOMoveX(((Vector2)matrixSystem[endLocation]).x, duration);
		transform.DOMoveY(((Vector2)matrixSystem[endLocation]).y, duration);
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

	public int ApplyDamage(int dmg)
	{
		int actuallyCaused = Math.Clamp(dmg, 0, Hp);
		Hp -= dmg;
		return actuallyCaused;
	}
}
