using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IntVector2D
{
	public int X;
	public int Y;

	public IntVector2D(int x = 0, int y = 0)
	{
		X = x;
		Y = y;
	}
	
	public IntVector2D(Vector2 vec)
	{
		X = Mathf.RoundToInt(vec.x);
		Y = Mathf.RoundToInt(vec.y);
	}

	public bool IsZero
	{
		get
		{
			return (X == 0) && (Y == 0);
		}
	}

	public int SqrLength
	{
		get
		{
			return X * X + Y * Y;
		}
	}

	public float Length
	{
		get
		{
			return Mathf.Sqrt(SqrLength);
		}
	}

	public float Angle
	{
		get
		{
			return Mathf.Atan2(Y, X) + (X < 0 ? Mathf.PI : 0f);
		}
	}
	
	#region OPERATORS
	
	public static implicit operator bool(IntVector2D vec)
	{
		return !vec.IsZero;
	}

	public static explicit operator float(IntVector2D vec)
	{
		return vec.Length;
	}
	
	public static implicit operator Vector2(IntVector2D vec)
	{
		return new Vector2(vec.X, vec.Y);
	}

	public override string ToString()
	{
		return string.Format($"({X},{Y})");
	}

	public static IntVector2D operator +(IntVector2D vecA, IntVector2D vecB)
	{
		return new IntVector2D(vecA.X + vecB.X, vecA.Y + vecB.Y);
	}
	
	public static IntVector2D operator -(IntVector2D vecA, IntVector2D vecB)
	{
		return new IntVector2D(vecA.X - vecB.X, vecA.Y - vecB.Y);
	}

	public static IntVector2D operator *(IntVector2D vec, int val)
	{
		return new IntVector2D(vec.X * val, vec.Y * val);
	}
	
	public static IntVector2D operator *(int val, IntVector2D vec)
	{
		return vec * val;
	}

	public static Vector2 operator *(IntVector2D vec, float val)
	{
		return new Vector2(vec.X * val, vec.Y * val);
	}

	public static int operator *(IntVector2D vecA, IntVector2D vecB)
	{
		return vecA.X * vecB.X + vecA.Y * vecB.Y;
	}

	public static Vector2 operator /(IntVector2D vec, float val)
	{
		return new Vector2(vec.X / val, vec.Y / val);
	}

	public static bool operator ==(IntVector2D vecA, IntVector2D vecB)
	{
		return vecA.X == vecB.X && vecA.Y == vecB.Y;
	}
	
	public static bool operator !=(IntVector2D vecA, IntVector2D vecB)
	{
		return vecA.X != vecB.X || vecA.Y != vecB.Y;
	}

	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}

	public override bool Equals(object obj)
	{
		IntVector2D casted = (IntVector2D) obj;
		return casted != null && casted == this;
	}

	#endregion

	#region STATIC_PROPERTIES

	public static IntVector2D Zero
	{
		get
		{
			return new IntVector2D();
		}
	}
	
	public static IntVector2D One
	{
		get
		{
			return new IntVector2D(1, 1);
		}
	}
	
	public static IntVector2D Right
	{
		get
		{
			return new IntVector2D(1);
		}
	}
	
	public static IntVector2D Left
	{
		get
		{
			return new IntVector2D(-1);
		}
	}
	
	public static IntVector2D Up
	{
		get
		{
			return new IntVector2D(0, 1);
		}
	}
	
	public static IntVector2D Down
	{
		get
		{
			return new IntVector2D(0, -1);
		}
	}
	
	#endregion
}