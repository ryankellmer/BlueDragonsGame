using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;


public class BombController : ProjectileController
{
    public int bombDamage = 1;
    public int bombRange = 3;

    List<GameObject> enemiesInBlast = new List<GameObject>();
    

    void Start(){
        GetComponent<BoxCollider2D>().size = new Vector2((bombRange*2), (bombRange*2));
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if((other.tag == "Enemy") && (!enemiesInBlast.Contains(other.gameObject))){
            enemiesInBlast.Add(other.gameObject);
            UnityEngine.Debug.Log("Enemy Added to List");
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        enemiesInBlast.Remove(other.gameObject);
    }

    public override void HitTarget(){
        for(int i = 0; i < enemiesInBlast.Count; i++){
            EnemyControllerV2 enemy = enemiesInBlast[i].GetComponent<EnemyControllerV2>();
            enemy.ChangeHealth(bombDamage);
        gameObject.SetActive(false);
        }
    }
    
}
