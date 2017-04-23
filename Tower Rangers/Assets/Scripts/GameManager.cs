using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameover;
	public static bool gameStart;
    public GameObject gameoverpanel;
	public GameObject youwinpanel;
	public static PlayerScript player;
	public static bool youWin;

    void Start() {
        gameover = false;
		gameStart = false;
		youWin = false;
        }

    void Update() {
        if (gameover)
            return;
		if (!gameStart)
			return;

		if (youWin) {
			gameover = true;
			youwinpanel.SetActive(true);
		}

		if (PlayerLedger.hp <= 0) {
			gameover = true;
			gameoverpanel.SetActive(true);
			player.CmdPlayerDied (player.PlayerId);
        }
    }
}
