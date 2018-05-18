using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int currentHealth;
	public int startingHealth = 30;

	public int currentMana;
	public int manaThisTurn; // Increases by one every turn.
	public int startingMana = 3;
	public int maxMana = 7;

	public Deck deck;
	public List<Card> hand;
	public List<Card> cardsOnField; // Active cards on the game board

	public int startingHandSize = 4;
	public int maxHandSize;

	// Use this for initialization
	void Start () {
		currentMana = startingMana;
		currentHealth = startingHealth;

		deck.ShuffleDeck();
		// Draw a number of cards equal to the starting hand size
		for(int i = 0; i < startingHandSize; i++){
			hand.Add(deck.DrawCard());
		}
	}

	/**
	 *  @function PlayCard
	 *  @description Play a card from this player's hand and reduce the amount of mana it costs to play.
	 */
	public void PlayCard(Card cardToPlay, int positionOnBoard){
		// If player can afford this card...
		if(currentMana >= cardToPlay.manaCost){
			// Reduce mana
			currentMana -= cardToPlay.manaCost;
			// Play the card and trigger its abilities
			cardToPlay.PlayCard();
		}
	}

	/**
	 *  @function StartTurn
	 *  @description Called at the start of a player's turn.
	 */
	public void StartTurn(){
		// Reset mana counter
		currentMana = manaThisTurn;

		// Draw a card
		if(hand.Count < maxHandSize){
			hand.Add(deck.DrawCard());
		}

		for(card in cardsOnField){
		   card.isActive = true;   // Set all player's inactive cards to active
				card.whenTurnIsStarted; // Trigger any events this card has on turn start
		}
	}

	/**
	 *  @function EndTurn
	 *  @description Called at the end of a player's turn.
	 */
	public void EndTurn(){

	}

	/**
	 *  @function Concede
	 *  @description Called when this player decides to forfeit the game.
	 */
	public void Concede(){

	}

}
