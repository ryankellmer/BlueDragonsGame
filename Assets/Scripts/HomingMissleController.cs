using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissleController : ProjectileController
{
    public float missleSpeed;
    private float minSpeed = .5f;
    private float maxSpeed = 9.0f;
    private float accelerationTime = 7;
    private float time;
    Color colorStart = Color.red;
    Color colorEnd = Color.green;
    float duration = 1.0f;
    Renderer rend;

    void OnEnable(){
        missleSpeed = .5f;
        rend = GetComponent<Renderer> ();
        time = 0;
    }

    public override void Update() //Obtain target's position and move towards target 
    {
        missleSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time/accelerationTime);

        if(!projectileTarget.gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }
        //change homing missles color between red and green
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);

        if(projectileTarget == null){
            gameObject.SetActive(false);
            return;
        }
        
        //Rotate Towards Enemy
        enemyPos = projectileTarget.position;
        projectilePos = transform.position;
        enemyPos.x = enemyPos.x - projectilePos.x;
        enemyPos.y = enemyPos.y - projectilePos.y;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));


        enemyPos = projectileTarget.position;
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), enemyPos, missleSpeed * Time.deltaTime); 

        time += Time.deltaTime;
    }

    //When Target is hit, disable projectile and decrement enemy health
    public override void HitTarget()
    {
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("MissleExplosion");
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
