using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public static Tilemap CurrentMap{ get; set; }
    // Start is called before the first frame update
    public void SelectMap(string mapNumber){
        MapSelect.CurrentMap = GameObject.Find("Ground " + mapNumber).GetComponent<Tilemap>();
        SceneManager.LoadScene("BlakeScene");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
