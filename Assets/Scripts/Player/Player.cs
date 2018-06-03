using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	public int currentHealth;
	public int startingHealth = 30;

	public int currentMana;
	public int manaThisTurn; // Increases by one every turn.
	public int startingMana = 3;
	public int maxMana = 7;

	public int turnCounter; // How many turns have passed

	public Deck deck;
	public List<Card> hand;
	public List<Card> cardsOnField; // Active cards on the game board

	public int startingHandSize = 4;
	public int maxHandSize;

	// Use this for initialization
	public void Init(Deck newDeck) {
		currentMana = startingMana;
		currentHealth = startingHealth;
		turnCounter = 0;

		// Instantiate Deck
		deck = GameManager.instance.CreateInstance<Deck>(newDeck);
		deck.ShuffleDeck();

		// Instantiate Hand
		hand = new List<Card>();

		// Draw a number of cards equal to the starting hand size
		for(int i = 0; i < startingHandSize; i++){
			var drawnCard = GameManager.instance.CreateInstance<Card>(deck.DrawCard());
			hand.Add(drawnCard);
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
			// Remove card from hand
			hand.Remove(cardToPlay);
			// Play the card and trigger its abilities
			cardToPlay.PlayCard();
		}
	}

	/**
	 *  @function StartTurn
	 *  @description Called at the start of a player's turn.
	 */
	public void StartTurn(){
		// Increment turn turnCounter
		turnCounter++;

		// Reset mana counter
		currentMana = manaThisTurn;

		// Draw a card
		if(hand.Count < maxHandSize){
			hand.Add(deck.DrawCard());
		}

		foreach(Card card in cardsOnField){
			card.isActiveCard = true; // Set all player's inactive cards to active
			// card.whenTurnIsStarted;  // Trigger any events this card has on turn start
		}
	}

	/**
	 *  @function EndTurn
	 *  @description Called at the end of a player's turn.
	 */
	public void EndTurn(){
		foreach(Card card in cardsOnField){
			// card.whenTurnIsEnded // Trigger any events on turn end for
		}
	}


	/**
	 *  @function DrawCard
	 *  @description Draw a card for this player
	 */
	public void DrawCard(){
			// newCard = HandView.AddCardToHand(deck.DrawCard());
			// hand = gameObject.getComponent<HorizontalLayoutGroup>(); // Get the layout group for the hand
			// hand.add(newCard); // Add card to hand
	}

	/**
	 *  @function Concede
	 *  @description Called when this player decides to forfeit the game.
	 */
	public void Concede(){

	}

}
