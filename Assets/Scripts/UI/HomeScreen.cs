using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public AudioManager audioManager;

    public void LoadSampleScene()
    {
        if (audioManager != null)
        {
            audioManager.PlayTransitionMusic();
        }
        else
        {
            Debug.LogError("AudioManager is not assigned in the HomeScreen script.");
        }
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

}
