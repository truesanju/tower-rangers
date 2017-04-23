using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class consoleUI : MonoBehaviour {

    public static consoleUI instance;

    void Awake()
    {
        instance = this;
    }

    public Text consoletext;
   
       
}
