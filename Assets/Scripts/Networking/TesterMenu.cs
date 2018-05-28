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
	    if (roomProperties.ContainsKey((int)PhotonPropId.MatchState))
	    {
	        var matchState = (MatchState)roomProperties[(int)PhotonPropId.MatchState];
	        StateText.text = matchState.ToString();


	    }
    }
	
	// Update is called once per frame
	void Update () {
	    var roomProperties = PhotonNetwork.room.CustomProperties;
	    MatchState matchState = MatchState.Wait;
	    if (roomProperties.ContainsKey((int)PhotonPropId.MatchState))
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
        if (startTime - Time.time > 1.5f)
        {
            //switch to play
            if (PhotonNetwork.isMasterClient)
            {
                var props = new Hashtable();
                props.Add((int)PhotonPropId.MatchState, (int)MatchState.Play);
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

    }
}
