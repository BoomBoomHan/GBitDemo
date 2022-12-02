using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsPool
{
	private Stack<Bomb> bombStack;

	private GameObject bombResource;

	private Vector2 hiddenPosition;

	public BombsPool()
	{
		bombStack = new Stack<Bomb>();
		bombResource = Resources.Load<GameObject>("Prefabs/Main/Bomb");
		hiddenPosition = new Vector2(99.0f, 99.0f);
		for (int i = 0; i < 10; i++)
		{
			CollectBomb(SpawnBomb());
		}
	}

	public Bomb ApplyBomb(BombSpawnParameters parameters)
	{
		//Debug.Log("Apply " + parameters.LifeSpan);
		Bomb bomb;
		if (bombStack.Count == 0)
		{
			bomb = SpawnBomb();
			bombStack.Push(bomb);
		}

		bomb = bombStack.Pop();
		bomb.Init(parameters);
		return bomb;
	}

	public void CollectBomb(Bomb bomb)
	{
		//Debug.LogWarning("Collect");
		bomb.OnRemove();
		bombStack.Push(bomb);
	}

	private Bomb SpawnBomb() => GameObject.Instantiate(bombResource, hiddenPosition, Quaternion.identity).GetComponent<Bomb>();
}
