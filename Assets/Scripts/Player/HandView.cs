using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandView : MonoBehaviour {

	public List<Card> hand;

	// Use this for initialization
	void Start () {
		foreach(Card card in hand){
			GameObject cardInstance = (GameObject)Instantiate(Resources.Load("Card"));
		}
	}

}
