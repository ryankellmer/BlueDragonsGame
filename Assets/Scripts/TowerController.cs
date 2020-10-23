using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class TowerController : MonoBehaviour
{
   
    [Header("Necessary Public Variables")]
    public Transform target;
    public string tagOfTarget = "Enemy"; //targets will need "Enemy" tag for towers to be able to find them
    public GameObject ProjectilePrefab;
    public enum towerTypes{standard, slow, freeze, poison, burn}
    public enum towerLevel {start, mid, high}
    public float count;

    [Header("Tower Stats")]
    [Range(0f, 20f)]
    public float range = 2f;
    public float currentRange; //Specific to Bomb Tower's Bombs
    public int currentAttack; 
    public float timeBeforeNextShot = 0.75f;
    public int damage = 1;
    public float rotationSpeed = .5f;
    public towerTypes type; 
    public towerLevel level;
    public int towerCost;
    public int upgradeCost; 

    public virtual void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2)); //Set Trigger area size equal to range
        count = 0f;
        type = towerTypes.standard; 
        level = towerLevel.start;
        towerCost = 50;
        upgradeCost = 50;
        target = null;
    }

    //Wait to shoot next projectile
    public virtual void Update()
    {
        count += Time.deltaTime;

        if (count > timeBeforeNextShot)
        {
            Shoot();
            count = 0;
        }
    }

    //Locate Target
    public virtual void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOfTarget);
        float closestTargetDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach (GameObject element in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, element.transform.position);
            if (distanceToTarget < closestTargetDistance)
            {
                closestTargetDistance = distanceToTarget;
                closestTarget = element;
            }
        }

        if (closestTarget != null && closestTargetDistance <= range)
        {
            target = closestTarget.transform;
        }
        else
        {
            target = null;
        }
    }

    //Series of upgrade functions to upgrade tower to different type
    public void upgradetoFreeze(){
        type = towerTypes.freeze; 
    }
    public void upgradeToPoison(){
        type = towerTypes.poison; 
    }

    public void upgradeToBurn(){
        type = towerTypes.burn; 
    }

    public void upgradeToSlow(){
        type = towerTypes.slow; 
    }

    //Rotate Tower Guns towards Enemy
    public virtual void RotateTower()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
    }

    //When enemy enters trigger box, search for closest enemy if no enemy is currently selected by tower as target, rotate tower twice a second towards target
    public void OnTriggerStay2D()
    {
        if(target == null){
            UpdateTarget();
        }
        InvokeRepeating("RotateTower", 0f, 0.5f);
    }

    //When tower's current target leaves collider, allow tower to look for new target by setting target to null 
    public void OnTriggerExit2D(Collider2D col){
        if(col.transform == target){
            target = null;
            UnityEngine.Debug.Log("Target Leaving Collider");
        }
    }

    public virtual void Shoot(){}

    //When tower is selected in game view, circle will be drawn to show tower range
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
