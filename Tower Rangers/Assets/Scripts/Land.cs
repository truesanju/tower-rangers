using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Land : MonoBehaviour {

    public Color hoverColor;

    public Color notEnoughGoldColor;

    public Vector3 positionOffset;


    [HideInInspector]

    public GameObject tower;

    [HideInInspector]

    public TowerBlueprint towerblueprint;

    //[HideInInspector]

    //public bool isUpgraded = false;


    LandManager landmanager;

    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;

        landmanager = LandManager.instance;

    }



    public Vector3 GetBuildPosition()

    {
        return transform.position + positionOffset;
    }



    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())

            return;

        //if there is already a tower there
        if (tower != null)
        {
            landmanager.selectland(this);
            return;
        }

        
        if (!landmanager.CanBuild)
            return;

        BuildTower(landmanager.GetTowerToBuild());

    }



    void BuildTower(TowerBlueprint blueprint)

    {
        //cnt build tower
        if (PlayerStats.Gold < blueprint.cost)

        {
            Debug.Log("Not enough gold");

            return;
        }

        //can build tower
        PlayerStats.Gold -= blueprint.cost;



        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        tower = _tower;

        towerblueprint = blueprint;



        GameObject effect = (GameObject)Instantiate(landmanager.buildEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 5f);

        Debug.Log("Tower build!");

    }

   
    void OnMouseEnter()

    {

        if (EventSystem.current.IsPointerOverGameObject())

            return;

        if (!landmanager.CanBuild)

            return;


        if (landmanager.HasGold)

        {

            rend.material.color = hoverColor;

        }
        else

        {

            rend.material.color = notEnoughGoldColor;

        }



    }



    void OnMouseExit()

    {

        rend.material.color = startColor;

    }



}
