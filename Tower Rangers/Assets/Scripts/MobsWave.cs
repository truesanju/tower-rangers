using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //to show in inspector
public class Wave {

    public GameObject mobsprefab;   
    public int mobscount;  //amount of mobs to spawn
    public float mobsrate;      //time between mobs
	
}
