using System.Collections;



using System.Collections.Generic;



using UnityEngine;



using UnityEngine.UI;





public class mobsspawner : MonoBehaviour

{



    public Transform mobsprefab;

    //attach startnode to spawnPoint

    public Transform spawnPoint;



    //to edit in inspector

    public float timeBetweenWaves = 5f;



    //time before first wave is spawned

    private float countdown = 2f;



    private int waveIndex = 0;





    void Update()

    {

        if (countdown <= 0f)

        {

            SpawnWave();

            //!StartCoroutine(SpawnWave());

            countdown = timeBetweenWaves;

        }

        countdown -= Time.deltaTime;

    }



    //pause SpawnWave, wait for x seconds before continuing

    //!IEnumerator SpawnWave()

    void SpawnWave()

    {

        //Debug.Log("Wave is here");



        //!waveIndex++;

        for (int i = 0; i < waveIndex; i++)

        {

            SpawnMob();

            //wait 0.5 seconds before spawning next enemy

            //yield return new WaitForSeconds(0.5f);

        }



        waveIndex++;



    }



    void SpawnMob()

    {

        Instantiate(mobsprefab, spawnPoint.position, spawnPoint.rotation);

    }

}