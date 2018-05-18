using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CardView cardPrefab;

    List<object> _players;
    private object _currentPlayer;

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
        // Init (players, scene, etc)
        // Populate local player's hand
        // Decide who goes first
        // Start game
	}

    public void EndTurn(object player) {
        // End the current player's turn
        // Start the next player's turn
    }

	private void Update() {
		// Tick player's turn timer
            // If it expires, end their turn
	}
}
