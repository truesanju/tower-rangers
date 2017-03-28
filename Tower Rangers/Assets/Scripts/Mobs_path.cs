using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mobs_path : MonoBehaviour {

    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    private Mobs mobs;


    void Start()
    {
        mobs = GetComponent<Mobs>();
        //set target as first waypoint
        target = Waypoints.points[0];
    }



    void Update()  //for every frame that is called
    {
        //get direction vector
        Vector3 dir = target.position - transform.position;
        //moving w direction vector ==> tranform.Translate(dir);
        //do not edit speed here
        //speed is not dependent on frame rate
        transform.Translate(dir.normalized * mobs.speed * Time.deltaTime, Space.World);

        //if we have reached waypoint:
        //edit 0.4f if there is accuracy issues upon testing
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)

        {
            GetNextWaypoint();
        }

        mobs.speed = mobs.startSpeed;
    }


    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            //Destroy(gameObject);   
            EndPath();
            return;
        }

        //set target as next waypoint
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }

    //for mobs that finish path but remain unkilled
    void EndPath()
    {
        PlayerStats.Lives--;

        Mobs_spawner.MobsAlive--;

        Destroy(gameObject);

    }

}
