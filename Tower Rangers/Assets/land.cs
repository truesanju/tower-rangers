using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//land script to be attached to node prefab or map?
public class land : MonoBehaviour {

    //define color in inspector
    public Color colorwhenhovering;
    public Vector3 positionOffset;

    private GameObject tower;



    //do not change to renderer
    private Renderer rend;
    //initial color of node
    private Color startingColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (tower != null) {
            Debug.Log("a tower is already there");
            return;
        }

        //else, build tower
        GameObject towertobuild = LandManager.instance.gettowertobuild();
        //cast to GameObject
        //can change position offset of tower in land inspector
        tower=(GameObject)Instantiate(towertobuild, transform.position+ positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = colorwhenhovering;
     }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
    }
}
