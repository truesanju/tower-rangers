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
    //public Gameobject icetowerprefab;
    //public Gameobject lightningtowerprefab;
    //public Gameobject firetowerprefab;
    //public Gameobject poisontowerprefab;

    //Offensive towers:
    //public Gameobject airtowerprefab;
    //public Gameobject woodtowerprefab;

    void Start() {
        towertobuild = basictowerprefab;
    }

    private GameObject towertobuild;

    public GameObject gettowertobuild() {
        return towertobuild;
    }


    

}
