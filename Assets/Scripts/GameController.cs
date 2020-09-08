using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Stats
    public float maxHealth = 100;
    float health;
    float score = 0;
    float money = 0;

    //Enemies
    //public EnemyController enemy;
    public float timeBetweenSpawns;
    public float wave = 1;
    float spawnCools = 0f;
    public int enemesPerWave;

    public void DetermineEnemiesPerWave()
    {

    }

    public void SpawnEnemy()
    {

    }

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {


        if (health <= 0) GameOver();
    }

    public void GameOver()
    {

    }

    public void TakeDamage(float amt)
    {
        health -= amt;
    }

    public void AddScore(float amt)
    {
        score += amt;
    }

    public void AddMoney(float amt)
    {
        money += amt;
    }
}
