using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class upgradeButton : MonoBehaviour
{
    SingleShooterTowerController parent;
    Vector2 startPos;
    private void Start()
    {
        startPos = transform.localPosition;
        //parent = transform.parent.GetComponent<SingleShooterTowerController>(); 
    }
    private void OnMouseDown()
    {
        GetComponentInParent<TowerController>().upgrade();
    }

    private void LateUpdate()
    {
        transform.position = (Vector2)transform.parent.position + startPos;
        transform.rotation = Quaternion.identity;
    }
}
