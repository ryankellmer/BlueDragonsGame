using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_spawning : MonoBehaviour
{
    public GameObject the_prefab;
    bool already = false;

    private void OnMouseDown()
    {
        check_tower(); 
    }

    void check_tower()
    {
        if(already == false)
        {
            Instantiate(the_prefab, transform.position, Quaternion.identity);
            already = true; 
        }
    }
}
