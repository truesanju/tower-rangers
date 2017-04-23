using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : MonoBehaviour {


    [Header("Tower setup")]
    public float range = 8f;
    public float level = 1;
    public int cost = 100;

    [Header("Material setup")]
    private ParticleSystem.ShapeModule frostSM;
    private Material material;
    private Material nodeMat;
    private Material roadMat;
    private Renderer[] renderers;
	private Owner owner;

    // Use this for initialization
    void Start () {
        frostSM = gameObject.GetComponentInChildren<ParticleSystem>().shape;
        renderers = transform.GetComponentsInChildren<Renderer>();
        material = transform.GetComponentInChildren<Renderer>().material;
        nodeMat = GameObject.FindGameObjectWithTag("Node").GetComponent<Renderer>().material;
        roadMat = GameObject.FindGameObjectWithTag("Road").GetComponent<Renderer>().material;
		owner = gameObject.GetComponent<Owner> ();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTarget();
	}

  
    public void upgradeTower()
    { 
        if (level == 3)
        {
            Debug.Log("Tower is a max level");
        }
        else
        {
            //Scale model

            Vector3 originalScale = transform.localScale;
            transform.localScale += new Vector3(0, 15.0f);


            foreach (Renderer r in renderers)
            {
                Material temp = r.material;
                temp.SetColor("_EmissionColor", temp.color * 0.10f * level);
                r.material = temp;
            }

            range += 6f;
            frostSM.radius += range/2;
            
            level++;
            return;
        }
    }

  
    private void colorNodesInRange(Material _m)
    {
        GameObject[] nodesInRange = GameObject.FindGameObjectsWithTag("Node");


        foreach (GameObject node in nodesInRange)
        {

            float distanceToNode = Vector3.Distance(node.transform.position, transform.position);

            if (distanceToNode < range)
            {
                Renderer r = node.GetComponent<Renderer>();
                r.material = _m;
            }
        }
    }

    void UpdateTarget()
    {
        mobs[] allMobs = GameObject.FindObjectsOfType<mobs>();
       

        foreach (mobs mob in allMobs)
        {
            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);
            Material mobMat = mob.GetComponentInChildren<Renderer>().material;

			int mobOwner = mob.gameObject.GetComponent<NetMobPath> ().OwnerId;
			//Debug.Log ("Mob owner is " + mobOwner + "and the targeting tower is from player" + owner.ownerId);

			if (distanceToMob < range && owner.ownerId == mobOwner)
            //if (distanceToMob < range)
            {
                
                if (level == 1)
                {
                    mob.Slow(0.6f);
                }
                if (level == 2)
                {
                    mob.Slow(0.4f);
                }
                if (level == 3)
                {
                    mob.Slow(0.25f);
                }

            } 

            
        }



    }

	/*
    private void OnMouseDown()
    {
        upgradeTower();
    }

    private void OnMouseOver()
    {
        colorNodesInRange("Road", material);
    }

    private void OnMouseExit()
    {
        colorNodesInRange("Road", roadMat);
    }
    */

	public void showRange()
	{
		colorNodesInRange("Road", material);
	}

	public void hideRange()
	{
		colorNodesInRange("Road", roadMat);
	}

    public void colorNodesInRange(string tag, Material _m)
    {
        GameObject[] nodesInRange = GameObject.FindGameObjectsWithTag(tag);


        foreach (GameObject node in nodesInRange)
        {

            float distanceToNode = Vector3.Distance(node.transform.position, transform.position);

            if (distanceToNode < range)
            {
                Renderer r = node.GetComponent<Renderer>();
                r.material = _m;
            }
        }
    }

}
