using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class mobs : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float speed;

    public float initialhealth;// initial health of mobs
    private float health;  //current health of mobs
//    private bool died = false;
    public int mobsvalue;
    public int mobseffectonplayerhp;
    public Image healthbar;
    

    void Start() {
        speed = initialSpeed;
        health = initialhealth;
		mobseffectonplayerhp = 5;
    }

    void Update()
    {
        speed = initialSpeed;
    }

    public float getHealth()
    {
        return this.health;
    }

    public void Slow(float _slow)
    {
        speed = speed*_slow;
    }

    public void SpeedUp(float _speed)
    {
        speed = speed/ _speed;
    }

    public void Heal(float amount)
    {
        health = health + amount;
    }


    public void Damage(float amount) {
        health = health - amount;

        if (health <= 0)
        {
            mobdie();
        }

        
        healthbar.fillAmount = health / initialhealth;
        Debug.Log("Health: " + health);
        return;
        
     }

    void mobdie() {
        //died = true;
		if (gameObject.GetComponent<NetMobPath>().OwnerId == GameManager.player.PlayerId)
			PlayerLedger.gold += mobsvalue;

        //input death effect here

        mobsspawner.mobsalive--;

        Destroy(gameObject);
        return;


        }

}