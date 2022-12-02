using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntVector2D))]
public class IntVectorInspector : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		

		EditorGUI.BeginProperty(position, label, property);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect xLabelRect = new Rect(position.x, position.y + 1, 10, position.height);
		Rect xRect = new Rect(position.x + 16, position.y, 108, position.height);
		Rect yLabelRect = new Rect(position.x + 128, position.y + 1, 10, position.height);
		Rect yRect = new Rect(position.x + 144, position.y, 108, position.height);
		Rect tipRect = new Rect(position.x + 260, position.y, 120, position.height);
		EditorGUI.LabelField(xLabelRect, "X");
		EditorGUI.PropertyField(xRect, property.FindPropertyRelative("X"), GUIContent.none);
		EditorGUI.LabelField(yLabelRect, "Y");
		EditorGUI.PropertyField(yRect, property.FindPropertyRelative("Y"), GUIContent.none);
		EditorGUI.HelpBox(tipRect, "这是一个整型向量。", MessageType.Info);

		EditorGUI.indentLevel = indent;
		
		EditorGUI.EndProperty();
	}
}
