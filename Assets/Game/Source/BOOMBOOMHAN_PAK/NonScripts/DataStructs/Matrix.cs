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

	public delegate int SumMethod_Int(T element);

	public int SumByRow(int row, SumMethod_Int method)
	{
		int result = 0;
		for (int i = 0; i < size.Y; i++)
		{
			result += method(tMat[row, i]);
		}
		return result;
	}

	public int SumByCol(int col, SumMethod_Int method)
	{
		int result = 0;
		for (int i = 0; i < size.X; i++)
		{
			result += method(tMat[i, col]);
		}
		return result;
	}

	public bool IsValid(int x, int y)
	{
		if (x < 0 || x >= size.X)
		{
			return false;
		}
		if (y < 0 || y >= size.Y)
		{
			return false;
		}

		return true;
	}

	public bool IsValid(IntVector2D vec)
	{
		return IsValid(vec.X, vec.Y);
	}

	public delegate T ComputeMethod_Matrixes(T a, T b);

	public static Matrix<T> LinearCompute(Matrix<T> a, Matrix<T> b, ComputeMethod_Matrixes method)
	{
		if (a.size != b.size)
		{
			throw new System.Exception("矩阵的行列不同，无法相加减！");
		}

		IntVector2D size = a.size;
		var result = new Matrix<T>(size);
		for (int i = 0; i < size.X; i++)
		{
			for (int j = 0; j < size.Y; j++)
			{
				result[i, j] = method(a[i, j], b[i, j]);
			}
		}

		return result;
	}

	public Matrix<T> SubMatrix(IntVector2D originPoint, IntVector2D size)
	{
		Matrix<T> result = new Matrix<T>(size);
		for (int i = 0; i < size.X; i++)
		{
			for (int j = 0; j < size.Y; j++)
			{
				result[i, j] = tMat[i + originPoint.X, j + originPoint.Y];
			}
		}

		return result;
	}
}
