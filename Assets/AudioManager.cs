using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip mainMenuMusic;

    public AudioClip transitionMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensure the AudioManager persists across scenes
    }

    private void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.loop = true; // Loop the main menu music
        audioSource.Play();
    }

    public void PlayTransitionMusic()
    {
        audioSource.loop = false; // Do not loop the transition music
        audioSource.clip = transitionMusic;
        audioSource.Play();
        Invoke("LoadGameScene", transitionMusic.length);
    }
    
    public void StopMusic()
    {
        audioSource.Stop();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
