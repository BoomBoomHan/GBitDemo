using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatPlayerState : PlayerState
{
    MatCharacter mc;

    bool isMoving;

    protected override void Start()
    {
        base.Start();

        mc = defaultCharacter as MatCharacter;
        mc.MoveBegin.AddListener(SetMoveState);
        mc.MoveEnd.AddListener(ResetMoveState);
    }

    public bool CanMove()
    {
        return IsInRound() && !IsMoving();
    }

    public bool IsInRound()
    {
        return true;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    void SetMoveState(IntVector2D vec)
    {
        AdvancedDebug.Log(mc.Team.ToString() + "Move");
        isMoving = true;
    }

    void ResetMoveState(IntVector2D vec)
    {
		AdvancedDebug.Log(mc.Team.ToString() + "EndMove");
		isMoving = false;
    }
}
