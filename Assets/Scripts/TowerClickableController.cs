using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickableController : MonoBehaviour
{
    void OnMouseDown(){
        transform.parent.gameObject.GetComponent<TowerController>().ChildClicked();
    } 
}
