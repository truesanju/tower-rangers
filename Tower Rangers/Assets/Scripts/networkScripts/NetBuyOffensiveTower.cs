using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetBuyOffensiveTower : MonoBehaviour {

    PlayerScript player;
	LocalLandManager landmanager;
	consoleUI consoleui;

	void Start() {
		landmanager = LocalLandManager.instance;
		consoleui = consoleUI.instance;
	}


    public void SelectAirTower()
	{
		consoleui.consoletext.text = "CONSOLE: Air tower selected. Click on an enemy land node to build an air tower. ";
		landmanager.buildingDefensiveTower = false;
		landmanager.selecttowertobuild(6);
    }

    public void SelectEarthTower()
	{
		consoleui.consoletext.text = "CONSOLE: Earth tower selected. Click on an enemy land node to build an earth tower. ";
		landmanager.buildingDefensiveTower = false;
		landmanager.selecttowertobuild(7);
    }
}

