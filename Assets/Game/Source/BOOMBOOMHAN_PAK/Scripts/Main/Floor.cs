using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public IntVector2D Coord;

	private int number;

	public EPlayerTeam Team
	{
		get;set;
	}

	public virtual int Number
	{
		get => IsVoidAndFall ? 0 : number;
		set => number = value;
	}

	[SerializeField, DisplayName("中心点")]
	Transform centerPoint;

	[SerializeField, DisplayName("图片Sprite")]
	SpriteRenderer pictureRenderer;

	[SerializeField, DisplayName("图片Sprite")]
	SpriteRenderer maskRenderer;

	[SerializeField, DisplayName("个位数Sprite")]
	SpriteRenderer unitRenderer;

	public bool HasCharaterUpon
	{
		get;set;
	}

	public Floor()
	{
		Team = EPlayerTeam.Blue;
	}

	private static Sprite[] numberSprites;

	private static Sprite voidSprite;
	private static GameObject empty;

	private static int alphaCode;

	protected virtual void Awake()
	{
		if (numberSprites != null)
		{
			return;
		}

		numberSprites = new Sprite[10];
		for (int i = 0; i <= 4; i++)
		{
			numberSprites[i] = Resources.Load<Sprite>($"Sprites/Pictures/Main/Numbers/Number{i}");
		}
		voidSprite = Resources.Load<Sprite>("Sprites/Pictures/Main/Floors/S_VoidFloor");
		empty = Resources.Load<GameObject>("Prefabs/Floor/EmptyFloor");
		alphaCode = Shader.PropertyToID("_Alpha");

		//TimerManager.GetTimerManager().SetTimer(() => AdvancedDebug.LogError("666"), 0.0f, 1.0f, 1L);
	}

	protected virtual void Update()
	{
		if (IsVoidAndFall)
		{
			return;
		}

		Vector3 target = transform.position;
		target.z = MatrixSystem.ZLocation;
		transform.position = target;
	}

	void OnNumberChanged(MatCharacter character)
	{
		//AdvancedDebug.LogWarning("OnNumberChanged");

		void TurnToVoid(IntVector2D location)
		{
			AdvancedDebug.LogError("VOID");
			TurnToVoidImpl();
			character.MoveEnd.RemoveListener(TurnToVoid);
		}

		/*void UnleashListener(IntVector2D location)
		{

		}*/
		
		Color targetColor = Color.white;
		targetColor.a = 0.85f;
		unitRenderer.color = targetColor;
		unitRenderer.sprite = numberSprites[number];
		if (IsVoid && character)
		{
			character.MoveEnd.AddListener(TurnToVoid);
		}
	}

	private async void TurnToVoidImpl()
	{
		maskRenderer.enabled = false;
		unitRenderer.enabled = false;
		pictureRenderer.sprite = voidSprite;
		pictureRenderer.transform.localScale = new Vector3(0.135f, 0.135f, 0.135f);
		pictureRenderer.material.SetFloat(alphaCode, 1.0f);

		await UniTask.Delay(100);
		var result = transform.DOMoveZ(36.0f, 32.0f);
		result.onComplete += Disappear;
		ReplaceToEmpty();
	}

	void Disappear()
	{
		pictureRenderer.enabled = false;
	}

	public virtual void OnCharacterEnter(MatCharacter character)
	{
		//AdvancedDebug.LogWarning("OnCharacterEnter:number=" + number);
		SetNumber(Mathf.Clamp(number + 1, 0, 4), character);
	}

	public void SetNumber(int num, MatCharacter character = null)
	{
		int tmp = number;
		number = num;
		if (tmp != number)
		{
			OnNumberChanged(character);
		}
	}

	public bool IsVoid
	{
		get => number == 4;
	}

	public bool IsVoidAndFall
	{
		get => IsVoid && pictureRenderer.sprite == voidSprite;
	}

	public static int ToNumber(Floor floor)
	{
		return floor.Number;
	}

	public static implicit operator Vector2(Floor floor)
	{
		return floor.centerPoint.transform.position;
	}

	public void TurnBright()
	{
		maskRenderer.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.95f), 1.0f);
	}

	public void TurnNormal()
	{
		maskRenderer.DOColor(Color.clear, 1.0f);
	}

	public void ReplaceToEmpty()
	{
		GameObject go = Instantiate(empty, transform.position, Quaternion.identity);
		var floor = go.GetComponent<Floor>();
		floor.number = 4;
		if (maskRenderer.color == Color.white)
		{
			floor.maskRenderer.color = Color.white;
		}
		GameModeBase.Get<MatGameModeBase>().MatSystem.FloorMatrix[Coord] = floor;
		//GameModeBase.Get<MatGameModeBase>().MatSystem[]
	}
}
