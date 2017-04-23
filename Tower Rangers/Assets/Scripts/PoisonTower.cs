using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : MonoBehaviour {
    [Header("Tower setup")]
    private Transform target;
    public float range = 8f;
    public static float damage = 0.5f;
    public int level = 1;
    public int cost = 100;

    [Header("Shooting")]
    public float fireRate = 1.0f;
    private float fireCountdown = 0.0f;
    public GameObject poison;
    public Transform poisonStartPt;

    [Header("Material setup")]
    private Material material;
    private Material nodeMat;
    private Material roadMat;
    private Renderer[] renderers;
	private Owner owner;

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0.2f, 0.2f);
        material = transform.GetComponentInChildren<Renderer>().material;
        roadMat = GameObject.FindGameObjectWithTag("Road").GetComponent<Renderer>().material;
        renderers = transform.GetComponentsInChildren<Renderer>();
		owner = gameObject.GetComponent<Owner> ();
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            Shoot();
        }
        
       

    }

    void UpdateTarget()
    {
        GameObject[] mobInRange = GameObject.FindGameObjectsWithTag("MobGO");
        float closestRange = Mathf.Infinity;
        GameObject closestMob = null;
        
        foreach (GameObject mob in mobInRange)
        {
            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);

			int mobOwner = mob.transform.parent.gameObject.GetComponent<NetMobPath> ().OwnerId;
			//Debug.Log ("Mob owner is " + mobOwner + "and the targeting tower is from player" + owner.ownerId);

			if (distanceToMob < closestRange && owner.ownerId == mobOwner)
            //if (distanceToMob < closestRange)
            {
                closestMob = mob;
                closestRange = distanceToMob;
            }
        }

        if (closestMob != null && closestRange < range)
        {
            target = closestMob.transform;
            
        }
        else
        {
            target = null;
        }

    }

    void Shoot()
    {

        if (fireCountdown <= 0f)
        {
            //Bullet code here
            makePoison();
            fireCountdown = 1 / fireRate;

        }

        fireCountdown -= Time.deltaTime;
    }


    void makePoison()
    {
        GameObject poisonObject = (GameObject)Instantiate(poison, poisonStartPt.position, poisonStartPt.rotation);
        Poison p = poisonObject.GetComponent<Poison>();

        p.setTarget(target);

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

            
            transform.localScale += new Vector3(0, 15.0f);


            foreach (Renderer r in renderers)
            {
                Material temp = r.material;
                temp.SetColor("_EmissionColor", temp.color * 0.20f * level);
                r.material = temp;
            }


            fireRate += 1.0f;
            damage += 0.5f*level;
            range += 8f;
            level++;
            return;
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


