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

	private bool targetable; // Other cards can target this card
	private bool canTarget;  // This card can target other cards

	public List<GameAction> whenPlayed;
	public List<GameAction> whenDestroyed;
	public List<GameAction> whenAttacked;
	public List<GameAction> whenCardIsDrawn;

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
