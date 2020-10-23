using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   
    public GameObject PauseUI;
    public GameObject SoundOptionMenuUI;
    
    


    void Start() 
   {
        
        //UI.SetActive(false);
        
   
   }
    
  void Update() 
  { 
    
   
        if(PauseUI.gameObject.activeInHierarchy == true) 
        {
            Pause();
            if (SoundOptionMenuUI.gameObject.activeInHierarchy == true) 
            {
                Pause();
            }
        }
        
        
        
        else
        {
            Resume();
        }

        



  } 
    
    
   public void LoadScene(string SceneName)
   {
       SceneManager.LoadScene(SceneName);
   }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("ButtonPresed");
        Application.Quit();
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause() 
    {
        //PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
   
    public void Resume() 
    {
        Time.timeScale = 1;
    }
}
