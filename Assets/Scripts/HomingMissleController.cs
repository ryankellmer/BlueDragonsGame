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

    //When missle is pulled from object pool, reset missle speed and time
    void OnEnable(){
        missleSpeed = minSpeed;
        rend = GetComponent<Renderer> ();
        time = 0;
    }

    //Disable projectile after certaina mount of time if projectile is not reached
    public override void Start(){
        Invoke("DisableProjectile", 6f);
    }

    //Obtain target's position and move towards target every frame
    public override void Update() 
    {
        //Allow missle to gain speed as time increases
        missleSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time/accelerationTime);

        //if not target exists when projectile is spawned, disable projectile
        if(projectileTarget == null){
            gameObject.SetActive(false);
            return;
        }

        //If projectile's target gets killed before it reaches target, disable projectile
        if(!projectileTarget.gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }

        //change homing missles color between red and green
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        
        //Rotate Towards Enemy
        enemyPos = projectileTarget.position;
        projectilePos = transform.position;
        enemyPos.x = enemyPos.x - projectilePos.x;
        enemyPos.y = enemyPos.y - projectilePos.y;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        enemyPos = projectileTarget.position;
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), enemyPos, missleSpeed * Time.deltaTime); 

        //Increment time for missle speed acceleration
        time += Time.deltaTime;
    }

    //When Target is hit, disable projectile and decrement enemy health
    public override void HitTarget()
    {
        GameObject explosion = ObjectPool.SharedInstance.GetPooledObject("MissleExplosion");
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
