using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerV2 : MonoBehaviour
{
	//Enemy Stats
    public int currentHealth;
    public int maxHealth;
    public int attackDamage = 1;
    private int waypointIndex = 0;
    public List<Vector3> waypoints;
    public GameObject player;

    [SerializeField]
    float speed;
    
    void Start()
    {
        // Get waypoints from the Path gameobject function 'Positions'
        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;

        // Set Enemy starting position to first waypoint in the Index, waypoints[0] (spawnpoint)
        transform.position = waypoints[waypointIndex]; 

        // Find player object that the enemy will later attack.
        player = GameObject.FindWithTag("Player");

        // Set enemy health to its max health
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Move from current position towards next waypoint at a set speed.
    	transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

        // Finding current direction headed, and rotating sprite to face forward direction.
        Vector3 dir = waypoints[waypointIndex] - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Check if enemy has reached the next waypoint. If so, increment waypoint index if there are more waypoints available
        // If enemy has reached final waypoint, delete the enemy.
        if(Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.01f)
        {
            if(waypointIndex < waypoints.Count-1)
                waypointIndex++;
            else
            {
                player.GetComponent<PlayerController>().ChangePlayerHealth(attackDamage);
                Destroy(gameObject);
            }
        }
    }

    // Function to be called by projectile controller
    // Takes in damage value of projectile and changes enemy health accordingly
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
}