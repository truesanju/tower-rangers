using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandManager : MonoBehaviour {

    public static LandManager instance;

    void Awake()

    {
        if (instance != null)
        {
            Debug.LogError("More than one Land Manager in scene!");
            return;
        }
        instance = this;
    }


    public GameObject buildEffect;

    //public GameObject sellEffect;

    private TowerBlueprint towerToBuild;

    private Land selectedland;



    public LandUI landUI;



    public bool CanBuild { get { return towerToBuild != null; } }

    public bool HasGold { get { return PlayerStats.Gold >= towerToBuild.cost; } }



    public void selectland(Land land)

    {

        if (selectedland == land)

        {

            DeselectNode();

            return;

        }


        selectedland = land;

        towerToBuild = null;

        LandUI.SetTarget(land);

    }



    public void DeselectNode()

    {

        selectedland = null;

        LandUI.Hide();

    }



    public void SelectTowerToBuild(TowerBlueprint tower)

    {
        towerToBuild = tower;
        DeselectNode();
    }



    public TowerBlueprint GetTowerToBuild()

    {
        return towerToBuild;
    }



}
