using UnityEngine;
public class Netwaypoint2 : MonoBehaviour{
	//to construct mob path

	public static Transform[] points;

	//list of gameobjects in scene

	public void Start(){
		Debug.Log ("creating way point");
		//load all children under waypoints into points
		points = new Transform[transform.childCount];

		for (int i = 0; i < points.Length; i++){
			//access specific waypoint position using index of points
			points[i] = transform.GetChild(i);

		}
	}
}

