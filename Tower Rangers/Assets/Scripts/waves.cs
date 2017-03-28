using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //to show in inspector for editing
public class Wave {

    //reference to mobs prefab
    public GameObject mobs;

    //how many of mobs per spawn
    public int count;

    //spawnrate
    public float rate;



}
