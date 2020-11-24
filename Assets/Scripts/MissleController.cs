using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : ProjectileController
{

    
    //When target is hit, create explosion, damage enemy
    public override void HitTarget()
    {
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("BulletExplosion");
        explosion.SetActive(true);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        EnemyController enemy = projectileTarget.GetComponent<EnemyController>();
        gameObject.SetActive(false);
        if(enemy == null){
            return;
        }
        enemy.AttackEnemy(missleDamage, type, 10f);
    }
}
