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

    public virtual void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2)); //Set Trigger area size equal to range
        count = 0f;
        type = towerTypes.standard; 
        level = towerLevel.start;
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
            //Debug.Log(distanceToTarget);
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

       RotateTower(target);
    }

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
    public void RotateTower(Transform currentTarget)
    {
        if (currentTarget != null)
        {
            //Vector3 targetPos = currentTarget.position;
            //Quaternion towerPos = transform.rotation;
            Vector3 dir = currentTarget.position - transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //transform.rotation = Quaternion.Lerp(towerPos, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
    }

    //When enemy enters trigger box, search for closest enemy by calling UpdateTarget function twice a second
    //Enemies and tower will need box collider 2d
    void OnTriggerStay2D()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public virtual void Shoot(){}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
