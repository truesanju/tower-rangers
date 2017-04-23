
using UnityEngine;

public class NetBuyDefensiveTower : MonoBehaviour {
	
    //create ref to landmanager
	LocalLandManager landmanager;
    consoleUI consoleui;

    void Start() {
        landmanager = LocalLandManager.instance;
        consoleui = consoleUI.instance;
    }

    public void SelectBasicTower()
    {
        consoleui.consoletext.text = "CONSOLE: Basic tower selected. Click on a land node to build a basic tower." ;
        //Debug.Log("basic tower selected");
        //landmanager.settowertobuild(landmanager.basictowerprefab);
		landmanager.buildingDefensiveTower = true;
		landmanager.selecttowertobuild(1);
    }
    public void SelectIceTower()
    {
        consoleui.consoletext.text = "CONSOLE: Ice tower selected. Click on a land node to build a ice tower. ";
        //Debug.Log("ice tower selected");
        //landmanager.settowertobuild(landmanager.icetowerprefab);
		landmanager.buildingDefensiveTower = true;
		landmanager.selecttowertobuild(2);

    }
    public void SelectLightningTower()
    {
        consoleui.consoletext.text = "CONSOLE: Lightning tower selected. Click on a land node to build a lightning tower. ";
        //Debug.Log("lightning tower selected");
        //landmanager.settowertobuild(landmanager.lightningtowerprefab);
        //landmanager.selecttowertobuild(lightningtower,"lightningtower");
		landmanager.buildingDefensiveTower = true;
		landmanager.selecttowertobuild(3);
    }
    public void SelectFireTower()
    {
        consoleui.consoletext.text = "CONSOLE: Fire tower selected. Click on a land node to build a fire tower. ";
        //Debug.Log("fire tower selected");
        //landmanager.settowertobuild(landmanager.firetowerprefab);
		landmanager.buildingDefensiveTower = true;
		landmanager.selecttowertobuild(4);

    }
    public void SelectPoisonTower()
    {
        consoleui.consoletext.text = "CONSOLE: Poison tower selected. Click on a land node to build a poison tower. ";
        //Debug.Log("poison tower selected");
        //landmanager.settowertobuild(landmanager.poisontowerprefab);
		landmanager.buildingDefensiveTower = true;
		landmanager.selecttowertobuild(5);
    }
}
