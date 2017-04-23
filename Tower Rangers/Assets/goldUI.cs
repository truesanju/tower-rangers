using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldUI : MonoBehaviour {

    public Text goldtext;

    void Update() {
        goldtext.text = PlayerLedger.gold.ToString();
    }
}
