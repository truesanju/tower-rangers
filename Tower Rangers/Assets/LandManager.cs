using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Offensive towers:
    //public GameObject airtowerprefab;
    //public GameObject woodtowerprefab;

    //remove upon button
    //void Start() {
    //towertobuild = towerprefab;
    //}

    //private GameObject towertobuild;

    //public GameObject gettowertobuild() {
    //return towertobuild;
    //}

    //public void settowertobuild(GameObject tower) {
    //towertobuild = tower;
    //}
    private TowerBlueprint towertobuild;


    public bool canbuild {
        get { return towertobuild != null; } //get boolean result. 
    }

    public bool haveenoughgold
    {
        get { return SinglePlayer.gold >= towertobuild.cost; } //get boolean result. 
    }

    public void buildtoweron(land landd) {

        if (SinglePlayer.gold < towertobuild.cost)
        {
            Debug.Log("not enough gold");
            return;
        }

        SinglePlayer.gold -= towertobuild.cost;

        GameObject tower = (GameObject)Instantiate(towertobuild.prefab, landd.getbuildposition(), Quaternion.identity);
        landd.tower=tower;

        Debug.Log("tower build. gold left = "+ SinglePlayer.gold);
    }

    public void selecttowertobuild(TowerBlueprint tower) {
        towertobuild = tower;
     }
    

}
