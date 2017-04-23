using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 1f;
    private Transform target;
    private mobs targetMob;
    private Renderer renderer;
    private TrailRenderer tRenderer;
    private bool destroyed = false;

    // Use this for initialization
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        tRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (target == null)
        {
            
            Destroy(gameObject);
            return;
        }
        if(target!= null)
        {
            moveToTarget();
        }


            
    
        
    }

    public void setTarget(Transform _target)
    {
        target = _target.transform;
        targetMob = _target.gameObject.GetComponentInParent<mobs>();
        
    }

    void moveToTarget()
    {
       
        Vector3 dir = target.position - transform.position;
        float bulletDistance = bulletSpeed * Time.deltaTime;

        if (dir.magnitude < bulletDistance || dir.magnitude<2.0)
        {
            
            Hit();
            Destroy(gameObject);
            return;
            
        }

        transform.Translate(dir.normalized*bulletSpeed,Space.World);

    }

    void Hit()
    {
        
        mobsspawner.mobsalive--;
        targetMob.Damage(Tower.damage);
        return;
            }

   
    
   

	
}
