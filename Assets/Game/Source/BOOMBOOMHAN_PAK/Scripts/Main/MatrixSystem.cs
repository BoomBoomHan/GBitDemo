using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixSystem : MonoBehaviour
{
	[SerializeField, DisplayName("地砖1样品")]
	GameObject floor1Sample;
	[SerializeField, DisplayName("地砖2样品")]
	GameObject floor2Sample;

	Matrix<Floor> floorMatrix;

	MatGameModeBase gmb;

	[SerializeField]
	private GameObject blockingWall;

	public GameObject BlockingWall
	{
		get => blockingWall;
	}

	public IntVector2D Size
	{
		get;private set;
	}

	public float Distance
	{
		get; private set;
	}

	public Vector2 ZeroPoint
	{
		get => floorMatrix[0, 0];
	}

	private void Awake()
	{
		Distance = Mathf.Abs(floor1Sample.transform.position.x - floor2Sample.transform.position.x);
		AdvancedDebug.Log(Distance);
		floorMatrix = new Matrix<Floor>(6, 12);

		Size = floorMatrix.Size;
		MatchFloors(GameObject.FindGameObjectsWithTag("Tile"));

		/*Destroy(floor1Sample);
		Destroy(floor2Sample);*/
	}

	private void Start()
	{
		gmb = GameModeBase.Get<MatGameModeBase>();
		gmb.P1Character.MoveBegin.AddListener(ResetCharacterEnter);
		gmb.P1Character.MoveEnd.AddListener(OnBlueEnter);

		gmb.P2Character.MoveBegin.AddListener(ResetCharacterEnter);
		gmb.P2Character.MoveEnd.AddListener(OnRedEnter);
	}

	[DisplayName("浮动系数")]
	public float FloatingRatio = 0.35f;

	public static float ZLocation = 0.0f;

	float tick = 0.0f;
	private void Update()
	{
		ZLocation = Mathf.Sin(tick) * FloatingRatio * 0.75f;
		tick = (tick + Time.deltaTime) % (2.0f * Mathf.PI);

		Vector3 target = blockingWall.transform.position;
		target.z = ZLocation;
		blockingWall.transform.position = target;
	}

	private void MatchFloors(GameObject[] floors)
	{
		//AdvancedDebug.Log(floors.Length);
		foreach (var floor in floors)
		{
			string name = floor.name;
			int l = name.IndexOf("(");
			int r = name.IndexOf(")");
			int d = name.IndexOf(",");

			int x = int.Parse(name.Substring(l + 1, d - l - 1));
			int y = int.Parse(name.Substring(d + 1, r - d - 1));
			
			FloorMatrix[x, y] = floor.GetComponent<Floor>();
			FloorMatrix[x, y].Coord = new IntVector2D(x, y);
			if (y >= FloorMatrix.Size.Y / 2)
			{
				FloorMatrix[x, y].Team = EPlayerTeam.Red;
			}
		}
	}

	public Matrix<Floor> FloorMatrix
	{
		get => floorMatrix;
	}

	public Floor this[IntVector2D vec]
	{
		get => FloorMatrix[vec];
	}

	public Floor this[int x, int y]
	{
		get => FloorMatrix[new IntVector2D(x, y)];
	}

	public void SetCharacterEnter(IntVector2D location)
	{
		floorMatrix[location].HasCharaterUpon = true;
	}

	public void ResetCharacterEnter(IntVector2D location)
	{
		floorMatrix[location].HasCharaterUpon = false;
	}

	private void OnBlueEnter(IntVector2D location)
	{
		//AdvancedDebug.LogWarning("OnBlueEnter");
		floorMatrix[location].OnCharacterEnter(gmb.P1Character);
		SetCharacterEnter(location);
	}

	private void OnRedEnter(IntVector2D location)
	{
		//AdvancedDebug.LogWarning("OnRedEnter");
		floorMatrix[location].OnCharacterEnter(gmb.P2Character);
		SetCharacterEnter(location);
	}
}
