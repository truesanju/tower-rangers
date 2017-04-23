using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameover;
    public GameObject gameoverpanel;
	public static bool gameStart;
    void Start() {
        gameover = false;
        }


    void Update() {
		if (gameover)
			return;
        if (!gameStart)
            return;

		if (PlayerLedger.hp <= 0) {
            end();
        }
    }

    void end()
    {
        gameover = true;
        Debug.Log("game over");
        gameoverpanel.SetActive(true);
		PlayerScript player = GameObject.Find ("LOCAL player").GetComponent<PlayerScript> ();
		player.CmdPlayerDied (player.PlayerId);
    }

}
