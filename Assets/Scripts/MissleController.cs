using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : ProjectileController
{

    

    public override void HitTarget()
    {

        EnemyControllerV2 enemy = projectileTarget.GetComponent<EnemyControllerV2>();
        gameObject.SetActive(false);
        if(enemy == null){
            return;
        }
        enemy.ChangeHealth(missleDamage);
    }
}
