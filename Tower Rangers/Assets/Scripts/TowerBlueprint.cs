using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //save and load values, to be edited in inspector
// no monobehaviour: not component in unity, not to be attached to any gameobject
public class TowerBlueprint {

    //@ BuyDefensiveTower(Script) fields
    public GameObject prefab;
    public float cost;

    public float sellamount()
    {
        return cost / 2;
    }

	public float upgradeamount()
	{
		return cost;
	}
		
}
