using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class upgradeButton : MonoBehaviour
{
    TowerController parent;
    Vector2 startPos;
    SpriteRenderer rend;
    GameController gc;
    public Text lvlText;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        rend = GetComponent<SpriteRenderer>();
        startPos = transform.localPosition;
        parent = transform.parent.GetComponent<TowerController>();
    }
    private void OnMouseDown()
    {
        GetComponentInParent<TowerController>().upgrade();
    }

    private void OnMouseOver()
    {
        rend.color = (gc.GetMoney() >= parent.towerCost) ? Color.green : Color.red;
    }

    private void OnMouseExit()
    {
        rend.color = Color.white;
    }

    private void LateUpdate()
    {
        int l = (int)parent.level + 1;
        lvlText.text = "Lvl " + l.ToString();
        //rend.color = (gc.GetMoney() >= parent.towerCost) ? Color.green : Color.red;
        transform.position = (Vector2)transform.parent.position + startPos;
        transform.rotation = Quaternion.identity;
    }
}
