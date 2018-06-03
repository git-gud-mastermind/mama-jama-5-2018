using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class TesterMenu : MonoBehaviour
{
    public ConnectionHandler ConnectionHandler;

    private float startTime = 1;

    public Text StateText;

	// Use this for initialization
	void Start ()
	{
	    //_connectionHandler = Object.FindObjectOfType<ConnectionHandler>();
	    startTime = Time.time;
	    var roomProperties = PhotonNetwork.room.CustomProperties;
	    if (roomProperties.ContainsKey(PhotonPropId.MatchState))
	    {
	        var matchState = (MatchState)roomProperties[PhotonPropId.MatchState];
	        OnMatchState(matchState);

	    }
    }
	
	// Update is called once per frame
	void Update () {
	    var roomProperties = PhotonNetwork.room.CustomProperties;
	    MatchState matchState = MatchState.Wait;
	    if (roomProperties.ContainsKey(PhotonPropId.MatchState))
	    {
	        matchState = (MatchState)roomProperties[PhotonPropId.MatchState];

	    }

        //cycling through some states
        switch (matchState)
	    {
	        case MatchState.Start:
	            StartingState();
                break;
            case MatchState.Play:
                PlayingState();
                break;
	    }
       
	}

    void StartingState()
    {
        if (Time.time - startTime > 1.5f)
        {
            //switch to play
            if (PhotonNetwork.isMasterClient)
            {
                var props = new Hashtable();
                props.Add(PhotonPropId.MatchState, MatchState.Play);
                PhotonNetwork.room.SetCustomProperties(props);

            }
        }
    }

    void PlayingState()
    {

    }

    public void Concede()
    {
        ConnectionHandler.Concede();
    }

    public void OnMatchState(MatchState matchState)
    {
        StateText.text = matchState.ToString();
    }
}
