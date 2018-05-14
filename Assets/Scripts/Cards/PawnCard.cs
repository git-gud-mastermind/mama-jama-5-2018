using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pawn Card", menuName = "Pawn Card")]
public class PawnCard : Card {

	private bool isTargetable = true;
	private bool canTarget = true;

	/**
	 *  @function Attack
	 *  @description What happens when this card attacks another card.
	 */
	public void Attack () {
		// this.whenAttacking
	}

	/**
	*  @function Defend
	*  @description What happens when this card is attacked.
	*/
	public void Defend () {
		// this.whenTargeted
	}
}
