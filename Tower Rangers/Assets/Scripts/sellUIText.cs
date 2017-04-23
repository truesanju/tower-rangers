using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sellUIText : MonoBehaviour
{

	public Text sellText;
	
	// Update is called once per frame
	void Update ()
	{
		sellText.text = LocalLandManager.instance.getSellCost();
	}
}

