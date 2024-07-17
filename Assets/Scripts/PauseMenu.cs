using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject screenDim;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        screenDim.SetActive(true);
        Time.timeScale = 0;
    }


    public void Home()
    {

        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        screenDim.SetActive(false);
        Time.timeScale = 1;
    }

}