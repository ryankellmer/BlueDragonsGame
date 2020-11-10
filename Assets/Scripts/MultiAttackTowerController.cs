using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public Transform currentTarget;

    public int pooledObjects = 8;
    public int multiShooterBaseAttack = 1;
    public int multiShooterMidAttack = 2;
    public int multiShooterHighAttack = 3;
    public float multiShooterBaseRange = 1f;
    public float multiShooterMidRange = 1.5f;
    public float multiShooterHighRange = 2f;
    public int numToAttack;
    public int baseNum = 2;
    public int midNum = 3;
    public int highNum = 4;

    public AudioClip shotSound;
    AudioSource audioSource; 


    public override void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = multiShooterBaseRange; //Set Circle Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = multiShooterBaseAttack;
        currentRange = multiShooterBaseRange; 
        numToAttack = baseNum;
        towerCost = 75;
        upgradeCost = 50;
        timeBeforeNextShot = 8.0f;
    }

    //Upgrade Tower attack speed, range, rotation speed, attack damage, and number of enemies to attack
    public void upgrademultiShooterTower(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            currentAttack = multiShooterMidAttack;
            currentRange = multiShooterMidRange;
            numToAttack = midNum;
            GetComponent<CircleCollider2D>().radius = currentRange;
          
        }
        if (level == towerLevel.mid){
            upgradeCost = 100;
            currentAttack = multiShooterHighAttack;
            currentRange = multiShooterHighRange;
            numToAttack = highNum;
            GetComponent<CircleCollider2D>().radius = currentRange;
        }
        if (level == towerLevel.high){
            return;
        }
        rotationSpeed += 0.5f;
        timeBeforeNextShot -= .5f;
    }

    
    public override void Shoot(){
      Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, currentRange); //Array of objects within tower range
      int numhit = 0;

      //For all objects within range, check if enemy, if enemy send projectile to number of enemies up to numToAttack
      for(int i = 0; i<objectsInRange.Length; i++){
      currentTarget = objectsInRange[i].transform;
      GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("Missle");
      ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
      Projectile.MissleReceiveStats(currentAttack);
      if ((Projectile != null) && (objectsInRange[i].gameObject.tag == "Enemy")){
        ProjectileGO.transform.position = transform.position;
        ProjectileGO.transform.rotation = transform.rotation;
        ProjectileGO.SetActive(true);
        if(currentTarget == null){
            continue;
        }
        Projectile.ReceiveTarget(currentTarget, currentAttack); //Pass target to ProjectileController and damage amount
                audioSource.PlayOneShot(shotSound, 0.5f); 
        numhit += 1;
        if (numhit == numToAttack){
          return;
        }
        }
       }
     }


}
