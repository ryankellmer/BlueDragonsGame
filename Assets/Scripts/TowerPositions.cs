using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TowerPositions : MonoBehaviour
{
    public GameObject towerPosition;

    public void CreateTowerPositions(){
        Tilemap tiles = GameObject.Find("Ground").GetComponent<Tilemap>();
        MapGenerator mapGenerator = GameObject.Find("Ground").GetComponent<MapGenerator>();
        GameObject foliage = GameObject.Find("Foliage");
        List<Vector3Int> positions = new List<Vector3Int>();
        BoundsInt towerBounds = tiles.cellBounds;
        //towerBounds.yMin += 1; // These lines are used to limit the tower placement rectangle
        //towerBounds.yMax -= 1;
        foreach(var t in towerBounds.allPositionsWithin){
            if(tiles.GetTile(t) == mapGenerator.fillTile){
                positions.Add(t);
            }
        }
        //Debug.Log(foliage.transform.childCount);
        foreach(Transform t in foliage.transform){
            positions.Remove(new Vector3Int((int)t.localPosition.x,(int)t.localPosition.y, positions[0].z));
        }
        foreach(Vector3Int p in positions){
            var towerPos = Instantiate(towerPosition, p + new Vector3(.5f,.5f,0), Quaternion.identity);
            towerPos.transform.parent = transform;
        }
    }
}
