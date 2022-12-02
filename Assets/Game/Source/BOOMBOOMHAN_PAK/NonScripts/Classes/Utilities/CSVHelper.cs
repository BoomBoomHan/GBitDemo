using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class CsvHelper
{
	private static string ToPath(string csvName) => $"{Application.dataPath}/Brother/Datas/{csvName}.csv";

	public static bool TryCreateDataDirectory(string fmt)
	{
		if (Directory.Exists($"{Application.dataPath}/Brother/Datas/{fmt}"))
		{
			return false;
		}

		Directory.CreateDirectory($"{Application.dataPath}/Brother/Datas/{fmt}");
		return true;
	}
	
	public static string CreatorCsv(string csvName, params string[] cows)
	{
		Debug.Assert(cows != null && cows.Length != 0);

		string full = ToPath(csvName);
		Debug.Assert(!File.Exists(full), $"已存在文件{full}");

		using (var stream = File.Create(full))
		{
			using (var writer = new StreamWriter(stream, Encoding.UTF8))
			{
				writer.Write(cows[0]);
				for (int i = 1; i < cows.Length; i++)
				{
					writer.Write("," + cows[i]);
				}
			}
		}
		return full;
		/*Debug.Log(Application.dataPath);
		return "";*/
	}

	public static string[ , ] LoadFromCsv(string csvName)
	{
		string full = ToPath(csvName);
		Debug.Assert(File.Exists(full), $"不存在文件{full}");
		
		string[] lines = File.ReadAllLines(full);
		string[] firstLineElements = lines[0].Split(',');
		
		string[,] result = new string[lines.Length, firstLineElements.Length];
		
		for (int i = 0; i < firstLineElements.Length; i++)
		{
			result[0, i] = firstLineElements[i];
		}

		for (int i = 1; i < lines.Length; i++)
		{
			string[] lineElements = lines[i].Split(',');
			//AdvancedDebug.LogWarning($"{i}	{lineElements.Length}");
			for (int j = 0; j < lineElements.Length; j++)
			{
				result[i, j] = lineElements[j];	
			}
		}

		return result;
	}
}
