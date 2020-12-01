using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonResEnemyController : EnemyController
{
    void OnEnable()
    {
        ResetStuff();
        Invoke("ResetPos", 0.1f);
    }

    protected override void ResetStuff(){
        maxHealth = 10;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        defense = 1;
        attackDamage = 10;
        moneyDrop = 10;
        scoreValue = 10;
        normalSpeed = 1.5f;
        slowSpeed = 0.5f;
        poisonResistance = 100f;
        burnResistance = 0f;
        slowResistance = 0f;
        freezeResistance = 0f;
        curBurn = 0f;
        curFreeze = 0f;
        curPoison = 0f;
        curSlow = 0f;
        poisonCools = 0f;
        poisonDmgCools = 0f;
        burnCools = 0f;
        burnDmgCools = 0f;
        slowCools = 0f;
        freezeCools = 0f;
        //currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
}
