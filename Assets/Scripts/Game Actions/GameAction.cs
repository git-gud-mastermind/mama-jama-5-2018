using UnityEditor;
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameAction {

	public enum ActionType {
		DamageTarget,
		HealTarget
	};

	public ActionType type;

	public int damageValue;
	public int healingValue;

	public void Start(){

	}

	public void Update(){

	}

}
