using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;  
    public AudioClip otherMusic;    

    [Header("Scenes with Main Menu Music")]
    public string[] mainMenuScenes; 

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsMainMenuScene(scene.name))
        {
            PlayMusic(mainMenuMusic);
        }
        else
        {
            PlayMusic(otherMusic);
        }
    }

    private bool IsMainMenuScene(string sceneName)
    {
        foreach (string menuScene in mainMenuScenes)
        {
            if (sceneName == menuScene)
                return true;
        }
        return false;
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; 
        audioSource.clip = clip;
        audioSource.Play();
    }
}
