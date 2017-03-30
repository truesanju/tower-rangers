using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCam : MonoBehaviour {

    private Vector3 initialCameraPt;
    public float sensitivity = 2.0f;
    private Vector3 dragStartPt;
    private Vector3 position;

    private void Start()
    {
        initialCameraPt = transform.position;
    }

    // Update is called once per frame
    

	// Update is called once per frame
	void Update () {


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

        position = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragStartPt);
        Vector3 motion = new Vector3(position.x * sensitivity, position.y * sensitivity, 0);
        
        transform.Translate(motion,Space.Self);
        

    }
}
