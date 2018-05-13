using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameAction)), CanEditMultipleObjects]
public class GameActionEditor : Editor {

	public SerializedProperty
		actionType,
		damageValue,
		healingValue;

	void OnEnable(){
		// Set up SerializedProperties here
		actionType   = serializedObject.FindProperty ("type");
		damageValue  = serializedObject.FindProperty ("damageValue");
		healingValue = serializedObject.FindProperty ("healingValue");
	}

	public override void OnInspectorGUI () {
		// Update serialized Property
		serializedObject.Update();

		EditorGUILayout.PropertyField( actionType );

		GameAction.ActionType type = (GameAction.ActionType)actionType.enumValueIndex;

		// Depending on the Action Type, show different fields
		switch(type){
			case GameAction.ActionType.DamageTarget:
				EditorGUILayout.IntSlider(damageValue, 0, 100, new GUIContent("Damage Dealt"));
				break;
			case GameAction.ActionType.HealTarget:
				EditorGUILayout.IntSlider(healingValue, 0, 100, new GUIContent("Healing Done"));
				break;
		}

		serializedObject.ApplyModifiedProperties ();
	}

}
