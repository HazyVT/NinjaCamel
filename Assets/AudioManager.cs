using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
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

    private void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            PlayGameplayMusic();
        }
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true; // Loop the gameplay music
        audioSource.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the scene loaded event
    }
}
