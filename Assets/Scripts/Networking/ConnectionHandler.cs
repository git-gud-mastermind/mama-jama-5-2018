using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionHandler : Photon.PunBehaviour {

	[SerializeField] string gameVersion = "0";

	public List<Selectable> connectionReqdObject = new List<Selectable>();
	List<bool> croStartingStates = new List<bool>();

	bool isInRoom = false, startMatch = false;

	public TextTweening infoText;

	public List<string> waitingTexts = new List<string>()
	{
		"Waiting for an idle god",
		".Waiting for an idle god.",
		"..Waiting for an idle god..",
		"...Waiting for an idle god..."
	};

	public string connectionSuccessText;

	#region Unity Callbacks

	private void Awake()
	{
		PhotonNetwork.autoJoinLobby = false;

		PhotonNetwork.automaticallySyncScene = true;
	}

	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings(gameVersion);

		foreach (var item in connectionReqdObject)
		{	
			croStartingStates.Add(item.interactable);
			item.interactable = false;
		}
	}

	private void Update()
	{
		if (isInRoom)
		{
			if (PhotonNetwork.room.PlayerCount == 2 && !startMatch)
			{
				infoText.SetText(connectionSuccessText);

				startMatch = true;

				//Start game
			}
		}
	}

	#endregion


	#region Primary Methods

	/// <summary>
	/// Start the connection process. 
	/// - If already connected, we attempt joining a random room
	/// - if not yet connected, Connect this application instance to Photon Cloud Network
	/// </summary>
	public void Connect(string ip)
	{
		// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
		if (PhotonNetwork.connected)
		{
			//Matchmaking
			if (string.IsNullOrEmpty(ip))
			{
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
				PhotonNetwork.JoinRandomRoom();
			}
			else
			{
				//Stub. May add Direct Connect
			}
		}

		else
		{
			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.ConnectUsingSettings(gameVersion);
		}
	}

	#endregion


	#region Photon.PunBehaviour CallBacks

	public override void OnConnectedToPhoton()
	{
		infoText.SetText("Welcome");

		isInRoom = startMatch = false;

		//Set our UI interactability to its proper starting state
		connectionReqdObject.ForEach(obj => obj.interactable = croStartingStates [connectionReqdObject.IndexOf(obj)]);
	}

	public override void OnDisconnectedFromPhoton()
	{
		infoText.SetText("Disconnected.");
	}

	public override void OnPhotonRandomJoinFailed(object [] codeAndMsg)
	{
		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
	}

	public override void OnJoinedRoom()
	{
		//Wait for match
		if (PhotonNetwork.room.PlayerCount == 1)
		{
			infoText.SetTextOnLoop(waitingTexts, 0.3f);
		}

		isInRoom = true;
	}

	#endregion
}
