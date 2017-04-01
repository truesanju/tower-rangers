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
	[SyncVar]
	int PlayerId = 0;
	public static HashSet<Player> ActivePlayers = new HashSet<Player>();

	//Networking local player objects
	public Camera gameCamera;
	public AudioListener gameAudioListener;


	public override void OnStartLocalPlayer(){
		//turn on our camera and audiolistener for the local player
		gameCamera.enabled = true;
		gameAudioListener.enabled = true;
		gameObject.name = "LOCAL player";

		//TODO get local player id


		//Debug.Log ("My game id is " + PlayerId);
		//TODO, spawn the camera (and alloc map) based off player id 

		//set our player controller and camera
		transform.position = new Vector3 (-34.46f, 47.63f, -72.01f);
		transform.eulerAngles = new Vector3 (0f,46.92f, 0f);
		gameCamera.transform.eulerAngles = new Vector3 (56.13f, 46.92f, 0f);
		initialCameraPt = transform.position;

		base.OnStartLocalPlayer ();
	}

	void Start(){
		ActivePlayers.Add (this);
		if (isServer) {
			PlayerId = ActivePlayers.Count;
		}
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
