using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;


public class BombController : ProjectileController
{
 
    //Deal Damage to All enemies within bombRange when bomb hits target
    public override void HitTarget() {
        Collider2D[] enemiesInBlast = Physics2D.OverlapCircleAll(transform.position, bombRange);
        for(int i=0; i<enemiesInBlast.Length; i++){
            if(enemiesInBlast[i].gameObject.tag == "Enemy"){
                EnemyControllerV2 enemy = enemiesInBlast[i].GetComponent<EnemyControllerV2>();
                gameObject.SetActive(false);
                if(enemy == null){
                    continue;
                }
                enemy.ChangeHealth(bombDamage);
                
            }
        }
    }
    
}
