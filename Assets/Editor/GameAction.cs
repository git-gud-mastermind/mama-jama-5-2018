using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameAction)), CanEditMultipleObjects]
public class GameActionEditor : Editor {

	public SerializedProperty
		actionType,
		damageValue,
		healingValue;

	void OnEnable(){
		// Set up all Serialized Properties here
		actionType   = serializedObject.FindProperty("type");
		damageValue  = serializedObject.FindProperty("damageValue");
		healingValue = serializedObject.FindProperty("healingValue");
	}

	public override void OnInspectorGUI () {
		serializedObject.Update();

		// Always display the Action Type dropdown
		EditorGUILayout.PropertyField( actionType );

		// Get the index of the selected Action Type
		GameAction.ActionType type = (GameAction.ActionType)actionType.enumValueIndex;

		// Depending on the Action Type, show different fields
		switch(type){
			case GameAction.ActionType.DamageTarget:
				EditorGUILayout.PropertyField(damageValue);
				break;
			case GameAction.ActionType.HealTarget:
				EditorGUILayout.PropertyField(healingValue);
				break;
		}

		serializedObject.ApplyModifiedProperties ();
	}

}
