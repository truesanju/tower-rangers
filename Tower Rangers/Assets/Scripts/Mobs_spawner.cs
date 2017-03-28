using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobs_spawner : MonoBehaviour {

    public static int MobsAlive = 0;

    public Wave[] waves;

    //public Transform enemyPrefab;
    public Transform spawnPoint;

    //to edit
    public float timeBetweenWaves = 5f;

    //time before first wave is spawned
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;


    void Update()

    {

        if (MobsAlive > 0)
        {
            return;
        }


        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;  //disable script
        }


        //spawn wave when countdown reaches 0
        if (countdown <= 0f)
        {
            //call IEnumerator
            StartCoroutine(SpawnWave());

            //reset countdown
            countdown = timeBetweenWaves;

            return;
        }


        //reduce countdown by 1 every second 
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);

    }


    //IEnumberator pauses SpawnWave so that we wait x sec before continuing
    IEnumerator SpawnWave()

    {

        PlayerStats.Rounds++;

        //first wave, waveIndex=0, first element is stored in wave
        Wave wave = waves[waveIndex];

        MobsAlive = wave.count;

        for (int i = 0; i < wave.count; i++)

        {   
            // //spawn enemy, wait 0.5 seconds, repeat
            //SpawnMob();
            //yield return new WaitForSeconds(0.5f);

            SpawnMob(wave.mobs);

            yield return new WaitForSeconds(1f / wave.rate);

        }
        waveIndex++;

    }



    void SpawnMob(GameObject mobs)
    {
        Instantiate(mobs, spawnPoint.position, spawnPoint.rotation);
    }



}
