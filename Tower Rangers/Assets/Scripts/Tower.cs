using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    [Header("Tower setup")]
    private Transform target;
    public float range = 50f;
    public float turnSpeed = 10f;
    private Transform rotationPoint;
    private bool drawRange;

    [Header("Shooting")]
    public float fireRate= 1.0f;
    private float fireCountdown= 0.0f;
    public GameObject bullet;
    public Transform bulletStartPt; 

	// Use this for initialization
	void Start () {
        rotationPoint = GameObject.Find("FireRotationPoint").transform;
        drawRange = false;
        InvokeRepeating("UpdateTarget", 0.2f, 0.2f);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            return;
        }

     

        findNextTarget();
        Shoot();

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
        GameObject[] mobInRange = GameObject.FindGameObjectsWithTag("Mob");
        float closestRange = Mathf.Infinity;
        GameObject closestMob = null;

        foreach (GameObject mob in mobInRange)
        {
            float distanceToMob = Vector3.Distance(mob.transform.position, transform.position);

            if (distanceToMob < closestRange)
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
