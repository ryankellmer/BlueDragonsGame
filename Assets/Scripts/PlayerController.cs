using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    public Transform positions;

    [Header("Player Stats")]
    public int maxHealth = 5;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inpX = Input.GetAxisRaw("Horizontal");
        if (inpX != 0)
        {
            bod.AddForce(Vector2.right * inpX * spd * Time.deltaTime);
        }

        float inpY = Input.GetAxisRaw("Vertical");
        if (inpY != 0)
        {
            bod.AddForce(Vector2.up * inpY * spd * Time.deltaTime);
        }


    }

    public void ChangePlayerHealth(int amount)
    {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
        UnityEngine.Debug.Log("Player Health: " + health + "/" + maxHealth);
    }
}
