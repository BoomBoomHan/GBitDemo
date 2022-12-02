
using System;
using UnityEditor;

///
///
///

[CustomEditor(typeof(MatGameModeBase), false), CanEditMultipleObjects]
public class MatGameModeBaseInspector : GameModeInspector
{
	protected override Type[] types => new[] { typeof(GameModeBase), typeof(MatGameModeBaseInspector) }; 
}