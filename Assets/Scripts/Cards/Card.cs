using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : ScriptableObject {

	public Sprite cardArt;

	public string cardName;
	public string type;
	public string description;

	public int manaCost;
	public int attackPower;
	public int health;

    public virtual bool isTargetable { get { return false; } } // Other cards can target this card
    public virtual bool canTarget { get { return false; } } // This card can target other cards

    // Event triggers
    public List<GameAction> whenAttacking;
	public List<GameAction> whenPlayed;
	public List<GameAction> whenDestroyed;
	public List<GameAction> whenTargeted;
	public List<GameAction> whenDrawn;

	/**
	 *  @function PlayCard
	 *  @description Play this card from the player's hand and onto the field.
	 */
	public virtual void PlayCard() {
		// this.whenPlayed
	}

	/**
	 *  @function DestroyCard
	 *  @description Remove this card from the battlefield.
	 */
	public virtual void DestroyCard() {
		// this.whenDestroyed
	}

}
