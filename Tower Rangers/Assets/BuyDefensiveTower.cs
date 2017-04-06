
using UnityEngine;

public class BuyDefensiveTower : MonoBehaviour {

    public TowerBlueprint basictower;
    public TowerBlueprint icetower;
    public TowerBlueprint lightningtower;
    public TowerBlueprint firetower;
    public TowerBlueprint poisontower;


    //create ref to landmanager
    LandManager landmanager;

    void Start() {
        landmanager = LandManager.instance;
    }

    public void SelectBasicTower()
    {
        Debug.Log("basic tower selected");
        //landmanager.settowertobuild(landmanager.basictowerprefab);
        landmanager.selecttowertobuild(basictower);
    }
    public void SelectIceTower()
    {
        Debug.Log("ice tower selected");
        //landmanager.settowertobuild(landmanager.icetowerprefab);
        landmanager.selecttowertobuild(icetower);

    }
    public void SelectLightningTower()
    {
        Debug.Log("lightning tower selected");
        //landmanager.settowertobuild(landmanager.lightningtowerprefab);
        landmanager.selecttowertobuild(lightningtower);

    }
    public void SelectFireTower()
    {
        Debug.Log("fire tower selected");
        //landmanager.settowertobuild(landmanager.firetowerprefab);
        landmanager.selecttowertobuild(firetower);

    }
    public void SelectPoisonTower()
    {
        Debug.Log("poison tower selected");
        //landmanager.settowertobuild(landmanager.poisontowerprefab);
        landmanager.selecttowertobuild(poisontower);


    }
}
