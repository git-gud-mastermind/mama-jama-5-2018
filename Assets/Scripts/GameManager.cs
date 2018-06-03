using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;

    public GameObject opponentBoard;

    public HandView handView; // Attached in the editor

    List<Player> _players;
    private Player _currentPlayer;

    public Deck playerOneDeck;
    public Deck playerTwoDeck;

    // GameManager singleton access
    public static GameManager instance { get; private set; }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

  	private void Start() {
        // Init list of players
        _players = new List<Player>();

        // Create Player one
        Player p1 = new Player();
        p1.Init(playerOneDeck);

        // Create player one hand
        foreach(var card in p1.hand){
          GameObject cardView = Instantiate(cardPrefab);
          handView.AddCardToHand(cardView);

          CardView view = cardView.GetComponent<CardView>();
          view.card = card;
          view.player = p1;
        }

        // Create Player Two
        Player p2 = new Player();
        p2.Init(playerTwoDeck);

        // Create player 2 hand
        foreach(var card in p2.hand){
          GameObject cardView = Instantiate(cardPrefab);
          cardView.transform.SetParent(opponentBoard.transform);

          CardView view = cardView.GetComponent<CardView>();
          view.card = card;
          view.player = p2;
        }
  	}

    public T CreateInstance<T>(T objectToClone)  where T : ScriptableObject{
      return (T)Instantiate(objectToClone);
    }

    public void EndTurn() {
        if (_players.Count != 2) {
            Debug.LogError("Unexpected number of players: " + _players.Count);
            return;
        }

        // End the current player's turn
        _currentPlayer.EndTurn();

        // Determine the next player and start their turn
        if (_currentPlayer == _players.First()) {
            _currentPlayer = _players.Last();
        } else {
            _currentPlayer = _players.First();
        }

        _currentPlayer.StartTurn();
    }

  	private void Update() {
  		// Tick current player's turn timer
              // If it expires, end their turn
  	}
}
