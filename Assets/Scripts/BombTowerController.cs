using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerController : TowerController
{
    public float currentBlastRange; 
    [Header("Bomb Tower Upgrade Stats")]
    public int bomberBaseAttack = 1;
    public int bomberMidAttack = 3;
    public int bomberHighAttack = 5;
    public float bomberBaseRange = 2f;
    public float bomberMidRange = 2.5f;
    public float bomberHighRange = 3f;
    public float blastBaseRange = 1f;
    public float blastMidRange = 2f;
    public float blastHighRange = 3f;
    public GameController GameCtrl;
    public AudioClip shotSound;
    AudioSource audioSource; 

    public override void Start()
    {
        GameCtrl = FindObjectOfType<GameController>();
        type = towerTypes.standard;

        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = bomberBaseRange; //Set Box Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = bomberBaseAttack;
        currentRange = bomberBaseRange;
        currentBlastRange = blastBaseRange;
        towerCost = 100; 
        upgradeCost = 50;
        timeBeforeNextShot = 9.0f;
        

        //Generate line to be used for tower radius ring
        GenerateRing();
    

        lineRenderer.enabled = false;
        upgradeCost = 75;
    }


    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public override void upgrade(){
        if (level == towerLevel.start){
            if (upgradeCost <= GameCtrl.GetMoney())
            {
                level = towerLevel.mid;
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = bomberMidAttack;
                currentRange = bomberMidRange;
                currentBlastRange = blastMidRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= .5f;
                GenerateRing();
                upgradeCost = 100;
                return;
            }
        }
        if (level == towerLevel.mid){
            if (upgradeCost <= GameCtrl.GetMoney())
            {
                level = towerLevel.high;
                type = towerTypes.slow;
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = bomberHighAttack;
                currentRange = bomberHighRange;
                currentBlastRange = blastHighRange;
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


    //Send bomb to closest target 
   public override void Shoot(){
       if(target == null){
            return;
        }
        GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject("Bomb");
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
        Projectile.BombReceiveStats(currentAttack, currentBlastRange);
        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, currentAttack, type); //Pass target to ProjectileController and damage amount
            audioSource.PlayOneShot(shotSound, 0.5f); 
        }  
   }
    
}
