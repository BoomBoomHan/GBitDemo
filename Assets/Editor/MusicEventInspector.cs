using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AddBombEventCreator))]
public class AddBombEventInspector : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect xLabelRect = new Rect(position.x, position.y, 30, position.height);
		Rect xRect = new Rect(position.x + 29, position.y, 33, position.height);
		Rect yLabelRect = new Rect(position.x + 63, position.y, 40, position.height);
		Rect yRect = new Rect(position.x + 105, position.y, 33, position.height);
		
		Rect zLabelRect = new Rect(position.x + 140, position.y, 30, position.height);
		Rect zRect = new Rect(position.x + 168, position.y, 33, position.height);
		Rect wLabelRect = new Rect(position.x + 203, position.y, 40, position.height);
		Rect wRect = new Rect(position.x + 232, position.y, 33, position.height);
		EditorGUI.LabelField(xLabelRect, "小节");
		EditorGUI.PropertyField(xRect, property.FindPropertyRelative("ApproachSection"), GUIContent.none);
		EditorGUI.LabelField(yLabelRect, "到达拍");
		EditorGUI.PropertyField(yRect, property.FindPropertyRelative("ApproachBeat"), GUIContent.none);
		EditorGUI.LabelField(zLabelRect, "拍长");
		EditorGUI.PropertyField(zRect, property.FindPropertyRelative("BeatLength"), GUIContent.none);
		EditorGUI.LabelField(wLabelRect, "轨道");
		EditorGUI.PropertyField(wRect, property.FindPropertyRelative("TrackOrder"), GUIContent.none);
		//EditorGUI.HelpBox(tipRect, "这是一个整型向量。", MessageType.Info);

		EditorGUI.indentLevel = indent;
		
		EditorGUI.EndProperty();
	}
}

//[CustomPropertyDrawer(typeof(ChangeBackgroundColorEventCreator))]
public class ChangeBackgroundColorEventInspector : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect xLabelRect = new Rect(position.x, position.y, 30, position.height);
		Rect xRect = new Rect(position.x + 29, position.y, 33, position.height);
		Rect yLabelRect = new Rect(position.x + 63, position.y, 20, position.height);
		Rect yRect = new Rect(position.x + 80, position.y, 33, position.height);
		
		Rect zLabelRect = new Rect(position.x + 115, position.y, 42, position.height);
		Rect zRect = new Rect(position.x + 158, position.y, 33, position.height);
		Rect wLabelRect = new Rect(position.x + 193, position.y, 40, position.height);
		Rect wRect = new Rect(position.x + 223, position.y, 53, position.height);
		EditorGUI.LabelField(xLabelRect, "小节");
		EditorGUI.PropertyField(xRect, property.FindPropertyRelative("BeginSection"), GUIContent.none);
		EditorGUI.LabelField(yLabelRect, "拍");
		EditorGUI.PropertyField(yRect, property.FindPropertyRelative("BeginBeat"), GUIContent.none);
		EditorGUI.LabelField(zLabelRect, "拍持续");
		EditorGUI.PropertyField(zRect, property.FindPropertyRelative("BeatDuration"), GUIContent.none);
		EditorGUI.LabelField(wLabelRect, "颜色");
		EditorGUI.PropertyField(wRect, property.FindPropertyRelative("TargetColor"), GUIContent.none);

		EditorGUI.indentLevel = indent;
		
		EditorGUI.EndProperty();
	}
}

[CustomPropertyDrawer(typeof(GameOverEventCreator))]
public class GameOverEventInspector : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect xLabelRect = new Rect(position.x, position.y, 30, position.height);
		Rect xRect = new Rect(position.x + 29, position.y, 63, position.height);
		Rect yLabelRect = new Rect(position.x + 123, position.y, 20, position.height);
		Rect yRect = new Rect(position.x + 140, position.y, 63, position.height);
		
		EditorGUI.LabelField(xLabelRect, "小节");
		EditorGUI.PropertyField(xRect, property.FindPropertyRelative("Section"), GUIContent.none);
		EditorGUI.LabelField(yLabelRect, "拍");
		EditorGUI.PropertyField(yRect, property.FindPropertyRelative("Beat"), GUIContent.none);

		EditorGUI.indentLevel = indent;
		
		EditorGUI.EndProperty();
	}
}