using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tower_spawning : MonoBehaviour
{
    public GameObject the_prefab, tower_2, tower_3;
    public GameObject tower_placement_UI;
    bool already = false;

    private void Awake()
    {
        tower_placement_UI = FindObjectOfType<GameController>().towerUI;
    }

    private void OnMouseDown()
    {
        tower_placement_UI.SetActive(true); 



        if (already == false) { check_tower(); }
    }

    void check_tower()
    {
        Instantiate(the_prefab, transform.position, Quaternion.identity);
        already = true;
        tower_placement_UI.SetActive(false);
    }
}
