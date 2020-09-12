using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ronan_EnemyController : MonoBehaviour
{
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;
	public float speed = 5.0f;
    public GameObject player;
    public int attackDamage = 1;


    Rigidbody2D rigidbody2D;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public int currentHealth;

    

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = waypoints [currentWaypoint].transform.position;
        Vector3 endPos = waypoints [currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance (startPos, endPos);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time- lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp (startPos, endPos, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPos))
        {
        	if (currentWaypoint < waypoints.Length -2)
        	{
        		currentWaypoint++;
        		lastWaypointSwitchTime = Time.time;
        	}

        	else
        	{
                player.GetComponent<PlayerController>().ChangePlayerHealth(attackDamage); //When enemy reaches end of level, decrement player health 
                Destroy(gameObject);
        	}
        }
    }


    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UnityEngine.Debug.Log(currentHealth + "/" + maxHealth);
    }
}
