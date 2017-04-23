using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(mobs))]
public class NetMobPath : MonoBehaviour {

	private Transform target;
	private mobs mobss;
	private int waypointIndex = 0;
	public GameObject netwaypoint;
	public int OwnerId;
	public Transform[] points;

	void Start()
	{
		mobss = GetComponent<mobs>();
		OwnerId = NetMobsspawner.GetMobOwner ();
		//set target as first waypoint
		netwaypoint = GameObject.Find ("mobwaypoint"+OwnerId);
		switch (OwnerId) {
		case 1:
			target = Netwaypoint1.points[0];
			break;
		case 2:
			target = Netwaypoint2.points[0];
			break;
		case 3:
			target = Netwaypoint3.points[0];
			break;
		case 4:
			target = Netwaypoint4.points[0];
			break;
		}
		//target = Netwaypoint1.points[0];
	}
	void Update() {
		Vector3 dir = target.position - transform.position;  //get direction vector
		//moving w direction vector ==> tranform.Translate(dir);

		//do not edit speed here, speed is not dependent on frame rate

		transform.Translate(dir.normalized * mobss.speed * Time.deltaTime, Space.World);

		//if we have reached waypoint:
		//edit 0.4f if there is accuracy issues upon testing
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
			GetNextWaypoint();
	}


	void GetNextWaypoint(){
		if (waypointIndex >= Netwaypoint1.points.Length - 1){
		//if (waypointIndex >= Netwaypoint1.points.Length - 1){
		//if (waypointIndex >= points.Length - 1){
			EndPath();
			return;
		}
		waypointIndex++;
		//set next target
		switch (OwnerId) {
		case 1:
			target = Netwaypoint1.points[waypointIndex];
			break;
		case 2:
			target = Netwaypoint2.points[waypointIndex];
			break;
		case 3:
			target = Netwaypoint3.points[waypointIndex];
			break;
		case 4:
			target = Netwaypoint4.points[waypointIndex];
			break;
		}
		//target = points[waypointIndex];
		//target = netwaypoint.GetComponent<waypoints> ().points[waypointIndex];
		//target = Netwaypoint1.points[waypointIndex];
	}

	void EndPath(){
		int PlayerId = GameObject.Find ("LOCAL player").GetComponent<PlayerScript> ().PlayerId;
		Debug.Log ("Mob hits the end for owner " + PlayerId);
		if (OwnerId == PlayerId) {
			PlayerLedger.hp -= mobss.mobseffectonplayerhp;
		}
		mobsspawner.mobsalive--;
		Destroy(gameObject);
	}
}
