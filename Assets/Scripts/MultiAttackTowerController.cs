using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public int numToAttack = 2; 
   public int pooledObjects = 4;
   public Transform currentTarget;


    
    public override void Shoot(){
      Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, range); //Array of objects within tower range
      int numhit = 0;

      //For all objects within range, check if enemy, if enemy send projectile to enemy up to numToAttack
      for(int i = 0; i<objectsInRange.Length; i++){
      currentTarget = objectsInRange[i].transform;
      GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject(pooledObjects);
      ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
      if ((Projectile != null) && (objectsInRange[i].gameObject.tag == "Enemy")){
        ProjectileGO.transform.position = transform.position;
        ProjectileGO.transform.rotation = transform.rotation;
        ProjectileGO.SetActive(true);
        Projectile.ReceiveTarget(currentTarget, damage); //Pass target to ProjectileController and damage amount
          numhit += 1;
          if (numhit == numToAttack){
            return;
          }
        }
       }
     }
}
