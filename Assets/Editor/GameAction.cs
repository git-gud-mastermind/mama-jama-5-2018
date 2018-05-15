using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameAction))]
public class GameActionEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        GameAction action = GetActionInstanceFromProperty(property);
        if (action == null) {
            return;
        }

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var valueRect = new Rect(position.x, position.y + 20, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"));
        switch (action.type) {
            case GameAction.ActionType.DamageTarget:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("damageValue"));
                break;
            case GameAction.ActionType.HealTarget:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("healingValue"));
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        GameAction action = GetActionInstanceFromProperty(property);
        if (action == null) {
            return 0;
        }

        const int padding = 10;

        switch(action.type) {
            case GameAction.ActionType.DamageTarget:
                return (EditorGUIUtility.singleLineHeight * 2) + padding;
            case GameAction.ActionType.HealTarget:
                return (EditorGUIUtility.singleLineHeight * 2) + padding;
            default:
                return EditorGUIUtility.singleLineHeight;
        }
    }

    private GameAction GetActionInstanceFromProperty(SerializedProperty property) {
        // Parse the target array and index of this property from its path
        string propertyPath = property.propertyPath;
        string listName = propertyPath.Substring(0, propertyPath.IndexOf('.'));
        string arrayIndex = Regex.Match(propertyPath, @"\d+").Value;

        // Try converting the index we parsed to an integer value
        int index;
        if (!Int32.TryParse(arrayIndex, out index)) {
            Debug.LogError(String.Format(
                "Error rendering GameAction property drawer: Could not parse array index ({0}) as integer",
                arrayIndex
            ));
            return null;
        }

        // Get a reference to the array field we parsed out of the property path
        // on the actual type.
        var targetObject = property.serializedObject.targetObject;
        var targetObjectClassType = targetObject.GetType();
        var field = targetObjectClassType.GetField(listName);
        if (field == null) {
            Debug.LogError(String.Format(
                "Error rendering GameAction property drawer: Could not get field {0} of type {1}",
                listName,
                targetObjectClassType.ToString()
            ));
            return null;
        }

        // Get the actual array reference on the target object
        var array = field.GetValue(targetObject) as List<GameAction>;
        if (array == null) {
            Debug.LogError(String.Format(
                "Error rendering GameAction property drawer: Could not get value of field {0} from target instance",
                listName
            ));
            return null;
        }
        
        if (array.Count <= index) {
            // This happens when changing the size of the array in the editor, so don't
            // log an error in this case
            return null;
        }

        return array[index];
    }
}
