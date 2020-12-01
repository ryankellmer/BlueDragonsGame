using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Necessary Public Variables")]
    public int maxHealth;
    public int currentHealth;
    public int defense;
    public float attackDamage;
    public float moneyDrop;
    public float scoreValue;
    public int waypointIndex = 0;
    public List<Vector3> waypoints;
    public GameController GameCtrl;
    public Transform enemySprite;
    public Slider slider;
    public float normalSpeed;
    public float slowSpeed;
    public float currentSpeed;

    [Header("Enemy Resistances")]
    [Range(0, 100)]
    public float poisonResistance;
    protected float curPoison = 0;
    public float poisonTime = 8f;
    [Range(0, 100)]
    public float burnResistance;
    protected float curBurn = 0;
    public float burnTime = 6f;
    [Range(0, 100)]
    public float slowResistance;
    protected float curSlow = 0;
    public float slowTime = 7f;
    [Range(0, 100)]
    public float freezeResistance;
    protected float curFreeze = 0;
    public float freezeTime = 5f;
    public float timeBetweenPoisonDamage = 0.5f;
    public float timeBetweenBurnDamage = 0.5f;

    public float iframes = 0.15f;
    float cools = 0f;

    //Cooldowns for status effects
    protected float poisonCools = 0f;
    protected float poisonDmgCools = 0f;
    protected float burnCools = 0f;
    protected float burnDmgCools = 0f;
    protected float slowCools = 0f;
    protected float freezeCools = 0f;

    //Damage done by poison and burn effects
    [Header("Damage taken via status effects")]
    public int poisonDamage = 3;
    public int burnDamage = 2;


    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void ResetStuff()
    {
        maxHealth = 15;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        defense = 1;
        attackDamage = 10;
        moneyDrop = 10;
        scoreValue = 10;
        normalSpeed = 1.5f;
        slowSpeed = 0.5f;
        poisonResistance = 0f;
        burnResistance = 0f;
        slowResistance = 0f;
        freezeResistance = 0f;
        curBurn = 0f;
        curFreeze = 0f;
        curPoison = 0f;
        curSlow = 0f;
        poisonCools = 0f;
        poisonDmgCools = 0f;
        burnCools = 0f;
        burnDmgCools = 0f;
        slowCools = 0f;
        freezeCools = 0f;
        //currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void ResetPos()
    {
        waypointIndex = 0;
        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;
        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        transform.position = waypoints[waypointIndex];
    }

    void Update()
    {
       if(cools > 0) cools -= Time.deltaTime;

       if(poisonCools > 0)
       {
           poisonCools -= Time.deltaTime;
           poisonDmgCools -= Time.deltaTime;
           if(poisonDmgCools <= 0f)
           {
               poisonDmgCools = timeBetweenPoisonDamage;
               currentHealth = Mathf.Clamp(currentHealth - poisonDamage, 0, maxHealth);
               SetHealth(currentHealth);
               if(currentHealth <= 0) Die();
           }
       } 

       if(burnCools > 0)
       {
           burnCools -= Time.deltaTime;
           burnDmgCools -= Time.deltaTime;
           if(burnDmgCools <= 0f)
           {
               burnDmgCools = timeBetweenBurnDamage;
               currentHealth = Mathf.Clamp(currentHealth - burnDamage, 0, maxHealth);
               SetHealth(currentHealth);
               if(currentHealth <= 0) Die();
           }

       }

       if(slowCools > 0)
       {
           slowCools -= Time.deltaTime;
           currentSpeed = slowSpeed;
       }

       if(freezeCools > 0)
       {
           Debug.Log("Freezing enemy");
           freezeCools -= Time.deltaTime;
           currentSpeed = 0f;
       }

       if(slowCools <= 0f)
       {
           if(freezeCools <= 0f) currentSpeed = normalSpeed;
           else currentSpeed = 0f;
       }
       if(freezeCools <= 0f)
       {
           if(slowCools <= 0f) currentSpeed = normalSpeed;
           else currentSpeed = slowSpeed;
       }
    }

    void FixedUpdate()
    {
        Move();
    }

    // Function to be called by projectile controller
    // Takes in damage value of projectile and changes enemy health accordingly
    public void AttackEnemy(int amount, TowerController.towerTypes type, float potency)
    {
        if (cools <= 0)
        {
            switch(type)
            {
                case (TowerController.towerTypes.standard):
                    Debug.Log("Standard Attack");
                    currentHealth = Mathf.Clamp(currentHealth - CalculateDamage(amount), 0, maxHealth);
                    Debug.Log(currentHealth);
                    SetHealth(currentHealth);
                    GameCtrl.AddMoney(1f);
                    if(currentHealth <= 0) Die();
                    break;
                case (TowerController.towerTypes.poison):
                    Debug.Log("Poison Attack");
                    currentHealth = Mathf.Clamp(currentHealth - CalculateDamage(amount), 0, maxHealth);
                    SetHealth(currentHealth);
                    GameCtrl.AddMoney(1f);
                    if(currentHealth <= 0) Die();
                    curPoison += potency;
                    if(curPoison >= poisonResistance && poisonCools <= 0)
                    {
                        poisonDmgCools = timeBetweenPoisonDamage;
                        poisonCools = poisonTime;
                    }
                    break;
                case (TowerController.towerTypes.burn):
                    Debug.Log("Burn Attack");
                    currentHealth = Mathf.Clamp(currentHealth - CalculateDamage(amount), 0, maxHealth);
                    SetHealth(currentHealth);
                    GameCtrl.AddMoney(1f);
                    if(currentHealth <= 0) Die();
                    curBurn += potency;
                    if(curBurn >= burnResistance && burnCools <= 0)
                    {
                        burnDmgCools = timeBetweenBurnDamage;
                        burnCools = burnTime;
                    }
                    break;
                case (TowerController.towerTypes.freeze):
                    Debug.Log("Freeze Attack");
                    currentHealth = Mathf.Clamp(currentHealth - CalculateDamage(amount), 0, maxHealth);
                    SetHealth(currentHealth);
                    GameCtrl.AddMoney(1f);
                    if(currentHealth <= 0) Die();
                    curFreeze += potency;
                    Debug.Log(curFreeze);
                    if(curFreeze >= freezeResistance && freezeCools <= 0) freezeCools = freezeTime;
                    break;
                case (TowerController.towerTypes.slow):
                    Debug.Log("Slow Attack");
                    currentHealth = Mathf.Clamp(currentHealth - CalculateDamage(amount), 0, maxHealth);
                    SetHealth(currentHealth);
                    GameCtrl.AddMoney(1f);
                    if(currentHealth <= 0) Die();
                    curSlow += potency;
                    if(curSlow >= slowResistance && slowCools <= 0) slowCools = slowTime;
                    break;

            }
            cools = iframes;
            anim.SetBool("flash", true);
            Invoke("ResetFlash", 0.01f);
        }
    }

    void ResetFlash()
    {
        anim.SetBool("flash", false);
    }

    void Move()
    {
        if (waypoints.Count > 0)
        {
            // Move from current position towards next waypoint at a set speed.
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], currentSpeed * Time.deltaTime);

            enemySprite = this.gameObject.transform.GetChild(0);

            // Finding current direction headed, and rotating sprite to face forward direction.
            Vector3 dir = waypoints[waypointIndex] - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            enemySprite.rotation = Quaternion.Slerp(enemySprite.rotation, q, Time.deltaTime * 10f);

            // Check if enemy has reached the next waypoint. If so, increment waypoint index if there are more waypoints available
            // If enemy has reached final waypoint, delete the enemy.
            if (Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.01f)
            {
                if (waypointIndex < waypoints.Count - 1)
                    waypointIndex++;
                else
                {
                    GameCtrl.TakeDamage(attackDamage);
                    GameCtrl.AddScore((-.25f) * scoreValue);
                    //Destroy(gameObject);
                    ResetStuff();
                    transform.position = new Vector3(-100, 100, 0);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void Die()
    {
        GameCtrl.AddScore(scoreValue);
        GameCtrl.AddMoney(moneyDrop);
        //Destroy(gameObject);
        ResetStuff();
        transform.position = new Vector3(-100, 100, 0);
        gameObject.SetActive(false);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public int CalculateDamage(int atk){
        int tot = atk - defense;
        if(tot > 0) return tot;
        return 1;
    }
}
