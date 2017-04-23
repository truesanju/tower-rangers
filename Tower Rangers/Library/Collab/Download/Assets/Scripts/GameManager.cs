using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameover;
    public GameObject gameoverpanel;
    void Start() {
        gameover = false;
        }

    void Update() {
        if (gameover)
            return;

		if (PlayerLedger.hp <= 0) {
            end();
        }
    }

    void end()
    {
        gameover = true;
        Debug.Log("game over");
        //gameoverpanel.SetActive(true);
    }

}
