using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{

    //define color in inspector
    public Color colorwhenhovering;
    public Vector3 positionOffset;

    //do not change to renderer
    private Renderer rend;
    //initial color of node
    private Color startingColor;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = colorwhenhovering;
        Debug.Log("on mouse enter");
    }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
        Debug.Log("on mouse exit");
    }
}
