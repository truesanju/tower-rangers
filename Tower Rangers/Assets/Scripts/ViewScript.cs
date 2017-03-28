using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ViewScript : NetworkBehaviour {

	public float ViewSpeed = 1.0f;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			GetComponentInChildren<Camera> ().gameObject.SetActive (false);
		else {
			transform.position = new Vector3 (-38.19f, 91.151f, -104.5f);
			transform.eulerAngles = new Vector3 (40.84f, 43.831f, 0f);
				}
	}
	
	// Update is called once per frame
	void Update () {
		//transform based off how much we move
		if (!isLocalPlayer) return;

		var camMoveAmount = Input.GetAxis("Vertical") * Time.deltaTime * ViewSpeed;

		transform.Translate(camMoveAmount, 0, camMoveAmount, Space.World);

	}
}
