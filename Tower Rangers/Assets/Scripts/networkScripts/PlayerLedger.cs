using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLedger : MonoBehaviour {

    public static float gold;
    //to be edited in inspector
    public float initialgold = 100;

    public static int hp;
    public int initialhp = 100;

    void Start() {
        gold = initialgold;
        hp = initialhp;
		GameManager.gameStart = true;
    }

}
