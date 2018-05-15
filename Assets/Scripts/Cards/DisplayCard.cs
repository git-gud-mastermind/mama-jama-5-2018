using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour {

	public Card card;

	public Text name;
	public Text description;

	public Text mana;
	public Text attack;
	public Text health;

	public Image cardArt;

	// Use this for initialization
	void Start () {
		name.text = card.name;
		description.text = card.description;

		cardArt.sprite = card.cardArt;

		mana.text = card.manaCost.ToString();
		attack.text = card.attackPower.ToString();
		health.text = card.health.ToString();
	}

}
