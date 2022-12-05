using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
	public static void GetPlayerStartPositions(IntVector2D size, out IntVector2D p1Start, out IntVector2D p2Start)
	{
		p1Start = new IntVector2D(size.X - 1, 0);
		p2Start = new IntVector2D(0, size.Y - 1);
	}

	public static bool IsValidMove(MatrixSystem map, 
		IntVector2D currentLocation, IntVector2D moveDirection, bool blockWallExists, bool characterHasSupplies)
	{
		IntVector2D endLocation = currentLocation + moveDirection;
		if (!map.FloorMatrix.IsValid(endLocation))
		{
			return false;
		}
		var floor = map[endLocation];
		if (floor.HasCharaterUpon)
		{
			return false;
		}
		if (floor.IsVoid)
		{
			return false;
		}
		var sf = floor as SupplyFloor;
		if (sf && !sf.CanCharacterEnter(characterHasSupplies))
		{
			return false;
		}
		if (blockWallExists)
		{
			float blockY = (map.Size.Y - 1.0f) / 2.0f;
			float y1 = currentLocation.Y - blockY;
			float y2 = y1 + moveDirection.Y;
			if (y1 * y2 < 0.0f)
			{
				return false;
			}
		}

		return true;
	}

	private static Matrix<Floor> blueMap;
	private static Matrix<Floor> redMap;
	
	private static Matrix<Floor> GetMap(EPlayerTeam team)
	{
		return team == EPlayerTeam.Blue ? blueMap : redMap;
	}
	private static IntVector2D ToSubMapLocation(IntVector2D globalLocation, int mapSizeY, EPlayerTeam team)
	{
		return team == EPlayerTeam.Blue ? globalLocation : globalLocation - new IntVector2D(0, mapSizeY / 2);
	}

	class LocationQueue
	{
		public Queue<int> XQueue;
		public Queue<int> YQueue;

		public LocationQueue()
		{
			XQueue = new Queue<int>(3);
			YQueue = new Queue<int>(3);
		}
	}

	private static LocationQueue blueQueue;
	private static LocationQueue redQueue;

	private static LocationQueue GetQueue(EPlayerTeam team)
	{
		return team == EPlayerTeam.Blue ? blueQueue : redQueue;
	}

	static Game()
	{
		blueQueue = new LocationQueue();
		redQueue = new LocationQueue();
	}

	//生成的是全局坐标
	public static IntVector2D GeneratePoint(MatrixSystem map, EPlayerTeam team)
	{
		var queue = GetQueue(team);
		var xSet = new HashSet<int>(queue.XQueue);
		var ySet = new HashSet<int>(queue.YQueue);
		System.Random random = new System.Random();

		int x, y;
		do
		{
			x = random.Next(0, map.Size.X);
		} while (xSet.Contains(x));
		do
		{
			y = random.Next(0, map.Size.Y / 2);
		} while (ySet.Contains(y));

		queue.XQueue.Enqueue(x);
		queue.YQueue.Enqueue(y);

		if (queue.XQueue.Count > 2)
		{
			queue.XQueue.Dequeue();
		}
		if (queue.YQueue.Count > 2)
		{
			queue.YQueue.Dequeue();
		}

		return new IntVector2D(x, y);
	}

	public static int ComputeDamage(MatrixSystem map, EPlayerTeam team, IntVector2D globalLocation)
	{
		if (blueMap == null || redMap == null)
		{
			blueMap = map.FloorMatrix.SubMatrix(new IntVector2D(0, 0), new IntVector2D(map.Size.X, map.Size.Y / 2));
			redMap = map.FloorMatrix.SubMatrix(new IntVector2D(0, map.Size.Y / 2), new IntVector2D(map.Size.X, map.Size.Y / 2));
		}

		var subMap = GetMap(team);
		IntVector2D subLocation = ToSubMapLocation(globalLocation, map.Size.Y, team);

		return subMap.SumByRow(subLocation.X, Floor.ToNumber)
			+ subMap.SumByCol(subLocation.Y, Floor.ToNumber)
			- subMap[subLocation].Number;
	}

	private static SupplyObject GenerateSupply()
	{
		System.Random random = new System.Random();
		int lIndex = random.Next(0, 3);
		switch (lIndex)
		{
		case 0:
			return new PowerSupplyObject();
		case 1:
			return new PlusSupplyObject();
		case 2:
			return new MinusSupplyObject();
		default:
			return new PowerSupplyObject();
		}
	}

	public static void GenerateSupplies(SupplyFloor[] lefts, SupplyFloor[] rights, int count = 1)
	{
		System.Random random = new System.Random();
		int[] lIndexes = new int[2];
		int[] rIndexes = new int[2];
		lIndexes[0] = random.Next(0, lefts.Length); 
		rIndexes[0] = random.Next(0, rights.Length);

		do
		{
			lIndexes[1] = random.Next(0, lefts.Length);
		} while (lIndexes[1] == lIndexes[0]);
		do
		{
			rIndexes[1] = random.Next(0, rights.Length);
		} while (rIndexes[1] == rIndexes[0]);

		/*int lIndex = random.Next(0, lefts.Length);
		int rIndex = random.Next(0, rights.Length);

		SupplyFloor lFloor = lefts[lIndex];
		SupplyFloor rFloor = rights[rIndex];*/

		lefts[lIndexes[0]].SObject = GenerateSupply();
		rights[rIndexes[0]].SObject = GenerateSupply();

		if (count == 2)
		{
			lefts[lIndexes[1]].SObject = GenerateSupply();
			rights[rIndexes[1]].SObject = GenerateSupply();
		}

		AdvancedDebug.Log(lefts[lIndexes[0]].Coord.ToString() + (lefts[lIndexes[0]].SObject.SSprite != null));
		AdvancedDebug.Log(rights[rIndexes[0]].Coord.ToString() + (rights[rIndexes[0]].SObject.SSprite != null));
		/*lFloor.SObject = GenerateSupply();
		rFloor.SObject = GenerateSupply();*/
	}
	
	public static IntVector2D ToMirrorLocation(MatrixSystem map, IntVector2D globalLocation)
	{
		return new IntVector2D(globalLocation.X, map.Size.Y - 1 - globalLocation.Y);
	}

	public static bool IsInDetention(MatrixSystem map, IntVector2D playerLocation, bool hasWall, bool characterHasSupply)
	{
		bool b1 = IsValidMove(map, playerLocation, IntVector2D.Up, hasWall, characterHasSupply);
		bool b2 = IsValidMove(map, playerLocation, IntVector2D.Left, hasWall, characterHasSupply);
		bool b3 = IsValidMove(map, playerLocation, IntVector2D.Down, hasWall, characterHasSupply);
		bool b4 = IsValidMove(map, playerLocation, IntVector2D.Right, hasWall, characterHasSupply);
		bool canMove = b1 || b2 || b3 || b4;
			
		if (!canMove)
		{
			AdvancedDebug.LogWarning($"{b1},{b2},{b3},{b4}");
		}

		return !canMove;
	}
}
