using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
	//public GameObject testEnemy;
    public int pooledObjects = 50;
    public float timer;
    public float spawnRate = 5f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        //Instantiate(testEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate)
        {
            //Instantiate(testEnemy);
            timer = 0f;
            GameObject EnemyGO = ObjectPool.SharedInstance.GetPooledObject("Enemy");
            EnemyControllerV2 Enemy = EnemyGO.GetComponent<EnemyControllerV2>();
            EnemyGO.SetActive(true);
        }
    }
}
