using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	//[SerializeField]
	public Image content;

	//[SyncVar(hook="updateHealth")]
	private static float currentHealth;


	// Use this for initialization
	void Start () {
		//currentHealth = PlayerLedger.hp;
	}




	// Update is called once per frame
	void Update () {
		/*
		try {
			content.fillAmount = Map (PlayerLedger.hp, 0, 100, 0, 1);
		} catch (NullReferenceException e) {
			Debug.Log ("Null exception caught");
		}
		*/


		if (currentHealth != PlayerLedger.hp) {
			try {
				content.fillAmount = Map (PlayerLedger.hp, 0, 100, 0, 1);
				currentHealth = PlayerLedger.hp;
			} catch (NullReferenceException e) {
				Debug.Log ("Null exception caught");
			}
		}

	}
	

	/*
	void updateHealth () {
		//content.fillAmount = Map (PlayerLedger.hp, 0, 100, 0, 1);
		if (currentHealth != PlayerLedger.hp) {
			try {
				content.fillAmount = Map (PlayerLedger.hp, 0, 100, 0, 1);
				currentHealth = PlayerLedger.hp;
			} catch (NullReferenceException e) {
				Debug.Log ("Null exception caught");
			}
		}
	}
	*/

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
