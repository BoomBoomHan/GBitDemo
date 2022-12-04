using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
	public static void GetPlayerStartPositions(IntVector2D size, out IntVector2D p1Start, out IntVector2D p2Start)
	{
		p1Start = new IntVector2D(size.X - 1, 0);
		p2Start = new IntVector2D(0, size.Y - 1);
	}

	public static bool IsValidMove(MatrixSystem map, 
		IntVector2D currentLocation, IntVector2D moveDirection, bool blockWallExists)
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
	public static IntVector2D GeneratePoint(Matrix<Floor> map, EPlayerTeam team)
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
		queue.XQueue.Dequeue();
		queue.YQueue.Dequeue();

		return new IntVector2D(x, y);
	}

	public static int ComputeDamage(Matrix<Floor> map, EPlayerTeam team, IntVector2D globalLocation)
	{
		if (blueMap == null || redMap == null)
		{
			blueMap = map.SubMatrix(new IntVector2D(0, 0), new IntVector2D(map.Size.X, map.Size.Y / 2));
			redMap = map.SubMatrix(new IntVector2D(0, map.Size.Y / 2), new IntVector2D(map.Size.X, map.Size.Y / 2));
		}

		var subMap = GetMap(team);
		IntVector2D subLocation = ToSubMapLocation(globalLocation, map.Size.Y, team);

		return subMap.SumByRow(subLocation.X, Floor.ToNumber)
			+ subMap.SumByCol(subLocation.Y, Floor.ToNumber)
			- subMap[subLocation].Number;
	}
}
