using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerScript : NetworkBehaviour {
	//Camera Variables
	private Vector3 initialCameraPt;
	public float sensitivity = 2.0f;
	private Vector3 dragStartPt;
	private Vector3 position;
	public static int NumberOfAliveOpponents = 3;

	//Game ID variables
	public int PlayerId = 1;
	static int CurrentPlayerId = 1;	//static to maintain same int across all player prefabs on server

	//Networking local player objects
	public Camera gameCamera;
	public AudioListener gameAudioListener;
	public NetLandManager netLandManager;

	//Player name UI
	public Text playerNameField;

	[Command]
	void CmdGetIdAndSetSpawnClient(){
		//announce the next available playerId. 
		//client calling this command (isLocalPlayer) will use it as his playerId
		CurrentPlayerId ++;
		RpcSetClientSpawn (CurrentPlayerId);
	}

	[Command]
	void CmdDebugLog(string message){
		Debug.Log (message);
	}

	[ClientRpc]
	void RpcSetClientSpawn(int gameId){
		if (isLocalPlayer){		
			//set playerId
			PlayerId = gameId;

			//Set up player's position
			GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn"+gameId);
			transform.position = SpawnPoint.transform.position;
			transform.eulerAngles = SpawnPoint.transform.eulerAngles;
			initialCameraPt = transform.position;


			//From Eiros: Update player name on UI
			playerNameField = GameObject.FindGameObjectWithTag("PlayerName").GetComponent<Text>();
			playerNameField.text = PlayerId.ToString ();
		}
	}

	public void SetStartSpawn(){
		//if isServer, spawn at position 1
		//else do a server command to ask for a spawn position
		//	Server will assign spawn position based off player id

		if (isServer) {
			GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn1");
			//Set spawn position, mob waypoint, and name
			transform.position = SpawnPoint.transform.position;
			transform.eulerAngles = SpawnPoint.transform.eulerAngles;
			gameCamera.transform.eulerAngles = new Vector3 (56.13f, 46.92f, 0f);
//			GameObject.Find("mobwaypoint1").GetComponent<Netwaypoints>().CreateStaticPoints();
			playerNameField = GameObject.FindGameObjectWithTag("PlayerName").GetComponent<Text>();
			playerNameField.text = PlayerId.ToString ();
			initialCameraPt = transform.position;
		} else {
			CmdGetIdAndSetSpawnClient ();
		}
	}

	[Command]
	public void CmdPlayerDied(int DiedPlayerId){
		Debug.Log ("Serverside: Player " + DiedPlayerId + " died");

		RpcPlayerDied (DiedPlayerId);
	}
	[ClientRpc]
	void RpcPlayerDied(int DiedPlayerId){
		Debug.Log ("Clientside: Player " + DiedPlayerId + " died");
		NumberOfAliveOpponents--;
		if (NumberOfAliveOpponents <= 0 && PlayerLedger.hp > 0) {
			Debug.Log ("You win!");
			GameManager.youWin = true;
		}
	}


	public void Start(){
		//All players run this once, and on their own device

		if (isLocalPlayer && isClient) {
			//turn on our camera and audiolistener for the local player
			gameCamera.enabled = true;
			gameAudioListener.enabled = true;
			gameObject.name = "LOCAL player";

			// set spawn dynamically, and assign player id from server.
			SetStartSpawn ();
			// Link LocalLandManager to this localplayer object so it can use network via NetLandManager
			LocalLandManager.player = this;

			//Set up GameManager for hp updates
			GameManager.player = this;

			//Set up Netmobspawnner
			NetMobsspawner.player = this;
		}

	}

	
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
		Vector3 motion = new Vector3(position.x * sensitivity,0, position.y * sensitivity);
		//Vector3 motion = new Vector3(0,0, position.y * sensitivity);
		transform.Translate(motion,Space.Self);
	}


	[ClientRpc]
	public void RpcSpawnMob(){
		NetMobsspawner netmobSpawner = GameObject.Find ("spawnpoint").GetComponent<NetMobsspawner> ();
		netmobSpawner.SpawnNextWave ();
	}


}
