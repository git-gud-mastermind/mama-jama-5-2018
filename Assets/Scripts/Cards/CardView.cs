using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour {

	public Card card;

	public TextMeshProUGUI cardName;
	public TextMeshProUGUI type;
	public TextMeshProUGUI abilities;

	public TextMeshProUGUI mana;
	public TextMeshProUGUI attack;
	public TextMeshProUGUI health;

	public Image cardArt;

	// Use this for initialization
	void Start () {
		cardName.text  = card.cardName;
		type.text      = card.type;
		abilities.text = card.abilities;

		mana.text   = card.manaCost.ToString();
		attack.text = card.attackPower.ToString();
		health.text = card.health.ToString();

		cardArt.sprite = card.cardArt;
	}

}
