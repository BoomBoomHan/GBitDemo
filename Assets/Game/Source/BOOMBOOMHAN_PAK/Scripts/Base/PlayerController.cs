using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Input;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
	//[SerializeField]
	protected Character defaultCharacter;

	protected PlayerState defaultPlayerState;

	[Header("输入轴或操作名称")]
	
	[SerializeField, DisplayName("水平移动轴")]
	private string horizontalAxisName;
	
	[SerializeField, DisplayName("垂直移动轴")]
	private string verticalAxisName;
	
	[SerializeField, DisplayName("跳跃操作")]
	private string jumpActionName;

	private bool allowInput;

	public Character DefaultCharacter
	{
		get => defaultCharacter;
		private set => defaultCharacter = value;
	}

	public PlayerState DefaultPlayerState
	{
		get => defaultPlayerState;
		private set => defaultPlayerState = value;
	}

	public PlayerController()
	{
		defaultCharacter = null;
		horizontalAxisName = "Horizontal";
		verticalAxisName = "Vertical";
		jumpActionName = "Jump";
		allowInput = true;
	}

	protected virtual void Awake()
	{
		
	}

	protected virtual void Start()
	{
		
	}

	protected float horizontalInput
	{
		get
		{
			return GetAxisRaw(horizontalAxisName);
		}
	}
	
	protected float verticalInput
	{
		get
		{
			return GetAxisRaw(verticalAxisName);
		}
	}

	protected virtual void Update()
	{
		InputTickAlways(Time.deltaTime);
		
		if (!allowInput) return;
		
		InputTick(Time.deltaTime);
	}

	protected virtual void FixedUpdate()
	{
		if (!allowInput) return;
		
		InputFixedTick(Time.fixedDeltaTime);
	}

	protected virtual void InputTickAlways(float deltaTime)
	{
		
	}

	protected virtual void InputTick(float deltaTime)
	{
		if (GetButtonDown(jumpActionName)) OnJumpingPressed();
		if (GetButton(jumpActionName)) OnJumpingHeld();
	}

	protected virtual void InputFixedTick(float fixedDeltaTime)
	{
		OnHorizontalInput(horizontalInput);
		OnVerticalInput(verticalInput);
	}

	protected virtual void OnHorizontalInput(float val)
	{
		DefaultCharacter.MoveHorizontal(val);
	}

	protected virtual void OnVerticalInput(float val)
	{
		DefaultCharacter.MoveVertical(val);
	}

	protected virtual void OnJumpingPressed()
	{
		DefaultCharacter.Jump();
	}

	protected virtual void OnJumpingHeld()
	{
		DefaultCharacter.JumpBonus();
	}

	public void BindCharacter(Character mc)
	{
		if (!defaultCharacter)
		{
			defaultCharacter = mc;
			/*if (!mc)
				Debug.Log("BindCharacter  " + mc.name);*/
			return;
		}
		Debug.LogError("Fatal!");
		Debug.LogError("在defaultCharacter已有绑定时尝试绑定其他角色！");
	}

	public void BindPlayerState(PlayerState state)
	{
		if (!defaultPlayerState)
		{
			defaultPlayerState = state;
			return;
		}
		Debug.LogError("Fatal!");
		Debug.LogError("在defaultPlayerState已有绑定时尝试绑定其他PlayerState！");
	}

	public void DisableInput()
	{
		allowInput = false;
	}

	public void EnableInput()
	{
		allowInput = true;
	}
}
