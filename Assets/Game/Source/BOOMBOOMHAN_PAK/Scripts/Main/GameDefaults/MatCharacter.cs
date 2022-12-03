using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatCharacter : Character2D
{
    IntVector2D location;

    private EPlayerTeam team;

    public EPlayerTeam Team
    {
        get { return team; }
        set { team = value; }
    }

    public UnityEvent<IntVector2D> MoveBegin;

    public UnityEvent<IntVector2D> MoveEnd;

    protected override void Awake()
    {
        base.Awake();

        MoveBegin = new UnityEvent<IntVector2D>();
        MoveEnd = new UnityEvent<IntVector2D>();

        MoveBegin.AddListener(OnMoveBegin);
        MoveEnd.AddListener(OnMoveEnd);
    }

    public void Move(Vector2 delta, float duration)
    {
        MoveBegin.Invoke(location);
        var dt = transform.DOMove(transform.position + (Vector3)delta, duration);
        dt.onComplete += EndMove;
    }

    private void EndMove()
    {
        MoveEnd.Invoke(location);
    }

    private void OnMoveBegin(IntVector2D vec)
    {

    }

	private void OnMoveEnd(IntVector2D vec)
	{

	}
}
