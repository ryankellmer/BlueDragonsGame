using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class GenerateScenary : MonoBehaviour
{
    public int spawnChance;
    // Start is called before the first frame update
    void Start()
    {
        Random rand = new Random();
        GameObject ground = GameObject.Find("Ground");
        Tilemap tilemap = ground.GetComponent<Tilemap>();
        Tile fillTile = ground.GetComponent<MapGenerator>().fillTile;
        string prefabFilePath = "";
        GameObject[] prefabs = Resources.LoadAll<GameObject>(prefabFilePath);
        GameObject parent = GameObject.Find("Foliage");
        foreach(var t in tilemap.cellBounds.allPositionsWithin){
            if(tilemap.GetTile(t) == fillTile){
                if(rand.Next(100) <= spawnChance){
                    GameObject o = Instantiate(prefabs[rand.Next(prefabs.Length)], t + new Vector3(.5f,.5f,-1), Quaternion.Euler(0, 0, rand.Next(361)));
                    o.transform.parent = parent.transform;
                }
            }
        }
       GameObject tp = GameObject.Find("TowerPositions");
       if(tp == null){
           GameObject tpPrefab = Resources.Load<GameObject>("Assets/Prefabs/TowerPositions.prefab");
           tp = Instantiate(tpPrefab, Vector3.zero, Quaternion.identity);
           tp.GetComponent<TowerPositions>().CreateTowerPositions();
       }
       else{
           tp.GetComponent<TowerPositions>().CreateTowerPositions();
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
