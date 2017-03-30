using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ViewScript : NetworkBehaviour {


	private Vector3 initialCameraPt;
	public float sensitivity = 2.0f;
	private Vector3 dragStartPt;
	private Vector3 position;

	//public float ViewSpeed = 1.0f;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			GetComponentInChildren<Camera> ().gameObject.SetActive (false);
		else {
			transform.position = new Vector3 (-34.46f, 47.63f, -72.01f);
			//transform.eulerAngles = new Vector3 (40.84f, 43.831f, 0f);
			transform.eulerAngles = new Vector3 (0f,46.92f, 0f);
			GetComponentInChildren<Camera> ().gameObject.transform.eulerAngles = new Vector3 (56.13f, 46.92f, 0f);
			initialCameraPt = transform.position;
				}

	}
	
	// Update is called once per frame
	void Update () {
		//transform based off how much we move
		if (!isLocalPlayer) return;

		//var camMoveAmount = Input.GetAxis("Vertical") * Time.deltaTime * ViewSpeed;

		//transform.Translate(camMoveAmount, 0, camMoveAmount, Space.World);

		if (Input.GetMouseButtonDown(0))
		{
			dragStartPt = Input.mousePosition;
			return;
		}

		if (Input.GetMouseButtonDown(1))
		{
			transform.position = initialCameraPt;
			return;
		}

		if (!Input.GetMouseButton(0)) { return; }

		//Sanjay's singleplayer camera
		//position = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragStartPt);
		//Vector3 motion = new Vector3(position.x * sensitivity, position.y * sensitivity, 0);
		//transform.Translate(motion,Space.Self);

		//Networked Version
		position = GetComponentInChildren<Camera> ().ScreenToViewportPoint(Input.mousePosition - dragStartPt);

		//With X movement
		//Vector3 motion = new Vector3(position.x * sensitivity,0, position.y * sensitivity);
		Vector3 motion = new Vector3(0,0, position.y * sensitivity);
		transform.Translate(motion,Space.Self);
	}
}
