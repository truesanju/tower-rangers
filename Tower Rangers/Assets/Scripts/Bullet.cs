using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 0.2f;
    private Transform target;

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    void moveToTarget()
    {
        Vector3 dir = target.position - transform.position;
        float bulletDistance = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <1)
        {
            Debug.LogError("works");
            Hit();
            return;
        }

        transform.Translate(dir.normalized*bulletSpeed,Space.World);

    }

    void Hit()
    {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (target == null)
        {
            Destroy(gameObject);
        }
        moveToTarget();
	
	}
}
