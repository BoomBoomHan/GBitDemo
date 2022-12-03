using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	private int number;

	public EPlayerTeam Team
	{
		get;set;
	}

	public virtual int Number
	{
		get => number;
		set => number = value;
	}

	[SerializeField, DisplayName("中心点")]
	Transform centerPoint;

	[SerializeField, DisplayName("个位数Sprite")]
	SpriteRenderer unitRenderer;

	IntChangedEvents iEvents;

	private static Sprite[] numberSprites;

	public bool HasCharaterUpon
	{
		get;set;
	}

	public Floor()
	{
		Team = EPlayerTeam.Blue;
	}

	protected virtual void Awake()
	{
		if (numberSprites != null)
		{
			return;
		}

		numberSprites = new Sprite[10];
		for (int i = 0; i < 4; i++)
		{
			numberSprites[i] = Resources.Load<Sprite>($"Sprites/Pictures/Main/Numbers/Number{i}");
		}

		//TimerManager.GetTimerManager().SetTimer(() => AdvancedDebug.LogError("666"), 0.0f, 1.0f, 1L);
	}

	void OnNumberChanged()
	{
		AdvancedDebug.LogWarning("OnNumberChanged");
		Color targetColor = Color.white;
		targetColor.a = 0.85f;
		unitRenderer.color = targetColor;
		unitRenderer.sprite = numberSprites[number];
	}

	private void Update()
	{
		
	}

	public virtual void OnCharacterEnter(MatCharacter character)
	{
		AdvancedDebug.LogWarning("OnCharacterEnter:number=" + number);
		SetNumer(Mathf.Clamp(number + 1, 0, 4));
	}

	void SetNumer(int num)
	{
		number = num;
		OnNumberChanged();
	}

	public static int ToNumber(Floor floor)
	{
		return floor.Number;
	}

	public static implicit operator Vector2(Floor floor)
	{
		return floor.centerPoint.transform.position;
	}
}
