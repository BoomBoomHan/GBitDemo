using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Input;

public class ShowMaker : MonoBehaviour
{
	Matrix<int> iMatrix;

	void Awake()
	{
		iMatrix = new Matrix<int>(4, 4);
		iMatrix[0, 0] = 1;
		iMatrix[0, 1] = 2;
		iMatrix[0, 2] = 3;
		iMatrix[0, 3] = 4;
		iMatrix[1, 0] = 5;
		iMatrix[2, 0] = 6;
		iMatrix[3, 0] = 7;

		/*AdvancedDebug.Log(iMatrix.IsValid(1, 1));
		AdvancedDebug.LogWarning(iMatrix.IsValid(-1, 1));
		AdvancedDebug.LogError(iMatrix.IsValid(1, 4));*/
		/*AdvancedDebug.LogWarning(iMatrix.SumByRow(0, (x) => x));
		AdvancedDebug.LogError(iMatrix.SumByCol(0, (x) => x));*/
	}
}
