using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShooterTowerController : TowerController
{
    [Header("Single Shooter Tower Stats")]
    public int singleShooterBaseAttack = 3;
    public int singleShooterMidAttack = 5;
    public int singleShooterHighAttack = 7;
    public float singleShooterBaseRange = 2f;
    public float singleShooterMidRange = 3f;
    public float singleShooterHighRange = 4f;

    public AudioClip shotSound;
    AudioSource audioSource;

    public GameController gameController;

    public override int returnAttack()
    {
        return currentAttack; 
    }
    public override int returnRange()
    {
        return (int) currentRange; 
    }
    public override void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>(); 
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = singleShooterBaseRange; //Set Box Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = singleShooterBaseAttack;
        currentRange = singleShooterBaseRange; 
        towerCost = 50;
        upgradeCost = 50;
        timeBeforeNextShot = 5.0f;

        //Generate line to be used for tower radius ring
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

        lineRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        gameController.towerStatsText.text = currentAttack.ToString(); 
    }

    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public override void upgrade(){
        if (level == towerLevel.start){
            level = towerLevel.mid; 
            upgradeCost = 75;
            currentAttack = singleShooterMidAttack;
            currentRange = singleShooterMidRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
            rotationSpeed += 0.5f;
            timeBeforeNextShot -= .5f;
        }
        if (level == towerLevel.mid){
            level = towerLevel.high;
            upgradeCost = 100;
            currentAttack = singleShooterHighAttack;
            currentRange = singleShooterHighRange;
            GetComponent<CircleCollider2D>().radius = currentRange;
            rotationSpeed += 0.5f;
            timeBeforeNextShot -= .5f;
        }
        if (level == towerLevel.high){
            return;
        }
    }

    //Shoot projectile at current target 
    public override void Shoot(){
        if(target == null){
            return;
        }
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("Missle"); //Grab projectile from object pool
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
        Projectile.MissleReceiveStats(currentAttack);
        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, currentAttack, type); //Pass target to ProjectileController and damage amount
            audioSource.PlayOneShot(shotSound, 0.5F); 
        }  
   }
    
}
