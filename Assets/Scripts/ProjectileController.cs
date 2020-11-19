using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Stats")]
    protected float projectileSpeed = 1500f;
    protected Transform projectileTarget;
    protected int bombDamage;
    protected float bombRange;
    protected int missleDamage; 
    public enum projectileTypes{standard, slow, freeze, poison, burn}

    Rigidbody2D rb2d;
    protected Vector3 enemyPos;
    protected Vector3 projectilePos;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public virtual void Start(){
        
    }

    public virtual void Update(){
        if(projectileTarget == null){
            gameObject.SetActive(false);
        }
        enemyPos = projectileTarget.position;
        projectilePos = transform.position;
        enemyPos.x = enemyPos.x - projectilePos.x;
        enemyPos.y = enemyPos.y - projectilePos.y;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        rb2d.AddForce(transform.up * projectileSpeed * Time.deltaTime);
    }

    //Sends target to projectile so projectile can lock onto enemy's location
    public void ReceiveTarget(Transform turretTarget, int d)
    {
        projectileTarget = turretTarget;
    }

    public void BombReceiveStats(int damage, float range){
        bombDamage = damage;
        bombRange = range;
    }

    public void MissleReceiveStats(int damage){
        missleDamage = damage; 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            gameObject.SetActive(false);
            HitTarget();
        }
    }

    //Decremet enemy health based on turret damage, destroy enemy when health is 0.
    public virtual void HitTarget(){}
}
