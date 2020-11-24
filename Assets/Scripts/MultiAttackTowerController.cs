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
        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>(); 
        GetComponent<CircleCollider2D>().radius = multiShooterBaseRange; //Set Circle Collider equal to range, so tower does not seek enemies unless they are close enough to hit
        count = 0f;
        currentAttack = multiShooterBaseAttack;
        currentRange = multiShooterBaseRange; 
        towerCost = 75;
        upgradeCost = 50;
        timeBeforeNextShot = 30.0f;

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

    //Upgrade Tower attack speed, range, rotation speed, attack damage, and number of enemies to attack
    public override void upgrade(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            if (GameCtrl.moneyAmt() <= upgradeCost)
            {
                level = towerLevel.mid; 
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = multiShooterMidAttack;
                currentRange = multiShooterMidRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= 5.0f;
            }
        }
        upgradeCost = 100;
        if (level == towerLevel.mid){
            if (GameCtrl.moneyAmt() <= upgradeCost)
            {
                level = towerLevel.high; 
                currentAttack = multiShooterHighAttack;
                currentRange = multiShooterHighRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= 5.0f;
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
