using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ViewScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			GetComponentInChildren<Camera> ().gameObject.SetActive (false);
		else {
			transform.position = new Vector3 (85.91463f, 156.7806f, 71.16507f);
			transform.eulerAngles = new Vector3 (43.419f, -143.422f, 0f);
				}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
