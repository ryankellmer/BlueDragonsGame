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
    
   
        if(PauseUI.gameObject.activeInHierarchy == true || SoundOptionMenuUI.gameObject.activeInHierarchy == true) 
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
        else
        {
            Time.timeScale = 1;
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

   
}
