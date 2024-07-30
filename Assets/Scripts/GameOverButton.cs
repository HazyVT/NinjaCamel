using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void Home()
    {

        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }


}
