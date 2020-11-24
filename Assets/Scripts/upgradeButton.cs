using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeButton : MonoBehaviour
{
    SingleShooterTowerController parent;
    private void Start()
    {
        //parent = transform.parent.GetComponent<SingleShooterTowerController>(); 
    }
    private void OnMouseDown()
    {
        GetComponentInParent<TowerController>().upgrade();
    }
}
