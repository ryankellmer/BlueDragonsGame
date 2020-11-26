//using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;


public class BombController : ProjectileController
{

    //Deal Damage to All enemies within bombRange when bomb hits target, create explosion
    public override void HitTarget() {
        Debug.Log(bombDamage);
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("BombExplosion");
        explosion.SetActive(true);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Collider2D[] enemiesInBlast = Physics2D.OverlapCircleAll(transform.position, bombRange);
        for(int i=0; i<enemiesInBlast.Length; i++){
            if(enemiesInBlast[i].gameObject.tag == "Enemy"){
                EnemyController enemy = enemiesInBlast[i].GetComponent<EnemyController>();
                gameObject.SetActive(false);
                if(enemy == null){
                    continue;
                }
                enemy.AttackEnemy(bombDamage, type, 10f);
            }
        }
    }
    
}
