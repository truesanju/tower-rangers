using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hpUI : MonoBehaviour {

    public Text hptext;

    void Update()
    {

		hptext.text = "HP: " + PlayerLedger.hp.ToString();
	}
}
