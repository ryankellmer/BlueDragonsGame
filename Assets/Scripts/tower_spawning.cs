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

    public static GameObject selectedTile; 
    Renderer rend;
    Color selectedColor = Color.black;
    Color unselectedColor = Color.white;
    bool clicked;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        tower_placement_UI = cont.towerUI;
        rend = GetComponent<Renderer>();
        clicked = false;
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
            if(clicked == false){
                TileSelection(gameObject);
                clicked = true;
            }
            else{
                rend.material.color = unselectedColor;
                clicked = false;
            }
        }
    }

    public void turnOffUI()
    {
        Destroy(this);
        tower_placement_UI.SetActive(false);
    }

    void check_tower()
    {

        Instantiate(the_prefab, transform.position, Quaternion.identity);
        already = true;
        tower_placement_UI.SetActive(false);
    }

    public void TileSelection(GameObject newTile){
        //checks if there was a tile selected previously, if there was, return tile to unselected color
        if(selectedTile != null){ 
            Renderer oldTile = selectedTile.GetComponent<Renderer>();
            oldTile.material.color = unselectedColor;
        }
        //Assign newly clicked tile to static selected tile, change color to selected color
        selectedTile = newTile;
        Renderer newTileRend = selectedTile.GetComponent<Renderer>();
        newTileRend.material.color = selectedColor; 
    }
}
