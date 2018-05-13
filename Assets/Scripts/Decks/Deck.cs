using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Card Deck")]
public class Deck : ScriptableObject {

	public List<Card> deck;

	/**
	*  @function DrawCard
	*  @description Draw a card from the deck.
	*  @return Card - The card drawn
	*/
	public Card DrawCard(){

	}
}
