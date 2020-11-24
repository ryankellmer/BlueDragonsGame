using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class TowerClickableController : MonoBehaviour
{
    //Draw circle indicating tower range when tower is clicked
    void OnMouseDown()
    {
        transform.parent.gameObject.GetComponent<TowerController>().DoRenderer();
    }
}
