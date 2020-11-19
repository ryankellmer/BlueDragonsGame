using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : ProjectileController
{

    

    public override void HitTarget()
    {
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("BulletExplosion");
        explosion.SetActive(true);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        EnemyControllerV2 enemy = projectileTarget.GetComponent<EnemyControllerV2>();
        gameObject.SetActive(false);
        if(enemy == null){
            return;
        }
        enemy.ChangeHealth(missleDamage);
    }
}
