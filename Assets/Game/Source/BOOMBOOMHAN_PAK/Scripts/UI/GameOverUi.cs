using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUi : MonoBehaviour
{
	public TMP_Text Level;

	public TMP_Text SubTitle;

	private void Awake()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
