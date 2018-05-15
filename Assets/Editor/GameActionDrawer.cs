using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameAction))]
public class GameActionDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var valueRect = new Rect(position.x, position.y + 20, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"));

        int actionType = property.FindPropertyRelative("type").enumValueIndex;
        switch ((GameAction.ActionType)actionType) {
            case GameAction.ActionType.DamageTarget:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("damageValue"));
                break;
            case GameAction.ActionType.HealTarget:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("healingValue"));
                break;
            default:
                Debug.LogWarning("Unknown action type passed to OnGUI");
                break;
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        const int padding = 10;

        int actionType = property.FindPropertyRelative("type").enumValueIndex;
        switch((GameAction.ActionType)actionType) {
            case GameAction.ActionType.DamageTarget:
                return (EditorGUIUtility.singleLineHeight * 2) + padding;
            case GameAction.ActionType.HealTarget:
                return (EditorGUIUtility.singleLineHeight * 2) + padding;
            default:
                return EditorGUIUtility.singleLineHeight;
        }
    }
}
