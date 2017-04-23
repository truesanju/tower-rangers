using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//attached to gamemaster
public class NetLandManager : NetworkBehaviour {
	
    private TowerBlueprint towertobuild;
	private string towertobuildString;
	public LocalLandManager locallandmgr;


	public void netbuildtoweron(int towerIndex, Vector3 position, int towerOwnerId) {
		//CmdDebugLog ("Someone is building");
		CmdSpawnTower (towerIndex,position, towerOwnerId);
    }
		
	[Command]
	void CmdSpawnTower(int towerIndex, Vector3 position, int towerOwnerId){
		RpcSpawnTower (towerIndex, position, towerOwnerId);
	}

	[ClientRpc]
	void RpcSpawnTower(int towerIndex, Vector3 position, int towerOwnerId){
		if (isLocalPlayer) {	//should have already spawned
			return;
		}
		//TODO call the static locallandledger and ask it to build a tower in the location
		locallandmgr = LocalLandManager.instance;
		locallandmgr.networkbuildTower(towerIndex, position, towerOwnerId);
	}

	[Command]
	public void CmdDebugLog(string message){
		Debug.Log (message);
	}
}
