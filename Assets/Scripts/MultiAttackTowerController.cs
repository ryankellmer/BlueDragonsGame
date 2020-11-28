using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackTowerController : TowerController
{

   List<GameObject> allTargets = new List<GameObject>();
   public Transform currentTarget;

    public int multiShooterBaseAttack = 5;
    public int multiShooterMidAttack = 7;
    public int multiShooterHighAttack = 9;
    public float multiShooterBaseRange = 5f;
    public float multiShooterMidRange = 6.5f;
    public float multiShooterHighRange = 8.0f;

    public GameController GameCtrl;
    public AudioClip shotSound;
    AudioSource audioSource; 


    public override void Start()
    {
        GameCtrl = FindObjectOfType<GameController>();
        type = towerTypes.standard;

        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = multiShooterBaseRange; //Set Circle Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = multiShooterBaseAttack;
        currentRange = multiShooterBaseRange; 
        towerCost = 75;
        upgradeCost = 50;
        timeBeforeNextShot = 30.0f;

        //Generate line to be used for tower radius ring
        GenerateRing();

        lineRenderer.enabled = false;
        upgradeCost = 75;
    }

    //Upgrade Tower attack speed, range, rotation speed, attack damage, and number of enemies to attack
    public override void upgrade(){
        if (level == towerLevel.start){
            if (GameCtrl.GetMoney() >= upgradeCost)
            {
                level = towerLevel.mid; 
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = multiShooterMidAttack;
                currentRange = multiShooterMidRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= 5.0f;
                GenerateRing();
                upgradeCost = 100;
                return;
            }
        }
        if (level == towerLevel.mid){
            if (GameCtrl.GetMoney() >= upgradeCost)
            {
                type = towerTypes.freeze;
                level = towerLevel.high; 
                currentAttack = multiShooterHighAttack;
                currentRange = multiShooterHighRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= 5.0f;
                GenerateRing();
                return;
            }
        }
        if (level == towerLevel.high){
            return;
        }
    }

    
    public override void Shoot(){
      if(target == null){
            return;
        }
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("HomingMissle"); //Grab a homing-missle from object pool
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
