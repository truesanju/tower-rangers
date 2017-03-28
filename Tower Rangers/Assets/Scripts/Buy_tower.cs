using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_tower : MonoBehaviour {

    public TowerBlueprint towerblueprint;

    LandManager landmanager;


    void Start()

    {

        landmanager = LandManager.instance;

    }



    public void SelectTower()

    {

        Debug.Log("Standard Turret Selected");

        landmanager.SelectTowerToBuild(towerblueprint);

    }
}
