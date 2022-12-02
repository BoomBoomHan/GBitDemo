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
	public int X = -8;

	public float Y = 19.0f;

	void Awake()
	{
		AdvancedDebug.Log($"{X}	{Y}");
	}
}
