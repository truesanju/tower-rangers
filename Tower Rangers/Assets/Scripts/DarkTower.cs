using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTower : MonoBehaviour {

    [Header("Tower setup")]
    public float range;
    public float level;
    public int cost;


    [Header("Material setup")]
    private ParticleSystem dark;
    private Material material;
    private Material nodeMat;
    private Material roadMat;
    private Renderer[] renderers;

	private Owner owner;


    // Use this for initialization
    void Start () {
        dark = gameObject.GetComponentInChildren<ParticleSystem>();
        dark.Stop();
        renderers = transform.GetComponentsInChildren<Renderer>();
        material = transform.GetComponentInChildren<Renderer>().material;
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
            level++;
            return;
        }
    }

    void UpdateTarget()
    {
        mobs[] allMobs = GameObject.FindObjectsOfType<mobs>();
        bool mobsInRange = false;

        foreach (mobs mob in allMobs)
        {
            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);
            Material mobMat = mob.GetComponentInChildren<Renderer>().material;

			int mobOwner = mob.gameObject.GetComponent<NetMobPath> ().OwnerId;
			//Debug.Log ("Mob owner is " + mobOwner + "and the targeting tower is from player" + owner.ownerId);

			if (distanceToMob < range && owner.ownerId != mobOwner)
            //if (distanceToMob < range)
            {
                mobsInRange = true;
                float health = mob.getHealth();
                if (health < mob.initialhealth)
                {
                    if (level == 1)
                    {

                        mob.Heal(Time.maximumDeltaTime * level);
                    }
                    if (level == 2)
                    {

                        mob.Heal(Time.maximumDeltaTime * 2.0f);
                    }
                    if (level == 3)
                    {

                        mob.Heal(Time.maximumDeltaTime * 3.0f);
                    }

                }

            }

        }

        if (mobsInRange)
        {
            dark.Play();
        }
        else { dark.Stop(); }

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
