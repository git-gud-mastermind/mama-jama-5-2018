using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaos Power Card", menuName = "Chaos Power Card")]
public class ChaosCard : Card {

	/**
	 *  @function Awake
	 *  @description Set up default field values for this card type.
	 */
	public void Awake(){
		this.isTargetable = true;
		this.canTarget = false;
	}

	/**
	 *  @function Defend
	 *  @description What happens when this card is attacked.
	 */
	public void Defend () {
		// this.whenTargeted
	}

}
