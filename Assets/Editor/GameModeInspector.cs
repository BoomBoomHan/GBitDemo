///
///
///

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

public struct DisplayedProperty
{
	public SerializedProperty Property;
	
	public GameModePropertyAttribute PropertyAttribute;
	public string Category { get; private set; }
	public string DisplayName { get; private set; }
	
	public float SpaceFromLast { get; private set; }

	public DisplayedProperty(SerializedProperty property, GameModePropertyAttribute attribute)
	{
		Property = property;
		PropertyAttribute = attribute;
		
		Category = "默认";
		DisplayName = null;
		SpaceFromLast = 0f;
		
		if (PropertyAttribute != null)
		{
			Category = PropertyAttribute.Category;
			DisplayName = PropertyAttribute.DisplayName;
			SpaceFromLast = PropertyAttribute.SpaceFromLast;
		}
	}
}

[CustomEditor(typeof(GameModeBase), false), CanEditMultipleObjects]
public class GameModeInspector : Editor
{
	protected GameModeBase gameModeBase;

	protected int toolbarId = 0;

	protected string[] options = {"绑定", "默认"};

	protected Dictionary<string, List<DisplayedProperty>> cpDict = new Dictionary<string, List<DisplayedProperty>>()
	{
		{"绑定", new List<DisplayedProperty>()},
		{"默认", new List<DisplayedProperty>()},
	};

	protected virtual void OnEnable()
	{
		gameModeBase = target as GameModeBase;

		/*tfDict = new Dictionary<int, UnityAction>()
		{
			{0, OnDefaultSettings},
			{1, OnOtherSettings},
		};*/

		GetAllFields(out var fields);
		foreach (var field in fields)
		{
			string category = field.Category;
			if (cpDict.ContainsKey(category))
			{
				cpDict[category].Add(field);
			}
			else
			{
				cpDict.Add(category, new List<DisplayedProperty>(){field});
			}
		}
		
		options = new string[cpDict.Count];
		cpDict.Keys.CopyTo(options, 0);
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		EditorGUILayout.Space();
		
		toolbarId = GUILayout.Toolbar(toolbarId, options, GUILayout.Width(300f), GUILayout.Height(35f), GUILayout.ExpandWidth(true));
		
		EditorGUILayout.Space(20f);

		var fields = cpDict[options[toolbarId]];

		foreach (var field in fields)
		{
			if (field.Property == null) continue;
			/*GUIContent content = field.DisplayName != null ? new GUIContent(field.DisplayName) : GUIContent.none;
			EditorGUILayout.Space(field.SpaceFromLast);
			EditorGUILayout.PropertyField(field.Property, content);*/
			DrawProperty(field.Property, field.PropertyAttribute);
		}

		serializedObject.ApplyModifiedProperties();
	}

	void DrawProperty(SerializedProperty property, GameModePropertyAttribute attribute)
	{
		if (attribute == null)
		{
			PropertyField(property);
		}
		else
		{
			Space(attribute.SpaceFromLast);
			if (attribute.Header != null)
			{
				LabelField(attribute.Header, EditorStyles.boldLabel);
			}

			PropertyField(property,
				attribute.DisplayName == null ? GUIContent.none : new GUIContent(attribute.DisplayName));
		}
	}

	protected virtual Type[] types
	{
		get
		{
			return new[] {typeof(GameModeBase)};
		}
	}

	protected void GetAllFields(out List<DisplayedProperty> fields)
	{
		fields = new List<DisplayedProperty>(20);
		HashSet<string> names = new HashSet<string>();

		foreach (var type in types)
		{
			
			var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			string infos = "";
			foreach (var info in fieldInfos)
			{
				infos += info.Name + ",";
			}
			//Debug.LogError(infos);

			foreach (var info in fieldInfos)
			{
				var att = info.GetCustomAttribute<GameModePropertyAttribute>();
				string infoName = info.Name;
				
				if (names.Contains(infoName)) continue;
				names.Add(infoName);
				
				string category = "默认";
				string displayName = infoName;
				if (att != null)
				{
					category = att.Category != null ? att.Category : category;
					displayName = att.DisplayName != null ? att.DisplayName : displayName;
				}

				var prop = serializedObject.FindProperty(info.Name);
				if (prop != null)
				{
					fields.Add(new DisplayedProperty(prop, att));
					//Debug.LogWarning($"{info.Name},{fields[fields.Count - 1].DisplayName}");
				}

			}
		}
		
	}
}