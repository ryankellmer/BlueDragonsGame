using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float projectileSpeed = 1500f;
    protected Transform projectileTarget;
    public int bombDamage;
    public float bombRange;
    public int missleDamage; 
    public enum projectileTypes{standard, slow, freeze, poison, burn}

    Rigidbody2D rb2d;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void OnEnable(){
        transform.LookAt(projectileTarget);
        rb2d.AddForce(transform.up * projectileSpeed);
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
