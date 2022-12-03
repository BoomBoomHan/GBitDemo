using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatPlayerController : PlayerController
{
	public string HorizontalAxisName
	{
		set => horizontalAxisName = value; 
	}
	public string VerticalAxisName
	{
		set => verticalAxisName = value; 
	}

	private MatCharacter mc;
	private MatPlayerState state;
	private MatGameState gameState;

	private MatrixSystem matrixSystem;

	protected override void Start()
	{
		base.Start();

		mc = defaultCharacter as MatCharacter;
		state = defaultPlayerState as MatPlayerState;
		gameState = GameModeBase.Get<MatGameModeBase>().DefaultGameState as MatGameState;
		matrixSystem = GameModeBase.Get<MatGameModeBase>().MatSystem;

		DisableInput();
	}

	protected override void InputTick(float deltaTime)
	{
		//base.InputTick(deltaTime);
		float hor = Input.GetAxisRaw(horizontalAxisName);
		float vert = Input.GetAxisRaw(verticalAxisName);

		if (hor != 0.0f && state.CanMove())
		{
			//mc.Move(hor * matrixSystem.Distance * Vector2.right, 0.5f);
			IntVector2D moveDirection = Mathf.RoundToInt(hor) * IntVector2D.Up;
			bool valid = Game.IsValidMove(matrixSystem, mc.Location,
				moveDirection, gameState.DoesBlockExist());
			if (valid)
			{
				mc.MoveTo(mc.Location + moveDirection, 0.5f);
			}
			else
			{
				AdvancedDebug.LogWarning($"{mc.Team} Invalid Move");
			}
		}
		else if (vert != 0.0f && state.CanMove())
		{
			//mc.Move(vert * matrixSystem.Distance * Vector2.up, 0.5f);
			IntVector2D moveDirection = Mathf.RoundToInt(vert) * IntVector2D.Left;
			bool valid = Game.IsValidMove(matrixSystem, mc.Location,
				moveDirection, gameState.DoesBlockExist());
			if (valid)
			{
				mc.MoveTo(mc.Location + moveDirection, 0.5f);
			}
			else
			{
				AdvancedDebug.LogWarning($"{mc.Team} Invalid Move");
			}
		}
	}

	protected override void InputFixedTick(float fixedDeltaTime)
	{
		
	}
}
