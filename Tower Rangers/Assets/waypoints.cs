using UnityEngine;

public class waypoints : MonoBehaviour
{
    //to construct mob path

    public static Transform[] points;
    //list of gameobjects in scene

    void Awake()

    {
        //load all children under waypoints into points
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)

        {
            //access specific waypoint position using index of points
            points[i] = transform.GetChild(i);

        }

    }

}

