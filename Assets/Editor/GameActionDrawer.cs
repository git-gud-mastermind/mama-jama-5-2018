using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameAction))]
public class GameActionDrawer : PropertyDrawer {

    private const string ACTION_TYPE_PROPERTY = "type";
    private const string DAMAGE_VALUE_PROPERTY = "damageValue";
    private const string HEALING_VALUE_PROPERTY = "healingValue";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property);

        if (property.isExpanded) {
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float lineHeight = EditorGUIUtility.singleLineHeight;
            var typeRect = new Rect(position.x, position.y, position.width, lineHeight);
            var valueRect = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);

            SerializedProperty typeProperty = property.FindPropertyRelative(ACTION_TYPE_PROPERTY);
            EditorGUI.PropertyField(typeRect, typeProperty, true);

            int actionType = typeProperty.enumValueIndex;
            switch ((GameAction.ActionType)actionType) {
                case GameAction.ActionType.DamageTarget:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(DAMAGE_VALUE_PROPERTY));
                    break;
                case GameAction.ActionType.HealTarget:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(HEALING_VALUE_PROPERTY));
                    break;
                default:
                    Debug.LogWarning("Unknown action type passed to OnGUI");
                    break;
            }

            EditorGUI.indentLevel = indent;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        if (!property.isExpanded) {
            return EditorGUIUtility.singleLineHeight;
        }
        
        const int padding = 10;

        int actionType = property.FindPropertyRelative(ACTION_TYPE_PROPERTY).enumValueIndex;
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
