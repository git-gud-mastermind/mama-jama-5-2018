using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionHandler : Photon.PunBehaviour {

	#region Declarations

	[SerializeField] string gameVersion = "0";

	[Tooltip("The buttons and other selectables for which we require a connection to Photon's network before we allow interactivity. aka THESE BUTTONS DO INTERNET THINGS")]
	public List<Selectable> connectionReqdObjects = new List<Selectable>();
	/** 'connectionReqdObjects' Starting States. Matches 1-to-1 with the Selectables list above **/
	List<bool> croStartingStates = new List<bool>();

	bool isInRoom = false;
	bool startMatch = false;

	[Tooltip("The UI text object's tween controller that will display current network state.")]
	public TextTweening infoText;

	[Tooltip("The text to show when waiting for a match. Will auto-cycle to create an illusion of animation.")]
	public List<string> waitingTexts = new List<string>()
	{
		"Waiting for an idle god",
		".Waiting for an idle god.",
		"..Waiting for an idle god..",
		"...Waiting for an idle god..."
	};

	public string connectionSuccessText;

	#endregion


	#region Unity Callbacks

	private void Awake()
	{
		PhotonNetwork.autoJoinLobby = false;

		PhotonNetwork.automaticallySyncScene = true;
	}

	private void Start()
	{
		//Connect to Photon proactively
		PhotonNetwork.ConnectUsingSettings(gameVersion);

		//Turn off our buttons, etc., before we confirm Photon connection
		foreach (var item in connectionReqdObjects)
		{	
			croStartingStates.Add(item.interactable);
			item.interactable = false;
		}
	}

	private void Update()
	{
		//Waiting for match
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

	/// <summary>
	/// We receive this from Photon when the server recognizes us
	/// </summary>
	public override void OnConnectedToPhoton()
	{
		infoText.SetText("Welcome");

		isInRoom = startMatch = false;

		//Set our UI interactability to its proper starting state
		connectionReqdObjects.ForEach(obj => obj.interactable = croStartingStates [connectionReqdObjects.IndexOf(obj)]);
	}

	/// <summary>
	/// We receive this when we lose connection to Photon servers
	/// </summary>
	public override void OnDisconnectedFromPhoton()
	{
		infoText.SetText("Disconnected.");

		//Lost connection. Stop interaction on the buttons.
		connectionReqdObjects.ForEach(obj => obj.interactable = false);
	}

	/// <summary>
	/// We receive this callback when we try to join a room, but no space is available in any extant rooms
	/// </summary>
	/// <param name="codeAndMsg"></param>
	public override void OnPhotonRandomJoinFailed(object [] codeAndMsg)
	{
		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
	}

	/// <summary>
	/// We receive this when Photon confirms we've entered a room (created or joined)
	/// </summary>
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
