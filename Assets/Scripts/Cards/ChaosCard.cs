using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaos Power Card", menuName = "Chaos Power Card")]
public class ChaosCard : Card {

    public override bool isTargetable { get { return true; } } // Other cards can target this card
    public override bool canTarget { get { return false; } } // This card can target other cards

	/**
	 *  @function Defend
	 *  @description What happens when this card is attacked.
	 */
	public void Defend () {
		// this.whenTargeted
	}

}
