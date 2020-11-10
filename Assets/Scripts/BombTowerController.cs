using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerController : TowerController
{
    public float currentBlastRange; 
    [Header("Bomb Tower Upgrade Stats")]
    public int pooledObjects = 4;
    public int bomberBaseAttack = 1;
    public int bomberMidAttack = 3;
    public int bomberHighAttack = 5;
    public float bomberBaseRange = 2f;
    public float bomberMidRange = 2.5f;
    public float bomberHighRange = 3f;
    public float blastBaseRange = 0.5f;
    public float blastMidRange = 0.75f;
    public float blastHighRange = .1f;

    public AudioClip shotSound;
    AudioSource audioSource; 

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = bomberBaseRange; //Set Box Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = bomberBaseAttack;
        currentRange = bomberBaseRange;
        currentBlastRange = blastBaseRange;
        towerCost = 100; 
        upgradeCost = 50;
        timeBeforeNextShot = 9.0f;
    }


    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public void upgradeBombTower(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            currentAttack = bomberMidAttack;
            currentRange = bomberMidRange;
            currentBlastRange = blastMidRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
        }
        if (level == towerLevel.mid){
            upgradeCost = 100;
            currentAttack = bomberHighAttack;
            currentRange = bomberHighRange;
            currentBlastRange = blastHighRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
        }
        if (level == towerLevel.high){
            return;
        }
        rotationSpeed += 0.5f;
        timeBeforeNextShot -= .5f;
    }


    //Send bomb to closest target 
   public override void Shoot(){
       if(target == null){
            return;
        }
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("Bomb");
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
        Projectile.BombReceiveStats(currentAttack, currentBlastRange);
        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, currentAttack); //Pass target to ProjectileController and damage amount
            audioSource.PlayOneShot(shotSound, 0.5f); 
        }  
   }
    
}
