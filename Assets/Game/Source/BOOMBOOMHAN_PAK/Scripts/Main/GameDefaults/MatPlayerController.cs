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

    protected override void Start()
    {
        base.Awake();
        mc = defaultCharacter as MatCharacter;
        state = defaultPlayerState as MatPlayerState;
    }

    protected override void InputTick(float deltaTime)
    {
        //base.InputTick(deltaTime);
        float hor = Input.GetAxisRaw(horizontalAxisName);
        float vert = Input.GetAxisRaw(verticalAxisName);

		if (hor != 0.0f && state.CanMove())
        {
            mc.Move(hor * Vector2.right, 0.5f);
            
        }
        else if (vert != 0.0f && state.CanMove())
        {
            mc.Move(vert * Vector2.up, 0.5f);
        }
    }
}
