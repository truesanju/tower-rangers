using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class land : MonoBehaviour
{

    //define color in inspector
    public Color buildcolor;
    public Color errorcolor;
    public Vector3 positionOffset;

    [Header("optional in inspector")]
    public GameObject tower;
    public TowerBlueprint towerblueprint;
	public int towerIndex;

    //do not change to renderer
    private Renderer rend;
    //initial color of node
    private Color startingColor;

    LocalLandManager landmanager;
    consoleUI consoleui;

	public int LandOwner = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
        landmanager = LocalLandManager.instance;
        consoleui = consoleUI.instance;
        consoleui.consoletext.text = "CONSOLE: Select your type of tower before building! ";

    }

    public Vector3 getbuildposition()
    {
        return transform.position + positionOffset;
    }

    public void selltower()
    {
		PlayerLedger.gold += towerblueprint.sellamount();
    	Destroy(tower);
    	towerblueprint = null;
    }
		
	public bool upgradetower()
	{
		if (PlayerLedger.gold >= towerblueprint.upgradeamount()) {
			PlayerLedger.gold -= towerblueprint.upgradeamount ();
			Debug.Log("Upgrading!");
			return true;
		} else {
			consoleui.consoletext.text = "CONSOLE: You do not have enough gold to upgrade this tower.";
			Debug.Log("Not upgrading");
			return false;
		}

	}

    private float starttime;
    private bool executed;

    void OnMouseDown()
    {   //first click
        //show shaded regions:
        
        //start time upon first click
        starttime = Time.time;
        executed = false;

        //double click:
        if (Time.time - starttime < 0.3)
        {
            Debug.Log("double clicked!");
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (tower != null)
            {
				/*
                rend.material.color = errorcolor;
                //Debug.Log("a tower is already there");
                consoleui.consoletext.text = "CONSOLE: A tower has already been built there. Choose another plot of land. ";
				*/
				landmanager.selectNode (this);
				return;
            }

			//if (landmanager.gettowertobuild() == null)
			//return;
			if (!landmanager.canbuild(LandOwner))
				return;

            if (!landmanager.haveenoughgold)
            {
                rend.material.color = errorcolor;
                consoleui.consoletext.text = "CONSOLE: You do not have enough gold. Kill more monsters!";
                //Debug.Log("not enough gold!");

            }

            else
            {
                rend.material.color = buildcolor;
				towerblueprint = landmanager.getTowerBlueprint ();
				towerIndex = landmanager.getTowerIndex();
                

				landmanager.buildtoweron(this);
                consoleui.consoletext.text = "CONSOLE: Built! Click on another land node to build this tower or select a different type of tower! ";

            }
        }

    }


    void OnMouseExit(){
        rend.material.color = startingColor;
    }

	public void SetLandOwner(int playerId){
		LandOwner = playerId;
	}
}