using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : MonoBehaviour {

    [Header("Tower setup")]
    private Transform target;
    private mobs targetMob;
    public float level =1;
   
    public float range ;
    public float turnSpeed = 10f;
    public LineRenderer lr;
    public Transform lightningStartPt;
    private Transform rotationPoint;
    private bool striking;

    [Header("Shooting")]
    public float damage=1f;

    [Header("Material setup")]
    public Material material;
    private Material roadMat;
    private Renderer[] renderers;

    [Header("Lightning")]
    public int vertexCount = 3;
    private Vector3[] vertices;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Use this for initialization
	private Owner owner;
  

    void Start()
    {
        rotationPoint = transform.Find("LightningRotationPoint").transform;
        lr.numPositions= vertexCount;
        renderers = transform.GetComponentsInChildren<Renderer>();
        roadMat = GameObject.FindGameObjectWithTag("Road").GetComponent<Renderer>().material;
		owner = gameObject.GetComponent<Owner> ();

        InvokeRepeating("UpdateTarget", 0.2f, 0.2f);
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null)
        {
            lr.enabled = false;
            return;
        }

        Lightning();
        UpdateTarget();
       findNextTarget();


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

            damage++;
            range += 6f;
            level++;
            return;
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
			try{
	            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);
				int mobOwner = mob.transform.parent.gameObject.GetComponent<NetMobPath> ().OwnerId;
	//			Debug.Log ("Mob owner is " + mobOwner + "and the targeting tower is from player" + owner.ownerId);
				
				if (distanceToMob < closestRange && owner.ownerId == mobOwner)
	            //if (distanceToMob < closestRange)
	            {
	                closestMob = mob;
	                closestRange = distanceToMob;
	            }
				
			}catch{
				//Debug.Log ("Exception in lightning tower updateTarget");
			}
        }

        if (closestMob != null && closestRange < range)
        {
            target = closestMob.transform;
            targetMob = target.gameObject.GetComponentInParent<mobs>();

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

   
 

    void Lightning()
    {
        if (!lr.enabled)
        {
            lr.enabled = true;
        }
        startPosition = lightningStartPt.position;
        endPosition = target.position;

        lr.SetPosition(0, startPosition);
        lr.SetPosition(vertexCount - 1, endPosition);

        for (int i = 1; i < vertexCount - 1; i++)
        {
            Vector3 nextPosition = Vector3.Lerp(startPosition, endPosition, i / (float)vertexCount);
            nextPosition.x += Random.Range(-1.0f, 1.0f);
            nextPosition.z += Random.Range(-1.0f, 1.0f);
            lr.SetPosition(i, nextPosition);
        }

        targetMob.Damage(damage*Time.deltaTime*level);
    }

	/*
	//Remove this
    private void OnMouseDown()
    {
        upgradeTower();
    }

	//Change this
    private void OnMouseOver()
    {
        colorNodesInRange("Road", material);
    }

	//Change this
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
