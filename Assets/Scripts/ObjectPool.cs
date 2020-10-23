using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Universal Object Pooling Script
To Add Object to list of Objects to be pooled:
1. Increment "itemsToPool" in the inspector for this script in GameController
2. Attach Prefab of GameObject you wish to pool
3. Set amountToPool to desired amount of objects that should be in list to start with
*/

[System.Serializable]
public class ObjectPoolItem {
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    void Awake() {
        SharedInstance = this;
    }

    //Create individual lsits for each game object set to pool in inspector
    void Start () {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool) {
            for (int i = 0; i < item.amountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }     
    }

    //Returns a non-used gameobject from pool create game object if all objects in pool are being used
    public GameObject GetPooledObject(string tag) {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
                return pooledObjects[i];
            }
        }
        //UnityEngine.Debug.Log("Test");
        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.objectToPool.tag == tag) {
                if (item.shouldExpand) {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }       
		
        }
        return null;
    }
}