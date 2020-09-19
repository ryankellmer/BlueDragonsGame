using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
	public GameObject testEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(testEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
