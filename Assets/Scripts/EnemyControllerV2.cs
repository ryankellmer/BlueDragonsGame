using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerV2 : MonoBehaviour
{
	//Enemy Stats
	public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public int currentHealth;
    public int attackDamage = 1;

    [SerializeField]
    float speed;

    Rigidbody2D rigidbody2D;

    [SerializeField]
    int waypointIndex = 0;

    public List<Vector3> waypoints;


    void Start()
    {
        GameObject path = GameObject.Find("Path");
        waypoints = path.GetComponent<Path>().Positions;
        transform.position = waypoints[waypointIndex]; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);
        Vector3 dir = waypoints[waypointIndex] - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.1f)
        {
            if(waypointIndex < waypoints.Count-1)
            {
                waypointIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Move()
    {
    	
    }
}