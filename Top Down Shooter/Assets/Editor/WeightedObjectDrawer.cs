﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeightedItemDrop.WeightedObject))]
public class WeightedObjectDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var itemRect = new Rect(position.x, position.y, position.width - 40f, position.height);
        var weightRect = new Rect(position.x + position.width - 40f, position.y, 40f, position.height);

        EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("item"), GUIContent.none);
        EditorGUI.PropertyField(weightRect, property.FindPropertyRelative("weight"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}