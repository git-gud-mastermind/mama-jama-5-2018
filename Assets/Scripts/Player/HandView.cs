using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public Card AddCardToHand(Card cardDrawn){
		// newCard = Instantiate(CardView);                   // Instantiate Prefab
		// newCard.getComponent<CardView>.setCard(cardDrawn); // Assign ScriptableObject to prefab
		// return newCard;
		return cardDrawn;
	}
}
