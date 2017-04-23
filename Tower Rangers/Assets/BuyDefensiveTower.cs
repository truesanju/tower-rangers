
using UnityEngine;

public class BuyDefensiveTower : MonoBehaviour {

    public TowerBlueprint basictower;
    public TowerBlueprint icetower;
    public TowerBlueprint lightningtower;
    public TowerBlueprint firetower;
    public TowerBlueprint poisontower;


    //create ref to landmanager
    LandManager landmanager;
    consoleUI consoleui;

    void Start() {
        landmanager = LandManager.instance;
        consoleui = consoleUI.instance;
    }

    public void SelectBasicTower()
    {
        consoleui.consoletext.text = "CONSOLE: Basic tower selected. Click on a land node to build a basic tower." ;
        //Debug.Log("basic tower selected");
        //landmanager.settowertobuild(landmanager.basictowerprefab);
        landmanager.selecttowertobuild(basictower);
    }
    public void SelectIceTower()
    {
        consoleui.consoletext.text = "CONSOLE: Ice tower selected. Click on a land node to build a ice tower. ";
        //Debug.Log("ice tower selected");
        //landmanager.settowertobuild(landmanager.icetowerprefab);
        landmanager.selecttowertobuild(icetower);

    }
    public void SelectLightningTower()
    {
        consoleui.consoletext.text = "CONSOLE: Lightning tower selected. Click on a land node to build a lightning tower. ";
        //Debug.Log("lightning tower selected");
        //landmanager.settowertobuild(landmanager.lightningtowerprefab);
        landmanager.selecttowertobuild(lightningtower);

    }
    public void SelectFireTower()
    {
        consoleui.consoletext.text = "CONSOLE: Fire tower selected. Click on a land node to build a fire tower. ";
        //Debug.Log("fire tower selected");
        //landmanager.settowertobuild(landmanager.firetowerprefab);
        landmanager.selecttowertobuild(firetower);

    }
    public void SelectPoisonTower()
    {
        consoleui.consoletext.text = "CONSOLE: Poison tower selected. Click on a land node to build a poison tower. ";
        //Debug.Log("poison tower selected");
        //landmanager.settowertobuild(landmanager.poisontowerprefab);
        landmanager.selecttowertobuild(poisontower);


    }
}
