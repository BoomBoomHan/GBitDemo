using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyObject
{
	public Sprite SSprite;

	protected static Dictionary<string, Sprite> spritesKvp;

	static SupplyObject()
	{
		spritesKvp = new Dictionary<string, Sprite>()
		{
			{"PowerSupply", Resources.Load<Sprite>("Sprites/Pictures/Main/Supplies/S_PowerSupply")},
			{"IceSupply", Resources.Load<Sprite>("Sprites/Pictures/Main/Supplies/S_Ice")},
			{"FireSupply", Resources.Load<Sprite>("Sprites/Pictures/Main/Supplies/S_Fire")},
		};
	}

	protected static Sprite Get(string sName)
	{
		return spritesKvp[sName];
	}

	public SupplyObject()
	{
		SSprite = null;
	}

	public virtual void TakeChance(MatrixSystem matrixSystem, MatCharacter character, MatPlayerState state)
	{

	}
}

public class PowerSupplyObject : SupplyObject
{
	public PowerSupplyObject()
	{
		SSprite = Get("PowerSupply");
	}

	public override void TakeChance(MatrixSystem matrixSystem, MatCharacter character, MatPlayerState state)
	{
		state.RemainingStepCount += 3;
	}
}

public class PlusSupplyObject : SupplyObject
{
	public PlusSupplyObject()
	{
		SSprite = Get("FireSupply");
	}

	public async override void TakeChance(MatrixSystem matrixSystem, MatCharacter character, MatPlayerState state)
	{
		IntVector2D location = character.Location;
		Floor floor = matrixSystem[location];
		AddNumber(floor);

		await UniTask.Delay(100);
		for (int i = 0; i < matrixSystem.Size.Y; i++)
		{
			if (TryGetFloor(matrixSystem, location + new IntVector2D(1, 0), out Floor dir1))
			{
				AddNumber(dir1);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(-1, 0), out Floor dir2))
			{
				AddNumber(dir2);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(0, 1), out Floor dir3))
			{
				AddNumber(dir3);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(0, -1), out Floor dir4))
			{
				AddNumber(dir4);
			}
			await UniTask.Delay(100);
		}
	}

	private bool TryGetFloor(MatrixSystem matrixSystem, IntVector2D location, out Floor result)
	{
		result = null;
		if (!matrixSystem.FloorMatrix.IsValid(location))
		{
			return false;
		}

		result = matrixSystem[location];
		return true;
	}

	private void AddNumber(Floor floor)
	{
		if (!floor.IsVoid)
		{
			floor.SetNumber(floor.Number + 1);
		}
	}
}

public class MinusSupplyObject : SupplyObject
{
	public MinusSupplyObject()
	{
		SSprite = Get("IceSupply");
	}

	public async override void TakeChance(MatrixSystem matrixSystem, MatCharacter character, MatPlayerState state)
	{
		IntVector2D location = character.Location;
		Floor floor = matrixSystem[location];
		SubNumber(floor);

		await UniTask.Delay(100);
		for (int i = 0; i < matrixSystem.Size.Y; i++)
		{
			if (TryGetFloor(matrixSystem, location + new IntVector2D(1, 0), out Floor dir1))
			{
				SubNumber(dir1);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(-1, 0), out Floor dir2))
			{
				SubNumber(dir2);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(0, 1), out Floor dir3))
			{
				SubNumber(dir3);
			}
			if (TryGetFloor(matrixSystem, location + new IntVector2D(0, -1), out Floor dir4))
			{
				SubNumber(dir4);
			}
			await UniTask.Delay(100);
		}
	}

	private bool TryGetFloor(MatrixSystem matrixSystem, IntVector2D location, out Floor result)
	{
		result = null;
		if (!matrixSystem.FloorMatrix.IsValid(location))
		{
			return false;
		}

		result = matrixSystem[location];
		return true;
	}

	private void SubNumber(Floor floor)
	{
		if ((floor.Number > 0) && (!floor.IsVoid))
		{
			floor.SetNumber(floor.Number - 1);
		}
	}
}