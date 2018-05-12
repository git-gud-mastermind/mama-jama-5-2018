using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaos Power Card", menuName = "Chaos Power Card")]
public class ChaosCard:Card {

	public int attackPower;
	public int health;

	boolean targetable = true;

	/**
	 *  @function Defend
	 *  @description What happens when this card is attacked.
	 */
	public void Defend (attackingCard) {

	}

}
