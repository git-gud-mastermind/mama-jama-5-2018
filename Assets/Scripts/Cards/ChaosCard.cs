using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaos Power Card", menuName = "Chaos Power Card")]
public class ChaosCard : Card {

	private bool isTargetable = true;
	private bool canTarget    = false; // Can't target, can only defend

	/**
	 *  @function Defend
	 *  @description What happens when this card is attacked.
	 */
	public void Defend () {
		// this.whenTargeted
	}

}
