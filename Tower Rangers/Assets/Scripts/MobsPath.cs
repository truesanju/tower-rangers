using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(mobs))]
public class MobsPath : MonoBehaviour {

    private Transform target;
    private mobs mobss;
    private int waypointIndex = 0;

    void Start()
    {
        mobss = GetComponent<mobs>();
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

        transform.Translate(dir.normalized * mobss.speed * Time.deltaTime, Space.World);

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation =  lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);


        //if we have reached waypoint:
        //edit 0.4f if there is accuracy issues upon testing
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)

        {

            GetNextWaypoint();

        }


    }


    void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        SinglePlayer.hp -= mobss.mobseffectonplayerhp;
        mobsspawner.mobsalive--;
        Destroy(gameObject);
    }

}
