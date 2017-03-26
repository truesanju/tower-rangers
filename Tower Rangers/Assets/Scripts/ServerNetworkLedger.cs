using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerNetworkLedger : MonoBehaviour {

	//Health of player
	//data of land
	//Spawning information
	int [] HealthArray = new int[4];

	private int CurrentNumberOfPlayers = 1;
	private int NUMBEROFPLAYERS = 4;


	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + CurrentNumberOfPlayers++ + " connected from " + player.ipAddress + ":" + player.port);
	}


	// Use this for initialization
	void Start () {

		//wait until 3 other players connect
		while (CurrentNumberOfPlayers < NUMBEROFPLAYERS){
		}

	}

	void onClientConnect(){
		//assign ID to other player
		//Allocate health for other player

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
