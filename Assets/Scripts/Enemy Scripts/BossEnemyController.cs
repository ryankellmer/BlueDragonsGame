using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    void OnEnable()
    {
        ResetStuff();
        Invoke("ResetPos", 0.1f);
    }

    protected override void ResetStuff(){
        maxHealth = 100;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        defense = 10;
        attackDamage = 100;
        moneyDrop = 100;
        scoreValue = 100;
        normalSpeed = .75f;
        slowSpeed = 0.5f;
        poisonResistance = 50f;
        burnResistance = 50f;
        slowResistance = 50f;
        freezeResistance = 50f;
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
