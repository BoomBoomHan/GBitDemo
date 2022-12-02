using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MusicProperty))]
public class MusicPropertyInspector : PropertyDrawer
{
	private float height = 2.5f;
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect xLabelRect = new Rect(position.x, position.y, 30, position.height / height);
		Rect xRect = new Rect(position.x + 90, position.y, 50, position.height / height);
		Rect yLabelRect = new Rect(position.x + 150, position.y, 80, position.height / height);
		Rect yRect = new Rect(position.x + 240, position.y, 50, position.height / height);

		float distance = 22.0f;
		Rect zLabelRect = new Rect(position.x, position.y + distance, 80, position.height / height);
		Rect zRect = new Rect(position.x + 90, position.y + distance, 50, position.height / height);
		Rect wLabelRect = new Rect(position.x + 150, position.y + distance, 80, position.height / height);
		Rect wRect = new Rect(position.x + 240, position.y + distance, 50, position.height / height);
		EditorGUI.LabelField(xLabelRect, "BPM");
		EditorGUI.PropertyField(xRect, property.FindPropertyRelative("bpm"), GUIContent.none);
		EditorGUI.LabelField(yLabelRect, "播放毫秒延迟");
		EditorGUI.PropertyField(yRect, property.FindPropertyRelative("playDelay"), GUIContent.none);
		EditorGUI.LabelField(zLabelRect, "音乐分数分子");
		EditorGUI.PropertyField(zRect, property.FindPropertyRelative("molecule"), GUIContent.none);
		EditorGUI.LabelField(wLabelRect, "音乐分数分母");
		EditorGUI.PropertyField(wRect, property.FindPropertyRelative("deno"), GUIContent.none);
		
		//EditorGUI.HelpBox(tipRect, "这是一个整型向量。", MessageType.Info);

		EditorGUI.indentLevel = indent;
		
		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) * height;
	}
}
