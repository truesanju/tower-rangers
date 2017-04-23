using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeUIText : MonoBehaviour
{

	public Text upgradeText;
	
	// Update is called once per frame
	void Update ()
	{
		upgradeText.text = LocalLandManager.instance.getUpgradeCost();
	}
}

