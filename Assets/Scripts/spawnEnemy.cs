using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
	public GameObject[] waypoints;
	public GameObject testEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(testEnemy).GetComponent<ronan_EnemyController>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
