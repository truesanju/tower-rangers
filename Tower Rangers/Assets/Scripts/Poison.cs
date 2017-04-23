using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {
    public float poisonSpeed = 0.001f;
    private Transform target;
    private mobs targetMob;
    private bool destroyed = false;

    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {

            Destroy(gameObject);
            return;
        }
        if (target != null)
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
        float bulletDistance = poisonSpeed * Time.deltaTime;

        if (dir.magnitude < bulletDistance || dir.magnitude < 1.0)
        {

            Hit();
            //Destroy(gameObject);
            return;

        }
        poisonSpeed =poisonSpeed+ 0.01f;

        transform.Translate(dir.normalized * poisonSpeed, Space.World);

    }

    void Hit()
    {

        mobsspawner.mobsalive--;
        targetMob.Damage(PoisonTower.damage);
        poisonSpeed = 0.001f;
        return;
    }






}
