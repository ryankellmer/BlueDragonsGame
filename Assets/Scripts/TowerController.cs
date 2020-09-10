using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private float count;

    [Header("Necessary Public Variables")]
    public Transform target;
    public string tagOfTarget = "Enemy"; //targets will need "Enemy" tag for towers to be able to find them
    public GameObject ProjectilePrefab;

    [Header("Tower Stats")]
    public float range = 20f;
    public float timeBeforeNextShot = 0.75f;
    public int damage = 1;
    public float rotationSpeed = 25f;

    void Start()
    {
        count = 0f;
    }

    //Wait to shoot next projectile
    void Update()
    {
        count += Time.deltaTime;

        if (count > timeBeforeNextShot)
        {
            Shoot();
            count = 0;
        }
    }

    //Locate Target
    void UpdateTarget()
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
            RotateTower(target);
        }
        else
        {
            target = null;
        }

    }

    //Rotate Tower Guns towards Enemy
    private void RotateTower(Transform currentTarget)
    {
        Vector3 targetPos = currentTarget.position;
        Quaternion towerPos = transform.rotation;
        float angle = (Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg) - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(towerPos, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //When enemy enters trigger box, search for closest enemy by calling UpdateTarget function twice a second
    //Enemies and tower will need box collider 2d
    void OnTriggerStay2D()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Shoot()
    {
        GameObject ProjectileGO = (GameObject)Instantiate(ProjectilePrefab, transform.position, transform.rotation);
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();

        if (Projectile != null)
        {
            Projectile.ReceiveTarget(target, damage); //Pass target to ProjectileController and damage amount
        }
    }

}
