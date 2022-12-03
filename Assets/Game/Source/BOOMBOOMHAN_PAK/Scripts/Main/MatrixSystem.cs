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
		MatchFloors(GameObject.FindGameObjectsWithTag("Tile"));
		Size = floorMatrix.Size;
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

	private void MatchFloors(GameObject[] floors)
	{
		foreach (var floor in floors)
		{
			string name = floor.name;
			int l = name.IndexOf("(");
			int r = name.IndexOf(")");
			int d = name.IndexOf(",");

			int x = int.Parse(name.Substring(l + 1, d - l - 1));
			int y = int.Parse(name.Substring(d + 1, r - d - 1));
			
			floorMatrix[x, y] = floor.GetComponent<Floor>();
			if (y >= floorMatrix.Size.Y / 2)
			{
				floorMatrix[x, y].Team = EPlayerTeam.Red;
			}
		}
	}

	public Matrix<Floor> FloorMatrix
	{
		get => floorMatrix;
	}

	public Floor this[IntVector2D vec]
	{
		get => floorMatrix[vec];
	}

	private void Replace(IntVector2D originLocation, GameObject resource)
	{
		var floor = floorMatrix[originLocation];
		var team = floor.Team;
		var pos = floor;
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
