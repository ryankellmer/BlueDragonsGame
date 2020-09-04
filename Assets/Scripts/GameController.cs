using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float maxHealth = 100;
    float health;
    public float score = 0;
    public float money = 0;

    public GameObject toSpawn;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            Instantiate(toSpawn, pos, Quaternion.identity);
        }
    }
}
