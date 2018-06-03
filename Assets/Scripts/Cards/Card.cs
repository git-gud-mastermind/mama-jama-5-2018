using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : ScriptableObject {

	public Sprite cardArt;

	public string cardName;
	public string type; // Creature, Warrior, Etc
	[TextArea(4,4)]
	public string abilities;

	public int manaCost;
	public int attackPower;
	public int health;

	public bool isActiveCard;

  public virtual bool isTargetable { get { return false; } } // Other cards can target this card
  public virtual bool canTarget { get { return false; } } // This card can target other cards

  // Event triggers
  public List<GameAction> whenAttacking;
	public List<GameAction> whenPlayed;
	public List<GameAction> whenDestroyed;
	public List<GameAction> whenTargeted;
	public List<GameAction> whenDrawn;
	public List<GameAction> whenTurnIsStarted;
	public List<GameAction> whenTurnIsEnded;

}
