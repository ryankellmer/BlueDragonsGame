//using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;


public class BombController : ProjectileController
{
    public LayerMask enemyMask;

    //Deal Damage to All enemies within bombRange when bomb hits target, create explosion
    public override void HitTarget() {
        Debug.Log(bombDamage);
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("BombExplosion");
        explosion.SetActive(true);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Collider2D[] enemiesInBlast = Physics2D.OverlapCircleAll(transform.position, bombRange, enemyMask);
        for(int i=0; i<enemiesInBlast.Length; i++){
            EnemyController enemy = enemiesInBlast[i].GetComponent<EnemyController>();
            enemy.AttackEnemy(bombDamage, type, 10f);
            gameObject.SetActive(false);
        }
    }
}
