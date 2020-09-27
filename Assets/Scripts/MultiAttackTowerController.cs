using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public int numtoAttack = 2; 
   public int pooledObjects = 4;
   public Transform currentTarget;

  private void OnTriggerEnter2D(Collider2D other){
        if((other.tag == "Enemy") && !allTargets.Contains(other.gameObject)){
            allTargets.Add(other.gameObject);
            UnityEngine.Debug.Log("Enemy Added to List");
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        allTargets.Remove(other.gameObject);
    }
  
    
     public override void Shoot(){

       for(int i = 0; i < numtoAttack; i++) {
        currentTarget = allTargets[i].transform;
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject(pooledObjects);
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();

        if (Projectile != null){
        
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(currentTarget, damage); //Pass target to ProjectileController and damage amount
        }
        if(allTargets.Count == 1) {
          break;
        }
       }
     } 
   
     

  
}
