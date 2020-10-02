using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShooterTowerController : TowerController
{
    public int pooledObjects = 4;
    public override void Shoot(){
       GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject(pooledObjects);
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();

        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, damage); //Pass target to ProjectileController and damage amount
        }  
   }
    
}
