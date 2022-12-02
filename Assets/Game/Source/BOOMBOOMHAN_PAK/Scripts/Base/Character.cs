using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public abstract class Character : MonoBehaviour
{
	[SerializeField, DisplayName("移动速度")]
	protected float moveSpeed;

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	[SerializeField, DisplayName("跟随相机(未实装)")]
	protected Camera followedCamera;

	public Camera FollowedCamera
	{
		get
		{
			return followedCamera;
		}
	}

	[SerializeField, DisplayName("跟随插值(未实装)")]
	protected float lerpedPercentage;

	public Character()
	{
		moveSpeed = 30f;
		lerpedPercentage = 0.7f;
	}

	public abstract void MoveHorizontal(float val);

	public abstract void MoveVertical(float val);

	public abstract void Jump();

	public abstract void JumpBonus();
}
