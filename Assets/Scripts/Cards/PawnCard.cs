using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pawn Card", menuName = "Pawn Card")]
public class PawnCard : Card {

    public override bool isTargetable { get { return true; } } // Other cards can target this card
    public override bool canTarget { get { return true; } } // This card can target other cards

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
