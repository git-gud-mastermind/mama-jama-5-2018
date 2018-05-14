using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pawn Card", menuName = "Pawn Card")]
public class PawnCard : Card {

	/**
	 *  @function Awake
	 *  @description Set up default field values for this card type.
	 */
	public void Awake(){
		this.isTargetable = true;
		this.canTarget = true;
	}

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
