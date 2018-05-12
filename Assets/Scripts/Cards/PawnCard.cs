using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pawn Card", menuName = "Pawn Card")]
public class PawnCard:Card {

	public int attackPower;
	public int health;

	boolean targetable = true;

	/**
	 *  @function Attack
	 *  @description What happens when this card attacks another card.
	 */
	public void Attack (targetCard) {

	}

	/**
	*  @function Defend
	*  @description What happens when this card is attacked.
	*/
	public void Defend (attackingCard) {

	}
}
