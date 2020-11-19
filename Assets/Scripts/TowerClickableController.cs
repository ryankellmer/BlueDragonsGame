using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickableController : MonoBehaviour
{
    void OnMouseDown(){
        //Debug.Log("Mouse down");
        transform.parent.gameObject.GetComponent<TowerController>().ChildClicked();
    } 
}
