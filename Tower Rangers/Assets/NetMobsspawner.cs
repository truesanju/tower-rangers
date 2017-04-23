using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetMobsspawner : NetworkBehaviour {
    //mobs: basic, quick, tank

    public Wave[] waveslist;

    public static int mobsalive = 0;

	public static int mobOwner = 0;

    //attach startnode to spawnPoint
    public Transform spawnPoint;

    //to edit in inspector
    public float timeBetweenWaves = 5f;

    //time before first wave is spawned
    private float countdown = 2f;

    private int waveIndex = 0;
	public static PlayerScript player;

    void Update(){
        //Server will control spawn for all
		if (!isServer) return;

        if (countdown <= 0f){
            //SpawnWave();

			//temp comment to try out server-side wave spawning
			//StartCoroutine(SpawnWave());
			if (player != null){
				player.RpcSpawnMob ();
				//player
			}
			//RpcSpawnWave();

            //!StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
    }


	public void SpawnNextWave(){
		StartCoroutine(SpawnWave());
	}
    

	//pause SpawnWave, wait for x seconds before continuing
	//void SpawnWave()
	IEnumerator SpawnWave(){   //first wave = index 0 etc
        Wave wave = waveslist[waveIndex];
        mobsalive = wave.mobscount;

        for (int i = 0; i < wave.mobscount; i++){
            SpawnMob(wave.mobsprefab);
            
            //waveIndex++;
            //wait 0.5 seconds before spawning next enemy
            yield return new WaitForSeconds(1f / wave.mobsrate);
        }

        waveIndex++;

        if (waveIndex == waveslist.Length){
            Debug.Log("all waves used");
            //disable script
            this.enabled = false;
         }
    }


    void SpawnMob(GameObject mobsprefab){
		for (int i = 1; i < 5; i++) {
			GameObject newMob = Instantiate (mobsprefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		}
        //mobsalive++;
    }
	public static int GetMobOwner(){
		mobOwner = (mobOwner + 1) % 4;
		return mobOwner + 1;
	}
}