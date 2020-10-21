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
    float money = 250;

    //Enemies
    //public EnemyController enemy;
    public float timeBetweenSpawns;
    public float wave = 1;
    float spawnCools = 0f;
    public int enemesPerWave;

    //UI
    public Text score_text;
    public Text money_text;
    public Slider healthBar;
    public GameObject GameOverUI;

    public GameObject towerUI;

    tower_spawning towerPos;

    public TowerController tower1, tower2, tower3;

    public bool isPickingTower = false;

    public void DetermineEnemiesPerWave()
    {

    }

    public void SpawnEnemy()
    {

    }

    private void Awake()
    {
        towerUI = GameObject.FindGameObjectWithTag("TowerUI");
        towerUI.SetActive(false);

        health = maxHealth;
        healthBar.maxValue = (int)maxHealth;
        healthBar.value = (int)maxHealth;

        score_text.text = "Score: " + score.ToString();
        money_text.text = "$" + money.ToString();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                AddMoney(50);
            }
        }

        if (health <= 0) GameOver();
    }

    public void UpdatePos(tower_spawning tow)
    {
        towerPos = tow;
        isPickingTower = true;
    }

    public void SpawnTower_1()  //regular tower
    {
        if (towerPos.already == false &&money >  tower1.towerCost)
        {
            RemoveMoney(tower1.towerCost); 
            Instantiate(tower1, towerPos.transform.position, Quaternion.identity);
            towerPos.already = true;
            isPickingTower = false;
        }
        towerPos.turnOffUI();
    }

    public void SpawnTower_2() // Bombtower
    {
        if (towerPos.already == false && money > tower2.towerCost)
        {
            RemoveMoney(tower2.towerCost);
            Instantiate(tower2, towerPos.transform.position, Quaternion.identity);
            towerPos.already = true;
            isPickingTower = false;
        }
        towerPos.turnOffUI();
    }
    public void SpawnTower_3() //Multiattack tower
    {
        if (towerPos.already == false && money > tower3.towerCost)
        {
            RemoveMoney(tower3.towerCost); 
            Instantiate(tower3, towerPos.transform.position, Quaternion.identity);
            towerPos.already = true;
            isPickingTower = false;
        }
        towerPos.turnOffUI();
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void TakeDamage(float amt)
    {
        health = Mathf.Clamp(health - amt, 0, maxHealth);
        healthBar.value = (int)health;
    }

    public void AddScore(float amt)
    {
        score = Mathf.Max(score + amt, 0);
        score_text.text = "Score: " + score.ToString();
    }

    public void AddMoney(float amt)
    {
        money += amt;
        money_text.text = "$" + money.ToString();
    }

    public void RemoveMoney(float amt)
    {
        money -= amt;
        money_text.text = "$" + money.ToString(); 
    }
}
