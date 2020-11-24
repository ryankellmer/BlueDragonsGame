using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllerV2 : MonoBehaviour
{
	//Enemy Stats
    public int maxHealth = 10;
    public int currentHealth;
    public float attackDamage;
    public float moneyDrop;
    public float scoreValue;
    int waypointIndex = 0;

    public List<Vector3> waypoints;
    public GameController GameCtrl;
    public Transform enemySprite;

    public Slider slider;

    [SerializeField]
    float speed;
    
    
    void Start()
    {
        // Get waypoints from the Path gameobject function 'Positions'
        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;

        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();

        // Set Enemy starting position to first waypoint in the Index, waypoints[0] (spawnpoint)
        transform.position = waypoints[waypointIndex]; 

        // Find player object that the enemy will later attack.
        

        // Set enemy health to its max health
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    //Resets Enemy stats for when they are returned to object pool either at end of level of reach 0 health
    void ResetEnemy() {
        waypointIndex = 0;
        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;
        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        transform.position = waypoints[waypointIndex]; 
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Move from current position towards next waypoint at a set speed.
    	transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

        enemySprite = this.gameObject.transform.GetChild(0);
        
        // Finding current direction headed, and rotating sprite to face forward direction.
        Vector3 dir = waypoints[waypointIndex] - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle-90, Vector3.forward);
        enemySprite.rotation = Quaternion.Slerp(enemySprite.rotation, q, Time.deltaTime * 10f);

        // Check if enemy has reached the next waypoint. If so, increment waypoint index if there are more waypoints available
        // If enemy has reached final waypoint, delete the enemy.
        if(Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.01f)
        {
            if(waypointIndex < waypoints.Count-1)
                waypointIndex++;
            else
            {
                GameCtrl.TakeDamage(attackDamage);
                GameCtrl.AddScore((-.25f)*scoreValue);
                //Destroy(gameObject);
                ResetEnemy();
                gameObject.SetActive(false);
            }
        }
    }

    // Function to be called by projectile controller
    // Takes in damage value of projectile and changes enemy health accordingly
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        SetHealth(currentHealth);   //Healthbar value

        //Give small amount of money when hit
        GameCtrl.AddMoney(.1f*moneyDrop);

        if(currentHealth < 1)
        {
            GameCtrl.AddScore(scoreValue);
            GameCtrl.AddMoney(moneyDrop);
            //Destroy(gameObject);
            ResetEnemy();
            gameObject.SetActive(false);
        }
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

}