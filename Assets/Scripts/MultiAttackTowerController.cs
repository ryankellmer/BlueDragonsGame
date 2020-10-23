using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public Transform currentTarget;

  [Header("multi Shooter Tower Stats")]
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


    public override void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2)); //Set Box Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = multiShooterBaseAttack;
        currentRange = multiShooterBaseRange; 
        numToAttack = baseNum;
        towerCost = 75;
        upgradeCost = 50;
    }

    //Upgrade Tower attack speed, range, rotation speed, attack damage, and number of enemies to attack
    public void upgrademultiShooterTower(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            currentAttack = multiShooterMidAttack;
            currentRange = multiShooterMidRange;
            numToAttack = midNum;
            GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2));
          
        }
        if (level == towerLevel.mid){
            upgradeCost = 100;
            currentAttack = multiShooterHighAttack;
            currentRange = multiShooterHighRange;
            numToAttack = highNum;
            GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2));
        }
        if (level == towerLevel.high){
            return;
        }
        rotationSpeed += 0.5f;
        timeBeforeNextShot += .25f;
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
        Projectile.ReceiveTarget(currentTarget, damage); //Pass target to ProjectileController and damage amount
        numhit += 1;
        if (numhit == numToAttack){
          return;
        }
        }
       }
     }
}
