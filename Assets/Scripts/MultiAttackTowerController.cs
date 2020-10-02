using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public int numtoAttack = 2; 
   public int pooledObjects = 4;
   public Transform currentTarget;


    
    public override void Shoot(){
      Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range); 
      int numhit = 0;

      for(int i = 0; i<hitColliders.Length; i++){
      currentTarget = hitColliders[i].transform;
      GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject(pooledObjects);
      ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
      if ((Projectile != null) && (hitColliders[i].gameObject.tag == "Enemy")){
        ProjectileGO.transform.position = transform.position;
        ProjectileGO.transform.rotation = transform.rotation;
        ProjectileGO.SetActive(true);
        Projectile.ReceiveTarget(currentTarget, damage); //Pass target to ProjectileController and damage amount
          numhit += 1;
          if (numhit == numtoAttack){
            return;
          }
        }
       }
     }
}
