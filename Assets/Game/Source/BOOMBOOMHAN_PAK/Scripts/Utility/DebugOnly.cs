///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnly : MonoBehaviour
{
	private void Start()
	{
		if (!Developer.InTest)
		{
			Destroy(gameObject);
		}
	}
}
