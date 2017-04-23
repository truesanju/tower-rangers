using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTower : MonoBehaviour {

    [Header("Tower setup")]
    public float range;
    public float level;
    public int cost;

    [Header("Material setup")]
    private ParticleSystem gas;
    private Material material;
    private Material nodeMat;
    private Material roadMat;
    private Renderer[] renderers;
	private Owner owner;


    // Use this for initialization
    void Start () {
        gas = gameObject.GetComponentInChildren<ParticleSystem>();
        gas.Stop();
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


    //Upgrade tower script
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
            level++;
            return;
        }
    }

    //Find mobs in range 
    void UpdateTarget()
    {
        mobs[] allMobs = GameObject.FindObjectsOfType<mobs>();
        bool mobsInRange = false;

        foreach (mobs mob in allMobs)
        {
            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);
            Material mobMat = mob.GetComponentInChildren<Renderer>().material;

			int mobOwner = mob.gameObject.GetComponent<NetMobPath> ().OwnerId;

			if (distanceToMob < range && owner.ownerId != mobOwner)
            //if (distanceToMob < range)
            {
                mobsInRange = true;
                if (level == 1)
                {
                  
                    mob.SpeedUp(0.6f);
                }
                if (level == 2)
                {
               
                    mob.SpeedUp(0.4f);
                }
                if (level == 3)
                {
                 
                    mob.SpeedUp(0.25f);
                }

            }

        }

        if (mobsInRange)
        {
            gas.Play();
        } else { gas.Stop(); }

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
