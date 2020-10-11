using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    public GameObject Soundcanvas;
    private bool isShowing = true;


    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isShowing)
            {
                Soundcanvas.SetActive(false);
                isShowing = false;
            }
        }

        // Update is called once per frame
    }
}
