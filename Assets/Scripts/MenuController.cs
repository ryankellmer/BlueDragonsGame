using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public SpriteRenderer playerRend;
    public void ChangeColor()
    {
        FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>().color = Color.red;
        playerRend.color = Color.green;
    }
}
