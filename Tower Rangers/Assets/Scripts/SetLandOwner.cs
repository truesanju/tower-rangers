using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLandOwner : MonoBehaviour {
	public int playerId;

	void Start () {
		foreach (Transform nodeTransform in transform) {
			nodeTransform.gameObject.GetComponent<land> ().SetLandOwner (playerId);
		}
	}
}