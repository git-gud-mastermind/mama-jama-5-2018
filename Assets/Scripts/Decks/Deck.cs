using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Card Deck")]
public class Deck : ScriptableObject {

	private static System.Random rnd = new System.Random();

	public List<Card> cards;

	// Events
	// @TODO: Plug these in to something?
	public object whenCardIsDrawn;

	/**
	 *  @function DrawCard
	 *  @description Draw a card from the deck.
	 *  @return Card - The card drawn
	 */
	public Card DrawCard(){
		// Handle events, if any
		// this.whenCardIsDrawn;

		Card drawnCard = cards[0];
		cards.Remove(drawnCard);
		return drawnCard;
	}

	/**
	 *  @function DrawRandomCard
	 *  @description Draw a random card from the deck.
	 *  @return Card - The card drawn
	 */
	public Card DrawRandomCard(){
		// Handle events, if any
		// this.whenCardIsDrawn;

		int randomCardIndex = rnd.Next(cards.Count);
		Card drawnCard = cards[randomCardIndex];
		cards.Remove(drawnCard);
		return drawnCard;
	}

	/**
	 *  @function ShuffleDeck
	 *  @description Shuffle the cards in this deck
	 */
	public void ShuffleDeck(){
		// Go through the entire deck and swap every card with a random card
		for(int i = 0; i < cards.Count; i++){
			// Stash the current card
			Card thisCard = cards[i];
			// Pick a random card
			int randomCardIndex = Random.Range(i, cards.Count);
			// Swap the random card with the current card
			cards[i] = cards[randomCardIndex];
			cards[randomCardIndex] = thisCard;
		}
	}
}
