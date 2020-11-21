using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;



public class MapSelect : MonoBehaviour
{
    private Map[] maps;// = new Map[4];

    public void SelectMap(int mapNumber){
        for(int i = 0; i < 4; i++){
            if(i == mapNumber){
                GameObject selectedMap = GameObject.Find("Ground " + i);
                selectedMap.transform.position = Vector3.zero;
                selectedMap.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                selectedMap.GetComponent<MapGenerator>().GenerateScenary();
                selectedMap.GetComponent<MapGenerator>().SetPath();
            } else {
                Destroy(GameObject.Find("Ground " + i));
            }
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
