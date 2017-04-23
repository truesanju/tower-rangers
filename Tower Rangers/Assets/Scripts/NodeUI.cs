using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	public GameObject nodeUIPanel;

	public void Show ()
	{
		nodeUIPanel.SetActive (true);
	}

	public void Hide ()
	{
		nodeUIPanel.SetActive (false);
	}

}
