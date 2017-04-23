using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuyOffensiveTower : NetworkBehaviour {
    public TowerBlueprint airtower;
    public TowerBlueprint earthtower;

    PlayerScript player;

    consoleUI consoleui;

    public void SelectAirTower()
    {
        //if player is on own map
        if (isLocalPlayer)
        {
            consoleui.consoletext.text = "CONSOLE : You cannot build an offensive tower on your own map. Please toggle to opponent's map.";
        }


        else
            consoleui.consoletext.text = "CONSOLE: Air tower selected. Click on a land node to build an Air tower.";

            
    }

    public void SelectEarthTower()
    {

        if (isLocalPlayer)
        {
            consoleui.consoletext.text = "CONSOLE : You cannot build an offensive tower on your own map. Please toggle to opponent's map.";
        }

        else

        consoleui.consoletext.text = "CONSOLE: Earth tower selected. Click on a land node to build an Earth tower.";

    }



}

