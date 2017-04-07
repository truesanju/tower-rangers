using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class land : MonoBehaviour {

    //define color in inspector
    public Color colorwhenhovering;
    public Color colorwhennotenoughgold;
    public Vector3 positionOffset;

    [Header("optional in inspector")]
    public GameObject tower;

    //do not change to renderer
    private Renderer rend;
    //initial color of node
    private Color startingColor;

    LandManager landmanager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;

        landmanager = LandManager.instance;
    }

    public Vector3 getbuildposition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //if (landmanager.gettowertobuild() == null)
            //return;
        if (!landmanager.canbuild)
            return;

        if (tower != null) {
            Debug.Log("a tower is already there");
            return;
        }

        landmanager.buildtoweron(this);
        //else, build tower: 
        //GameObject towertobuild = landmanager.gettowertobuild();
        //cast to GameObject
        //can change position offset of tower in land inspector
        //tower =(GameObject)Instantiate(towertobuild, transform.position+ positionOffset, transform.rotation);
        Debug.Log("on mouse down");
    }

    void OnMouseEnter()
    {   
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        //to ensure hover colour only changes when there is tower to be built
        //ie must select tower first
        //if (landmanager.gettowertobuild() == null)
            //return;

        if (!landmanager.canbuild)
            return;

        if (landmanager.haveenoughgold)
        { 
            rend.material.color = colorwhenhovering;
        }
        else
        {
            rend.material.color = colorwhennotenoughgold;
        }


        Debug.Log("on mouse enter");
     }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
        Debug.Log("on mouse exit");
    }
}
