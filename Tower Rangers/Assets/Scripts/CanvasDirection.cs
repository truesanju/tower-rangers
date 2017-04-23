using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDirection : MonoBehaviour {

	Quaternion direction;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		direction = Quaternion.Euler(-135.0f, 45.0f, 0.0f);
		transform.rotation = direction;
	}
}
