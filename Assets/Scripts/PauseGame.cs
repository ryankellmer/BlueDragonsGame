using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;

    
    
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

        if (pauseMenu.gameObject.activeInHierarchy == true)
        {
            
            
            if(gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                pauseMenu.SetActive(true);
            }




            else
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;

            }


        }    
    
    }

    public void UnpauseGame() 
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    
    
    
    }


}
