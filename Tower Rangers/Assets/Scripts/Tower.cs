using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    [Header("Tower setup")]
    private Transform target;
    public float range = 5f;
    public static float damage = 1.0f;
    public int level = 1;
    public int cost = 100;
    
    private Transform rotationPoint;
    

    [Header("Shooting")]
    public float turnSpeed = 10f;
    public float fireRate= 1.0f;
    private float fireCountdown= 0.0f;
    public GameObject bullet;
    public Transform bulletStartPt; 

    [Header("Material setup")]
    private Material material;
    private Material nodeMat;
    private Material roadMat;
    private Renderer[] renderers;
	private Owner owner;

	//private int PlayerId = 0;
    // Use this for initialization
    void Start()
    {
		//PlayerId = GameObject.Find ("LOCAL player").GetComponent<PlayerScript> ().PlayerId;

        rotationPoint = transform.Find("FireRotationPoint").transform;
        InvokeRepeating("UpdateTarget", 0.2f, 0.2f);

        renderers = transform.GetComponentsInChildren<Renderer>();
        material = transform.GetComponentInChildren<Renderer>().material;
        nodeMat = GameObject.FindGameObjectWithTag("Node").GetComponent<Renderer>().material;
		roadMat = GameObject.FindGameObjectWithTag ("Road").GetComponent<Renderer> ().material;
		owner = gameObject.GetComponent<Owner> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }


        Shoot();
        findNextTarget();


    }

    public void upgradeTower()
    {
        if (level == 3)
        {
            Debug.Log("Tower is a max level");
        } else
        {
            //Scale model
            
            Vector3 originalScale = rotationPoint.localScale;
            rotationPoint.localScale += new Vector3(0, 0.25f);
            
            
            foreach (Renderer r in renderers)
            {
                Material temp = r.material;
                temp.SetColor("_EmissionColor", temp.color * 0.20f * level);
                r.material = temp;
            }
            
           
            fireRate += 0.5f;
            damage += 2f;
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
        colorNodesInRange("Road",material);
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

    //Draws range of tower
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1f, 0.25f);
        Gizmos.DrawSphere(transform.position, range);
       
    }

    //Updates target to point to
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
//			if (distanceToMob < closestRange)
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

    //Points tower in direction of target
    void findNextTarget()
    {

		if (target == null)
			return;
        Vector3 dir = target.position - rotationPoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    //Shoots target
    void Shoot()
    {

        if (fireCountdown <= 0f)
        {
            //Bullet code here
            makeBullet();
            fireCountdown = 1 / fireRate;
    
        }

        fireCountdown-= Time.deltaTime;
    }


    void makeBullet()
    {
        GameObject bulletObject = (GameObject) Instantiate(bullet, bulletStartPt.position, bulletStartPt.rotation);
        Bullet b = bulletObject.GetComponent<Bullet>();

      
        b.setTarget(target);
        
    }

}
