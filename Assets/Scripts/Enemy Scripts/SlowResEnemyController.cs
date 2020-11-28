using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowResEnemyController : EnemyController
{
    void OnEnable()
    {
        maxHealth = 15;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        defense = 1;
        attackDamage = 10;
        moneyDrop = 10;
        scoreValue = 10;
        normalSpeed = 1.5f;
        slowSpeed = 0.5f;
        poisonResistance = 0f;
        burnResistance = 0f;
        slowResistance = 100f;
        freezeResistance = 0f;

        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;

        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();

        transform.position = waypoints[waypointIndex];
    }
}
