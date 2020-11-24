/*Temp EnemyController to demo turret attack and damage
 EnemyController will need: ChangeHealth() and Box Collider 2D Component to Trigger turret when within range*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public bool vertical;
    public float changeTime = 5.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public int currentHealth;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        currentHealth = maxHealth;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        position.x = position.x + Time.deltaTime * speed * direction; ;

        rigidbody2D.MovePosition(position);
    }



    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UnityEngine.Debug.Log(currentHealth + "/" + maxHealth);
    }

}
