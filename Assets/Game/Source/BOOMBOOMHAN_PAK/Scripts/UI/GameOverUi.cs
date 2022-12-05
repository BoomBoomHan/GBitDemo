using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
	public TMP_Text Level;

	public Sprite BlueSprite;
	public Sprite RedSprite;

	public Button ReturnButton;

	private void Awake()
	{
		//GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
