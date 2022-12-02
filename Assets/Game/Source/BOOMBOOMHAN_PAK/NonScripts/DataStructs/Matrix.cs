using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossNode<T>
{
	private T data;

	private CrossNode<T> right;
	private CrossNode<T> down;

	public CrossNode(T dt, CrossNode<T> r = null, CrossNode<T> d = null)
	{
		data = dt;
		right = r;
		down = d;
	}
}

/// <summary>
/// 矩阵1.0版，消耗空间较大
/// </summary>
/// <typeparam name="T"></typeparam>
public class Matrix<T>
{
	private T[,] tMat;

	private IntVector2D size;

	public T this[int x, int y]
	{
		get
		{
			return tMat[x, y];
		}
		set
		{
			tMat[x, y] = value;
		}
	}

	public T this[IntVector2D vec]
	{
		get
		{
			return this[vec.X, vec.Y];
		}
		set
		{
			this[vec.X, vec.Y] = value;
		}
	}

	public ref T Get(int x, int y)
	{
		return ref tMat[x, y];
	}

	public ref T Get(IntVector2D vec)
	{
		return ref Get(vec.X, vec.Y);
	}

	public Matrix(int x, int y)
	{
		size = new IntVector2D(x, y);
		tMat = new T[x, y];
	}

	public Matrix(IntVector2D sz)
	{
		size = sz;
		tMat = new T[size.X, size.Y];
	}

	public IntVector2D Size
	{
		get
		{
			return size;
		}
	}
}
