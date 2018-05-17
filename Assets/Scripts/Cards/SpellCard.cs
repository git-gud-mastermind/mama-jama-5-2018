using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Spell Card")]
public class SpellCard : Card {

    public override bool isTargetable { get { return false; } } // Other cards can target this card
    public override bool canTarget { get { return true; } } // This card can target other cards

    /**
     *  @function UseSpell
     *  @description Use this spell card and carry out any event scripts.
     */
    public void UseSpell() {
    // this.whenPlayed;

    // once used:
    // this.DestroyCard();
  }
}
