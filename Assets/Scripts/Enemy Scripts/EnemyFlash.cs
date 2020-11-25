using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    public GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        void OnCollsionEnter2D(Collision2D collision) 
        { 
            if (collision.gameObject.tag == "Bomb") 
            {

                Enemy.GetComponent<Animator>().Play("Flash");
            
            }
        }
        
    }
}
