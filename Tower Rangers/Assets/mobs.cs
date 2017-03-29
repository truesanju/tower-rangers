using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class mobs: MonoBehaviour{

    public float speed = 10f;
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        //set target as first waypoint
        target = waypoints.points[0];
    }

    void Update()  //for every frame that is called
    {
        //get direction vector
        Vector3 dir = target.position - transform.position;
        //moving w direction vector ==> tranform.Translate(dir);
        //do not edit speed here
        //speed is not dependent on frame rate
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //if we have reached waypoint:
        //edit 0.4f if there is accuracy issues upon testing

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        //mobs.speed = mobs.startSpeed;

    }

    void GetNextWaypoint()

    {
        if (waypointIndex >= waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = waypoints.points[waypointIndex];

    }

}

