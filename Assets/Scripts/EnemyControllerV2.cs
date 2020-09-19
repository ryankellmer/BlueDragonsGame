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
    float speed = 5.0f;

    Rigidbody2D rigidbody2D;

    [SerializeField]
    int waypointIndex = 0;

    // test stuff to be deleted
    float delay = 3f;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
    	GameObject path = GameObject.Find("Path");
    	List<Vector2Int> waypoints = new List<Vector2Int>();
    	waypoints = path.GetComponent<Path>().wayPoints;
        transform.position = new Vector3(waypoints[waypointIndex].x, waypoints[waypointIndex].y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > delay)
        {
        	Move();
        	timer = 0f;
        }
    }

    void Move()
    {
    	GameObject path = GameObject.Find("Path");
    	List<Vector2Int> waypoints = new List<Vector2Int>();
    	waypoints = path.GetComponent<Path>().wayPoints;
    	waypointIndex += 1;
    	transform.position = new Vector3(waypoints[waypointIndex].x, waypoints[waypointIndex].y, 0);
    }
}