using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;

    public void UnpauseGame() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
