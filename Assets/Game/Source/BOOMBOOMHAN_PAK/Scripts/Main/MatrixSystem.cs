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

	public static float Distance;

	private void Awake()
	{
		Distance = Mathf.Abs(floor1Sample.transform.position.x - floor2Sample.transform.position.x);
		AdvancedDebug.Log(Distance);
		Destroy(floor1Sample);
		Destroy(floor2Sample);

 
	}

	void MatchMatrix(GameObject[] floors)
	{

	}
}
