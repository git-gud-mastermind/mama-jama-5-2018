using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

//easy to grab network match states
[System.Serializable]
public enum MatchState
{
    Wait = 0,
    Start,
    Play,
    End
}

[SerializeField]
public class UnityEventMatchState : UnityEvent<MatchState>
{

}

public class ConnectionHandler : Photon.PunBehaviour {

	#region Declarations

	[SerializeField] string gameVersion = "0";

	[Tooltip("The buttons and other selectables for which we require a connection to Photon's network before we allow interactivity. aka THESE BUTTONS DO INTERNET THINGS")]
	public List<Selectable> connectionReqdObjects = new List<Selectable>();
	/** 'connectionReqdObjects' Starting States. Matches 1-to-1 with the Selectables list above **/
	List<bool> croStartingStates = new List<bool>();

	bool isInRoom = false;
	bool startMatch = false;

    public Canvas Canvas;

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

    #if UNITY_EDITOR
    //lets us test multiplayer stuff in editor alone
    public bool allowSinglePlayer = false;
    #endif

    public UnityEvent OnRoomReady;
    public UnityEventMatchState OnMatchStateChange = new UnityEventMatchState();
    public TesterMenu TesterMenuPrefab;
    public TesterMenu TesterMenu;

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
		    int playerCountNeeded = 2;

            #if UNITY_EDITOR
            //in editor single testing
		    if (allowSinglePlayer)
		    {
		        playerCountNeeded = 1;
		    }
            #endif

            if (PhotonNetwork.room.PlayerCount >= playerCountNeeded && !startMatch)
			{
				infoText.SetText(connectionSuccessText);

				startMatch = true;
                //sync state with connect clients
                //https://doc.photonengine.com/en-us/pun/current/gameplay/synchronization-and-state
                if (PhotonNetwork.isMasterClient)
			    {
			        var props = new Hashtable();
			        props.Add(PhotonPropId.MatchState, MatchState.Start);
                    PhotonNetwork.room.SetCustomProperties(props);

			    }

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

    public void Concede()
    {
        photonView.RPC("ConcedeRpc", PhotonTargets.All, PhotonNetwork.player.ID);
    }

    /// <summary>
    /// remote call across players
    /// </summary>
    /// <param name="playerId">player id for who is conceding</param>
    public void ConcedeRpc(int playerId)
    {
        Debug.Log("Player: " + playerId + " conceded the match");
        if (PhotonNetwork.isMasterClient)
        {

            var props = new Hashtable();
            props.Add(PhotonPropId.MatchState, (int)MatchState.End);

            PhotonNetwork.room.SetCustomProperties(props);

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

    /// <summary>
    /// called when
    /// </summary>
    /// <param name="propertiesThatChanged"></param>
    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        foreach (var prop in propertiesThatChanged)
        {
            Debug.Log(prop.Key.ToString() + " " + prop.Value.ToString());
        }

        //check properties to load stuff
        if (propertiesThatChanged.ContainsKey(PhotonPropId.MatchState))
        {
            var matchState = (MatchState)propertiesThatChanged[PhotonPropId.MatchState];

            //checking some states
            switch (matchState)
            {
                case MatchState.Start:
                    if (OnRoomReady != null)
                    {
                        OnRoomReady.Invoke();
                    }

                    TesterMenu = Instantiate(TesterMenuPrefab, Canvas.transform);
                    TesterMenu.ConnectionHandler = this;
                    OnMatchStateChange.AddListener(TesterMenu.OnMatchState);
                    break;
                case MatchState.Play:
                    break;
            }

            if (OnMatchStateChange != null)
            {
                OnMatchStateChange.Invoke(matchState);
            }
            

        }
    }

    #endregion
}
