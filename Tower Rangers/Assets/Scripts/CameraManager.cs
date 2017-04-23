using UnityEngine;
using UnityEngine.Networking;

public class CameraManager : NetworkBehaviour {
	public static int PlayerId = 1;
	public GameObject OtherPlayerController;

	//Used on button click, to request target player to broadcast the map
	public void ClickNextCamera(){
		//GameObject localPlayer = GameObject.Find ("LOCAL player");
		//PlayerScript OurPlayer = localPlayer.GetComponent<PlayerScript> ();
		//if (OurPlayer == null) {
	//		Debug.Log ("PLAYER NULL!");
//			return;
//		}
//		Debug.Log ("PLAYER"+OurPlayer.PlayerId+"requesting a camera!");
//		CmdRequestMap (OurPlayer.PlayerId, OurPlayer.PlayerId + 1);

		Debug.Log ("PLAYER"+PlayerId+"requesting a camera!");
		GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn2");

		CmdSpawnCamera ();

		//Instantiate (OtherPlayerController, SpawnPoint.transform.position, SpawnPoint.transform.rotation, 0);

		//CmdRequestMap (PlayerId, PlayerId + 1);
	}

	[Command]
	void CmdSpawnCamera(){
		GameObject SpawnPoint = GameObject.FindGameObjectWithTag ("Spawn2");
		var go = (GameObject)Instantiate (OtherPlayerController, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

		NetworkServer.SpawnWithClientAuthority (go, connectionToClient);

	}

	[Command]
	void CmdRequestMap(int RequestingPlayer, int TargetPlayer){
		Debug.Log ("Server received Player"+RequestingPlayer+"requesting map from Player"+TargetPlayer);
		RpcBroadcastMap (RequestingPlayer, TargetPlayer);
	}


	[ClientRpc]
	void RpcBroadcastMap(int RequestingPlayer, int TargetPlayer){
		if (TargetPlayer != PlayerId)
			return;
		Debug.Log ("Player" + PlayerId+"received request for map from Player"+RequestingPlayer);
		//TODO broadcast 
	}
}
