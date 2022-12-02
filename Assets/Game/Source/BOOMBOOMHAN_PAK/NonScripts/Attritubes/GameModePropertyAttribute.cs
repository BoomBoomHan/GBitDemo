///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class GameModePropertyAttribute : PropertyAttribute
{
	public string Category;

	public string DisplayName;

	public float SpaceFromLast;

	public string Header;
	
	public GameModePropertyAttribute()
	{
		Category = "默认";
		DisplayName = null;
		SpaceFromLast = 0f;
		Header = null;
	}
	
	/*public GameModePropertyAttribute(string category)
	{
		Category = category;
		DisplayName = null;
	}

	public GameModePropertyAttribute(string category, string displayName)
	{
		Category = category;
		DisplayName = displayName;
	}*/
}
