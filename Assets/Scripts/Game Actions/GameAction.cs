using UnityEditor;
using UnityEngine;
using System.Collections;

public enum ActionType {
	DamageTarget,
	HealTarget
};

public class GameAction : MonoBehaviour {

	public ActionType type;

	public int damageValue;
	public int healingValue;

	public void Start(){

	}

	public void Update(){

	}

}
