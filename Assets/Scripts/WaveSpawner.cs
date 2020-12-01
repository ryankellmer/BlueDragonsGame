using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public float initialWaitTime;
    public float timeBetweenWaves;
    public int numOfEnemiesPerWave;
    public float timeBetweenEnimies;
    public int numOfWaves;
    public GameObject enemy;

    float timer = 0;
    WaveSpawnerStates state = WaveSpawnerStates.Starting;
    int enemiesSpawned = 0;
    int wavesSpawned = 0;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch(state){
            case WaveSpawnerStates.Starting:
                if(timer > initialWaitTime){
                    state = WaveSpawnerStates.Spawning;
                    timer = 0;
                }
                break;
            case WaveSpawnerStates.Waiting:
                if(timer >= timeBetweenWaves){
                    state = WaveSpawnerStates.Spawning;
                    timer = 0;
                }
                break;
            case WaveSpawnerStates.Spawning:
                if(timer > timeBetweenEnimies){
                    //Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);
                    GameObject EnemyGO = ObjectPool.SharedInstance.GetPooledObject("Enemy");
                    EnemyControllerV2 Enemy = EnemyGO.GetComponent<EnemyControllerV2>();
                    EnemyGO.SetActive(true);
                    enemiesSpawned++;
                    timer = 0;
                    if(enemiesSpawned >= numOfEnemiesPerWave){
                        wavesSpawned++;
                        enemiesSpawned = 0;
                        if(wavesSpawned >= numOfWaves){
                            state = WaveSpawnerStates.Finished;
                        }
                        else{
                            state = WaveSpawnerStates.Waiting;
                            timer = 0;
                        }
                    }
                }
                break;
            default: // this will do nothing and is for the finished state as well as a catch all
                break;
        }

    }
}

public enum WaveSpawnerStates {
    Spawning,
    Waiting,
    Starting,
    Finished
}
