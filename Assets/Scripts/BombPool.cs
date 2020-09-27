using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{

    public static BombPool SharedInstance;
    public List<GameObject> objectsPooled;
    public GameObject objectToPool;
    public int numObjectsToPool = 3;
    
    
    void Awake() {
        SharedInstance = this;
    }
    void Start()
    {
        //declare which game object to pool and the amount to pool
        objectsPooled = new List<GameObject>();
        GameObject tmp;
        for(int i =0; i < numObjectsToPool; i++) {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            objectsPooled.Add(tmp);
            }  
    }

    //Function so scripts can set inactive objects to active
    public GameObject GetPooledObject(int numObjectsToPool)
    {
        for(int i = 0; i < numObjectsToPool; i++){
            if(!objectsPooled[i].activeInHierarchy){
                return objectsPooled[i];
            }
        }
        return null;
    }
}
