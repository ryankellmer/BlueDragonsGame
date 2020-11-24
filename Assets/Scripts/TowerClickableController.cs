using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickableController : MonoBehaviour
{
    //Draw circle indicating tower range when tower is clicked
    void OnMouseDown(){
        transform.parent.gameObject.GetComponent<TowerController>().DoRenderer();
    } 
}
