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
    lineRenderer = gameObject.GetComponent<LineRenderer>();
        Color c1 = new Color(0.5f, 0.5f, 0.5f, 1);
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.15f, 0.15f);
        lineRenderer.SetVertexCount(numSegments + 1);
        lineRenderer.useWorldSpace = false;

        GameCtrl = GameObject.Find("GameController").GetComponent<GameController>();

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


    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public override void upgrade(){
        if (level == towerLevel.start){
            upgradeCost = 75;
            if (upgradeCost <= GameCtrl.moneyAmt())
            {
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = bomberMidAttack;
                currentRange = bomberMidRange;
                currentBlastRange = blastMidRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= .5f;
            }
        }
        if (level == towerLevel.mid){
            upgradeCost = 100;
            if (upgradeCost <= GameCtrl.moneyAmt()){
                GameCtrl.RemoveMoney(upgradeCost); 
                currentAttack = bomberHighAttack;
                currentRange = bomberHighRange;
                currentBlastRange = blastHighRange;
                GetComponent<CircleCollider2D>().radius = currentRange;
                rotationSpeed += 0.5f;
                timeBeforeNextShot -= .5f;
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
