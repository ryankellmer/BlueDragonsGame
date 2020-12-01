using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public float initialWaitTime;
    public float timeBetweenWaves;
    public float timeBetweenSubWaves;
    public int numOfEnemiesPerWave;
    public float timeBetweenEnimies;
    public int numOfSubWaves;
    public int numOfWaves;
    public TextMeshProUGUI waveText;

    float timer = 0;
    WaveSpawnerStates state = WaveSpawnerStates.Starting;
    int enemiesSpawned = 0;
    int wavesSpawned = 0;
    int subWavesSpawned = 0;
    public static string[] enemyTypes = {"Enemy", "SlowResistEnemy", "BurnResistEnemy", "PoisonResistEnemy", "FreezeResistEnemy", "BossEnemy"};
    string currentSpawnType;
    System.Random rand = new System.Random();

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        timer += Time.deltaTime;
        switch(state){
            case WaveSpawnerStates.Starting:
                if(timer > initialWaitTime){
                    currentSpawnType = WaveSpawner.enemyTypes[rand.Next(enemyTypes.Length)];
                    //Debug.Log(currentSpawnType);
                    currentSpawnType = "SlowResistEnemy";
                    //Debug.Log(rand.Next(enemyTypes.Length));
                    state = WaveSpawnerStates.Spawning;
                    timer = 0;
                }
                break;
            case WaveSpawnerStates.Waiting:
                if(timer >= timeBetweenSubWaves){
                    currentSpawnType = enemyTypes[rand.Next(enemyTypes.Length)];
                    state = WaveSpawnerStates.Spawning;
                    timer = 0;
                }
                break;
            case WaveSpawnerStates.Spawning:
                if(timer > timeBetweenEnimies){
                    Debug.Log(currentSpawnType);
                    GameObject EnemyGO = ObjectPool.SharedInstance.GetPooledObject(currentSpawnType);
                    EnemyController Enemy = EnemyGO.GetComponent<EnemyController>();
                    EnemyGO.SetActive(true);
                    enemiesSpawned++;
                    timer = 0;
                    if(enemiesSpawned >= numOfEnemiesPerWave){
                        subWavesSpawned++;
                        enemiesSpawned = 0;
                        if(subWavesSpawned >= numOfSubWaves){
                            state = WaveSpawnerStates.Finished;
                            wavesSpawned++;
                            waveText.text = "Wave: " + (wavesSpawned + 1);
                        }
                        else{
                            state = WaveSpawnerStates.Waiting;
                            timer = 0;
                        }
                    }
                }
                break;
            case WaveSpawnerStates.Finished:
                if(wavesSpawned >= numOfWaves){
                    //level beat
                }
                if(timer > timeBetweenWaves){
                    state = WaveSpawnerStates.Spawning;
                    enemiesSpawned = 0;
                    subWavesSpawned = 0;
                    currentSpawnType = enemyTypes[rand.Next(enemyTypes.Length)];
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
