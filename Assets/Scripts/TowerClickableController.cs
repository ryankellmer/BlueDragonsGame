using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class TowerClickableController : MonoBehaviour
{
    public GameObject upgrade;
    //Draw circle indicating tower range when tower is clicked
    void OnMouseDown()
    {
        upgrade.SetActive(!upgrade.activeInHierarchy);
        transform.parent.gameObject.GetComponent<TowerController>().DoRenderer();
    }
}
