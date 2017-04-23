using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//attached to gamemaster
public class LocalLandManager : MonoBehaviour {

	public static LocalLandManager instance;

    void Awake() {
        if (instance != null){
            Debug.LogError("there is more than 1 landmanager in scene");
            return;
        }
        instance = this;
        
    }

    //instantiate all towers

    //Defensive towers:

	public TowerBlueprint basictowerprefab;
	public TowerBlueprint icetowerprefab;
	public TowerBlueprint lightningtowerprefab;
	public TowerBlueprint firetowerprefab;
	public TowerBlueprint poisontowerprefab;
	public TowerBlueprint airtowerprefab;
	public TowerBlueprint earthtowerprefab;


    private TowerBlueprint towertobuild;
	private int buildingtowerindex;


	public GameObject towerPanel;
	private land selectedNode;
	public bool buildingDefensiveTower;

	public static PlayerScript player;


	public bool canbuild(int landOwner) {
		//building defensive tower on own land
		if (buildingDefensiveTower && player.PlayerId == landOwner)
			return true;
		//building offensive tower on other people's land
		if (!buildingDefensiveTower && player.PlayerId != landOwner)
			return true;
		
		return false;
    }

	public bool haveenoughgold
    {
        get { return PlayerLedger.gold >= towertobuild.cost; } //get boolean result. 
    }

    public void buildtoweron(land landd) {
		if (PlayerLedger.gold < towertobuild.cost)
        {
            //Debug.Log("not enough gold");
            return;
        }

		PlayerLedger.gold -= towertobuild.cost;
		Vector3 position = landd.getbuildposition ();
		GameObject tower = (GameObject)Instantiate(towertobuild.prefab, position, Quaternion.identity);
		landd.tower=tower;

		tower.GetComponent<Owner> ().ownerId = player.PlayerId;

		//get local player object, Send the command to spawn the tower on netlandledger
		player.GetComponentInChildren<NetLandManager> ().netbuildtoweron(buildingtowerindex, position, player.PlayerId);
    }


	public void selecttowertobuild(int towerindex) {
		buildingtowerindex = towerindex;
		switch (towerindex){
		case 1:
			towertobuild = basictowerprefab;
			break;
		case 2:
			towertobuild = icetowerprefab;
			break;
		case 3:
			towertobuild = lightningtowerprefab;
			break;
		case 4:
			towertobuild = firetowerprefab;
			break;
		case 5:
			towertobuild = poisontowerprefab;
			break;
		case 6:
			towertobuild = airtowerprefab;
			break;
		case 7:
			towertobuild = earthtowerprefab;
			break;
		}
	}

	public void networkbuildTower(int towerindex, Vector3 position, int ownerId){
		//Debug.Log ("Someone else is spawning tower");
		GameObject tower = (GameObject)Instantiate(getTowerFromIndex(towerindex).prefab, position, Quaternion.identity);
		tower.GetComponent<Owner> ().ownerId = ownerId;
		land landnode;
		Collider[] hitColliders = Physics.OverlapSphere(position, 0.2f);
		if (hitColliders.Length == 1) {
			hitColliders [0].gameObject.GetComponent<land> ().tower = tower;
			//TODO mark land as having a tower built on it
		}
	
	}

	public TowerBlueprint getTowerFromIndex(int towerindex) {
		switch (towerindex){
		case 1:
			return basictowerprefab;
			break;
		case 2:
			return icetowerprefab;
			break;
		case 3:
			return lightningtowerprefab;
			break;
		case 4:
			return firetowerprefab;
			break;
		case 5:
			return poisontowerprefab;
			break;
		case 6:
			return airtowerprefab;
			break;
		case 7:
			return earthtowerprefab;
			break;

		}
		return null;
	}
	public void selecttowertobuild(TowerBlueprint tower) {
		towertobuild = tower;
	}

	//Eiros: Testing this
	public TowerBlueprint getTowerBlueprint() {
		return towertobuild;
	}

	//Eiros: Testing this
	public int getTowerIndex() {
		return buildingtowerindex;
	}

	public void selectNode (land landd)
	{
		bool youOwnTheTower = (landd.tower.GetComponent<Owner> ().ownerId == player.PlayerId);
		bool OffTowerInYourLand = false;

		if (landd.transform.parent.gameObject.name.Equals("Player1Map")&&player.PlayerId == 1) {
			OffTowerInYourLand = true;
		}
		if (landd.transform.parent.gameObject.name.Equals("Player2Map")&&player.PlayerId == 2) {
			OffTowerInYourLand = true;
		}
		if (landd.transform.parent.gameObject.name.Equals("Player3Map")&&player.PlayerId == 3) {
			OffTowerInYourLand = true;
		}
		if (landd.transform.parent.gameObject.name.Equals("Player4Map")&&player.PlayerId == 4) {
			OffTowerInYourLand = true;
		}
		if (youOwnTheTower || OffTowerInYourLand) {
			selectedNode = landd;
			towertobuild = null;

			//Displaying tower range
			int towerIndex = selectedNode.towerIndex;

			switch (towerIndex){
			case 1:
				break;
			case 2:
				selectedNode.tower.GetComponent<FrostTower>().showRange();
				break;
			case 3:
				selectedNode.tower.GetComponent<LightningTower>().showRange();
				break;
			case 4:
				selectedNode.tower.GetComponent<Tower>().showRange();
				break;
			case 5:
				selectedNode.tower.GetComponent<PoisonTower>().showRange();
				break;
			case 6:
				selectedNode.tower.GetComponent<GasTower>().showRange();
				break;
			case 7:
				selectedNode.tower.GetComponent<DarkTower>().showRange();
				break;

			}


			towerPanel.SetActive (true);
		}
	}

	public void sellTower ()
	{
		selectedNode.selltower ();

		deselectTower ();
	}

	public void upgradeTower ()
	{
		int towerIndex = selectedNode.towerIndex;

		if (selectedNode.upgradetower ()) {
			switch (towerIndex) {
			case 1:
				break;
			case 2:
				selectedNode.tower.GetComponent<FrostTower> ().upgradeTower ();
				break;
			case 3:
				selectedNode.tower.GetComponent<LightningTower> ().upgradeTower ();
				break;
			case 4:
				selectedNode.tower.GetComponent<Tower> ().upgradeTower ();
				break;
			case 5:
				selectedNode.tower.GetComponent<PoisonTower> ().upgradeTower ();
				break;
			case 6:
				selectedNode.tower.GetComponent<GasTower> ().upgradeTower ();
				break;
			case 7:
				selectedNode.tower.GetComponent<DarkTower> ().upgradeTower ();
				break;

			}
		}

		deselectTower ();

	}

	public string getUpgradeCost() {
		float cost = selectedNode.towerblueprint.upgradeamount ();
		return "<b>UPGRADE</b>\n" + cost.ToString() + " G";
	}

	public string getSellCost() {
		float cost = selectedNode.towerblueprint.sellamount ();
		return "<b>SELL</b>\n" + cost.ToString() + " G";
	}

	public void deselectTower() {
		int towerIndex = selectedNode.towerIndex;

		switch (towerIndex){
		case 1:
			break;
		case 2:
			selectedNode.tower.GetComponent<FrostTower>().hideRange();
			break;
		case 3:
			selectedNode.tower.GetComponent<LightningTower>().hideRange();
			break;
		case 4:
			selectedNode.tower.GetComponent<Tower>().hideRange();
			break;
		case 5:
			selectedNode.tower.GetComponent<PoisonTower>().hideRange();
			break;
		case 6:
			selectedNode.tower.GetComponent<GasTower>().hideRange();
			break;
		case 7:
			selectedNode.tower.GetComponent<DarkTower>().hideRange();
			break;

		}

		selectedNode = null;

		towerPanel.SetActive (false);
	}

}
