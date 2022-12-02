using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnParameters
{
	public float Speed;

	public float LifeSpan;

	public int TrackIndex;

	public BombSpawnParameters(float speed, float lifeSpan, int tIndex)
	{
		Speed = speed;
		LifeSpan = lifeSpan;
		TrackIndex = tIndex;
	}
}
