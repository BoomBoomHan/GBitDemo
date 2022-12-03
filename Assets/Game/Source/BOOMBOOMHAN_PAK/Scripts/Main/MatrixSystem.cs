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

	public static float Distance;

	private void Awake()
	{
		Distance = Mathf.Abs(floor1Sample.transform.position.x - floor2Sample.transform.position.x);
		AdvancedDebug.Log(Distance);

		floorMatrix = new Matrix<Floor>(6, 12);
		MatchFloors(GameObject.FindGameObjectsWithTag("Tile"));
		/*Destroy(floor1Sample);
		Destroy(floor2Sample);*/
	}

	private void MatchFloors(GameObject[] floors)
	{
		foreach (var floor in floors)
		{
			string name = floor.name;
			int l = name.IndexOf("{");
			int r = name.IndexOf("}");
			int d = name.IndexOf(",");

			int x = int.Parse(name.Substring(l + 1, d - l - 1));
			int y = int.Parse(name.Substring(d + 1, r - d - 1));
			floorMatrix[x, y] = floor.GetComponent<Floor>();
		}
	}
}
