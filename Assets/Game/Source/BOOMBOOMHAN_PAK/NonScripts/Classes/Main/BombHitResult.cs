using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BombHitResult
{
	private int count;

	private List<KeyValuePair<int, bool>> records;

    public BombHitResult(KeyValuePair<int, bool>[] givenResults)
    {
	    count = givenResults.Length;
	    records = givenResults.ToList();
    }

    public bool HasInjure
    {
	    get
	    {
		    foreach (var record in records)
		    {
			    if (!record.Value)
			    {
				    return true;
			    }
		    }

		    return false;
	    }
    }

    public int Count
    {
	    get => count;
    }

    public KeyValuePair<int, bool>[] Records
    {
	    get => records.ToArray();
    }

    public int[] Injures
    {
	    get
	    {
		    List<int> list = new List<int>(4);
		    for (int i = 0; i < records.Count; i++)
		    {
			    if (!records[i].Value)
			    {
				    list.Add(i);
			    }
		    }

		    return list.ToArray();
	    }
    }

    public bool this[int index]
    {
	    get => records[index].Value;
    }
}
