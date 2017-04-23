using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

//attached to gamemaster
public class LandManager : MonoBehaviour {

    public static LandManager instance;

    void Awake() {

        if (instance != null)
        {
            Debug.LogError("there is more than 1 landmanager in scene");
            return;
        }
        instance = this;
        
    }

    //instantiate all towers

    //Defensive towers:
    public GameObject basictowerprefab;
    public GameObject icetowerprefab;
    public GameObject lightningtowerprefab;
    public GameObject firetowerprefab;
    public GameObject poisontowerprefab;

	public GameObject towerPanel;
   
    private TowerBlueprint towertobuild;

	private land selectedNode;


    public bool canbuild {
        get { return towertobuild != null; } //get boolean result. 
    }

    public bool haveenoughgold
    {
        get { return SinglePlayer.gold >= towertobuild.cost; } //get boolean result. 
    }
	/*
	[Command]
	void CmdSpawnTower(GameObject tower){
		NetworkServer.Spawn (tower);
	}*/

    public void buildtoweron(land landd) {

        if (SinglePlayer.gold < towertobuild.cost)
        {
            //Debug.Log("not enough gold");
            return;
        }

        SinglePlayer.gold -= towertobuild.cost;

        GameObject tower = (GameObject)Instantiate(towertobuild.prefab, landd.getbuildposition(), Quaternion.identity);
	//	CmdSpawnTower (tower);
        landd.tower=tower;

        //Debug.Log("tower build. gold left = "+ SinglePlayer.gold);
    }

    public void selecttowertobuild(TowerBlueprint tower) {
        towertobuild = tower;
     }

	public void selectNode (land landd)
	{
		selectedNode = landd;
		towertobuild = null;

		towerPanel.SetActive (true);
	}

	public void sellTower ()
	{
		selectedNode.selltower ();
		selectedNode = null;

		towerPanel.SetActive (false);
	}
    
}
