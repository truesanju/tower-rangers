using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	//Camera Variables
	private Vector3 initialCameraPt;
	public float sensitivity = 2.0f;
	private Vector3 dragStartPt;
	private Vector3 position;

	//Game ID variables
	int PlayerId = 1;
	int CurrentPlayerId = 1;

	//Networking local player objects
	public Camera gameCamera;
	public AudioListener gameAudioListener;


	[Command]
	void CmdSetSpawnClient(){
		
		//increase the PlayerID
		CurrentPlayerId ++;

		RpcSetClientSpawn (CurrentPlayerId);		//Set the player's transform location based off the current player id
		Debug.Log ("Spawning Player" + CurrentPlayerId + "at Spawn" + CurrentPlayerId);

		//TODO Store the client into the hashset

		//TODO Assign the land to this player
	}

	[ClientRpc]
	void RpcSetClientSpawn(int gameId){
		if (PlayerId != 1) return;
		PlayerId = gameId;

		//set spawn
		GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn"+gameId);
		//Do transform here
		transform.position = SpawnPoint.transform.position;
		transform.eulerAngles = SpawnPoint.transform.eulerAngles;
		//initialCameraPt = transform.position;
	}

	public void SetStartSpawn(){
		//if isServer, spawn at position 1
		//else do a server command to ask for a spawn position
		//	Server will assign spawn position based off player id

		if (isServer) {
			GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn1");
			//Do transform here
			transform.position = SpawnPoint.transform.position;
			transform.eulerAngles = SpawnPoint.transform.eulerAngles;
		} else {
			CmdSetSpawnClient ();
		}
	}


	public override void OnStartLocalPlayer(){
		//All players run this once, and on their own device

		//turn on our camera and audiolistener for the local player
		gameCamera.enabled = true;
		gameAudioListener.enabled = true;
		gameObject.name = "LOCAL player";


		// set spawn dynamically, and assign player id from server
		SetStartSpawn();

		Debug.Log ("My player id is " + PlayerId);

		//set our player controller and camera
		//transform.position = new Vector3 (-34.46f, 47.63f, -72.01f);
		//transform.eulerAngles = new Vector3 (0f,46.92f, 0f);
		//gameCamera.transform.eulerAngles = new Vector3 (56.13f, 46.92f, 0f);
		initialCameraPt = transform.position;

		base.OnStartLocalPlayer ();
	}


	/*
	[Command]
	void CmdGetGameid(){
		PlayerId++;
		Debug.Log ("Current number of players is " + PlayerId);
		RpcSetGameId (PlayerId);
	}
		
	[ClientRpc]
	void RpcSetGameId(int gameId){
		this.gameId = gameId;
	}
*/
	
	// Update is called once per frame
	void Update () {
		//transform based off how much we move
		if (!isLocalPlayer) return;

		//var camMoveAmount = Input.GetAxis("Vertical") * Time.deltaTime * ViewSpeed;

		//transform.Translate(camMoveAmount, 0, camMoveAmount, Space.World);

		if (Input.GetMouseButtonDown(0))
		{
			dragStartPt = Input.mousePosition;
			return;
		}

		if (Input.GetMouseButtonDown(1))
		{
			transform.position = initialCameraPt;
			return;
		}

		if (!Input.GetMouseButton(0)) { return; }

		//Sanjay's singleplayer camera
		//position = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragStartPt);
		//Vector3 motion = new Vector3(position.x * sensitivity, position.y * sensitivity, 0);
		//transform.Translate(motion,Space.Self);

		//Networked Version
		position = GetComponentInChildren<Camera> ().ScreenToViewportPoint(Input.mousePosition - dragStartPt);

		//With X movement
		//Vector3 motion = new Vector3(position.x * sensitivity,0, position.y * sensitivity);
		Vector3 motion = new Vector3(0,0, position.y * sensitivity);
		transform.Translate(motion,Space.Self);
	}
}
