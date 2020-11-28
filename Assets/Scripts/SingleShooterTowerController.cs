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

    public GameController GameCtrl;

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
        type = towerTypes.standard;

        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = singleShooterBaseRange; //Set Box Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = singleShooterBaseAttack;
        currentRange = singleShooterBaseRange; 
        towerCost = 50;
        upgradeCost = 50;
        timeBeforeNextShot = 5.0f;

        //Generate line to be used for tower radius ring
        GenerateRing();

        lineRenderer.enabled = false;
        upgradeCost = 75;
    }

    private void OnMouseDown()
    {
        
    }

    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public override void upgrade(){
        if (level == towerLevel.start){
            if (upgradeCost <= GameCtrl.moneyAmt()){
                GameCtrl.RemoveMoney(upgradeCost);
                level = towerLevel.mid;
                currentAttack = singleShooterMidAttack;
                currentRange = singleShooterMidRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= .5f;
                GenerateRing();
                upgradeCost = 100;
                return;
             }
        }
        if (level == towerLevel.mid){
            if (upgradeCost <= GameCtrl.moneyAmt())
            {
                type = towerTypes.burn;
                GameCtrl.RemoveMoney(upgradeCost); 
                level = towerLevel.high;
                currentAttack = singleShooterHighAttack;
                currentRange = singleShooterHighRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= .5f;
                GenerateRing();
                return;
            }
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
