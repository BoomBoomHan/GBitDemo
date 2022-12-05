using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
	public static GameUi Instance;

	[HideInInspector]
	public Canvas UiCanvas;
	public Image BlueIcon;
	public Image RedIcon;
	public Slider BlueHpBar;
	public Slider RedHpBar;
	public TMP_Text BlueHpText;
	public TMP_Text RedHpText;
	public TMP_Text RemainStepsLabel;
	public TMP_Text RemainStepsCount;
	public TMP_Text BlueDamage;
	public TMP_Text RedDamage;
	public TMP_Text BottomText;
	public TMP_Text BluePreview;
	public TMP_Text RedPreview;

	public Color BlueColor;
	public Color RedColor;
	public Color NormalColor;

	UiGroup blueGroup;
	UiGroup redGroup;

	public GameUi()
	{
		blueGroup = new UiGroup(BlueIcon, BlueHpBar, BlueHpText);
		redGroup = new UiGroup(RedIcon, RedHpBar, RedHpText);
	}

	public class UiGroup
	{
		public Image Icon;
		public Slider Bar;
		public TMP_Text HpText;

		public UiGroup(Image icon, Slider bar, TMP_Text hpText)
		{
			Icon = icon;
			Bar = bar;
			HpText = hpText;
		}
	}

	public UiGroup GetGroup(EPlayerTeam team)
	{
		return team == EPlayerTeam.Blue ? blueGroup : team == EPlayerTeam.Red ? redGroup : null;
	}

	public Color GetColor(EPlayerTeam team)
	{
		return team == EPlayerTeam.Blue ? BlueColor : team == EPlayerTeam.Red ? RedColor : NormalColor;
	}

	private void Awake()
	{
		UiCanvas = GetComponent<Canvas>();
		UiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		UiCanvas.worldCamera = Camera.main;

		Instance = this;
	}
}
