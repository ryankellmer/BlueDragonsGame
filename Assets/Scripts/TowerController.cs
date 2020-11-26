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
    public string tagOfTarget = "Enemy"; 
    public GameObject ProjectilePrefab;
    public GameObject RangeCircle;
    public enum towerTypes{standard, slow, freeze, poison, burn}
    public enum towerLevel {start, mid, high}
    public float count;
    protected float currentRange; 
    public int currentAttack; 
    protected float timeBeforeNextShot;
    protected float rotationSpeed = .5f;
    public towerTypes type; 
    public towerLevel level;
    public int towerCost;
    public int upgradeCost; 

    public bool clicked;

    [Range(0.1f, 100f)]
    public float radius = 1.0f;

    [Range(3, 256)]
    public int numSegments = 28;

    protected LineRenderer lineRenderer;

    public virtual void Start(){}
 
    public virtual void Update(){}

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

        if (closestTarget != null && closestTargetDistance <= currentRange)
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
        count += Time.deltaTime;
        if (count > timeBeforeNextShot)
        {
            Shoot();
            count = 0;
        }
    }

    //When tower's current target leaves collider, allow tower to look for new target by setting target to null 
    public void OnTriggerExit2D(Collider2D col){
        if(col.transform == target){
            target = null;
            //UnityEngine.Debug.Log("Target Leaving Collider");
        }
    }

    //Shoot function defined individually by each child tower
    public virtual void Shoot(){}
        
    //When child clickable object is clicked, draw line around tower indicating range, when clicked again turn off line
    public void DoRenderer ( ) {
        if(clicked == false){
            lineRenderer.enabled = true;
            clicked = true;
        }
        else {
            lineRenderer.enabled = false;
            clicked = false;
        }
        
    }

    public void GenerateRing(){
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        Color c1 = new Color(0.5f, 0.5f, 0.5f, 1);
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.15f, 0.15f);
        lineRenderer.SetVertexCount(numSegments + 1);
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = currentRange * Mathf.Cos(theta);
                float y = currentRange * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, y, 0);
                lineRenderer.SetPosition(i, pos);
                theta += deltaTheta;
        }
    }
  
    //When tower is selected in scene view, circle will be drawn to show tower range
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, currentRange);
    }
    public virtual void upgrade() { }
    public virtual int returnAttack() { return 1; }
    public virtual int returnRange() { return 1;  }
}
