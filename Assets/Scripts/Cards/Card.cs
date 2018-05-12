using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject {

	public Sprite cardArt;

	public string name;
	public string type;
	public string abilities;

	public int manaCost;

	public EventAction whenPlayed;
	public EventAction whenDestroyed;
	public EventAction whenCardIsDrawn;
	public EventAction whenAttacked;

	public boolean targetable; // Other cards can target this card
	public boolean canTarget;  // This card can target other cards

	/**
	 *  @function PlayCard
	 *  @description Play a card from the player's hand and onto the field.
	 */
	public void PlayCard(){

	}

	/**
	 *  @function DestroyCard
	 *  @description Remove a card from the battlefield.
	 */
	public void DestroyCard(){

	}

}
