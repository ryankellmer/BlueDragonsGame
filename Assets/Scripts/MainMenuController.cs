using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   
    public GameObject PauseUI;
    public GameObject SoundOptionMenuUI;
    public bool gamePaused = false;
    


    void Start() 
   {
        
        //UI.SetActive(false);
        
   
   }
    
  void Update() 
  { 
        if (PauseUI != null && SoundOptionMenuUI != null) {
            if (PauseUI.gameObject.activeInHierarchy == true || SoundOptionMenuUI.gameObject.activeInHierarchy == true)
            {
                Time.timeScale = 0;

                /*if (gamePaused == false)
                {
                    gamePaused = true;
                    PauseUI.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    PauseUI.SetActive(false);
                    gamePaused = false;
                    Time.timeScale = 1;

                }*/
            }
        }
        else
        {
            //Time.timeScale = 1;
        }
  } 
    
    
   public void LoadScene(string SceneName)
   {
        Time.timeScale = 1f;
       SceneManager.LoadScene(SceneName);
   }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("ButtonPresed");
        Application.Quit();
    }

    public void RestartGame() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}
