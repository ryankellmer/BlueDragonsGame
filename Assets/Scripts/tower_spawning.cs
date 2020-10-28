using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tower_spawning : MonoBehaviour
{
    public GameObject the_prefab, tower_2, tower_3;
    public GameObject tower_placement_UI;
    public bool already = false;

    GameController cont;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        tower_placement_UI = cont.towerUI;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!already)
            {
                tower_placement_UI.SetActive(true);
                cont.UpdatePos(this);
            }
        }
    }

    public void turnOffUI()
    {
        tower_placement_UI.SetActive(false);
    }

    void check_tower()
    {

        Instantiate(the_prefab, transform.position, Quaternion.identity);
        already = true;
        tower_placement_UI.SetActive(false);
    }
}
