using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float projectileSpeed = 10f;
    protected Transform projectileTarget;
    public int bombDamage;
    public float bombRange;
    public int missleDamage; 
    public enum projectileTypes{standard, slow, freeze, poison, burn}

    Rigidbody2D rb2d;

    void Start()
    {
        
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


    void Update()
    {
        //If not enemy to shoot, destroy bullet, do not calculate distance or direction
        if (projectileTarget == null)
        {
            gameObject.SetActive(false);
            return;
        }

        //Calculate distance(based on speed) and direction for projectile to go
        Vector3 path = projectileTarget.position - transform.position;
        float frameDistance = projectileSpeed * Time.deltaTime;

        //Check if projectile has reached the target enemy
        if (path.magnitude <= frameDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(path.normalized * frameDistance, Space.World); //Move Projectile Towards Target 
    }

    //Decremet enemy health based on turret damage, destroy enemy when health is 0.
    public virtual void HitTarget(){}
}
