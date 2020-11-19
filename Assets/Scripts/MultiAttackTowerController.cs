using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public Transform currentTarget;

    public int multiShooterBaseAttack = 5;
    public int multiShooterMidAttack = 7;
    public int multiShooterHighAttack = 9;
    public float multiShooterBaseRange = 5f;
    public float multiShooterMidRange = 6.5f;
    public float multiShooterHighRange = 8.0f;
  

    public AudioClip shotSound;
    AudioSource audioSource; 


    public override void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = multiShooterBaseRange; //Set Circle Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = multiShooterBaseAttack;
        currentRange = multiShooterBaseRange; 
        towerCost = 75;
        upgradeCost = 50;
        timeBeforeNextShot = 20.0f;
    }

    //Upgrade Tower attack speed, range, rotation speed, attack damage, and number of enemies to attack
    public void upgrademultiShooterTower(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            currentAttack = multiShooterMidAttack;
            currentRange = multiShooterMidRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
          
        }
        if (level == towerLevel.mid){
            upgradeCost = 100;
            currentAttack = multiShooterHighAttack;
            currentRange = multiShooterHighRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
        }
        if (level == towerLevel.high){
            return;
        }
        rotationSpeed += 0.5f;
        timeBeforeNextShot -= .5f;
    }

    
    public override void Shoot(){
      if(target == null){
            return;
        }
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("HomingMissle"); //Grab a homing-missle from object pool
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>(); 
        Projectile.MissleReceiveStats(currentAttack);
        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, currentAttack); //Pass target to ProjectileController and damage amount
            audioSource.PlayOneShot(shotSound, 0.5F); 
        }  
     }


}
