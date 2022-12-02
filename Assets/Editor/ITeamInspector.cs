using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.EditorGUILayout;

[CustomEditor(typeof(ITeam))]
public class ITeamInspector : Editor
{
	private ITeam iteam;
	
	private SerializedProperty teamType;

	private void OnEnable()
	{
		iteam = target as ITeam;
		teamType = serializedObject.FindProperty("teamType");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		PropertyField(teamType, new GUIContent("所属阵营"));

		serializedObject.ApplyModifiedProperties();
	}
}