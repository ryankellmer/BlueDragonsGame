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
    protected GameObject enemyCol;
    public TowerController.towerTypes type;

    //Obtain rigidbody of projectile when created
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Disable projectile after certain amount of time if it fails to reach target
    public virtual void Start(){
        Invoke("DisableProjectile", 3f);
    }

    //Obtain position of target, push projectile towards target every frame
    public virtual void Update(){
        if(projectileTarget == null){
            gameObject.SetActive(false);
        }
        if(!projectileTarget.gameObject.activeInHierarchy){
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

    //Sends targets, attack type, and attack damage from towers to projectiles
    public void ReceiveTarget(Transform turretTarget, int d, TowerController.towerTypes towerType)
    {
        projectileTarget = turretTarget;
        type = towerType;
    }

    public void BombReceiveStats(int damage, float range){
        bombDamage = damage;
        bombRange = range;
    }

    public void MissleReceiveStats(int damage){
        missleDamage = damage; 
    }

    //Check if projectile has collided with target, if so deactivate projectile and hitTarget
    private void OnTriggerEnter2D(Collider2D other) {
        enemyCol = other.gameObject;
        if(enemyCol.transform == projectileTarget){
            gameObject.SetActive(false);
            HitTarget();
        }
    }

    //Returns Projectile to Object Pool
    protected void DisableProjectile(){
        gameObject.SetActive(false);
    }

    //Decremet enemy health based on turret damage, destroy enemy when health is 0.
    public virtual void HitTarget(){}
}
