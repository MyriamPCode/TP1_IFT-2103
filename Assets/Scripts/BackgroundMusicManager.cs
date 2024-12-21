using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    public List<SceneMusic> sceneMusicList = new List<SceneMusic>();
    public AudioMixer audioMixer;
    private Dictionary<string, AudioClip> sceneMusicDict;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        sceneMusicDict = new Dictionary<string, AudioClip>();
        foreach (var sceneMusic in sceneMusicList)
        {
            if (!sceneMusicDict.ContainsKey(sceneMusic.sceneName))
            {
                sceneMusicDict.Add(sceneMusic.sceneName, sceneMusic.musicClip);
            }
        }

        AudioMixerGroup musicGroup = audioMixer.FindMatchingGroups("Music")[0];
        audioSource.outputAudioMixerGroup = musicGroup;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

   public void PlayMusicForCurrentScene()
{
    string currentSceneName = SceneManager.GetActiveScene().name;

    if (sceneMusicDict.TryGetValue(currentSceneName, out AudioClip musicClip))
    {
        if (audioSource.clip != musicClip)
        {
            StartCoroutine(CrossfadeToNewMusic(musicClip, 0.5f)); 
            Debug.Log($"Switching music for scene: {currentSceneName}");
        }
        else
        {
            Debug.Log($"Music for scene {currentSceneName} is already playing.");
        }
    }
    else
    {
        Debug.LogWarning($"No background music assigned for scene: {currentSceneName}");
    }
}


public System.Collections.IEnumerator CrossfadeToNewMusic(AudioClip newClip, float duration)
{
    float startVolume = audioSource.volume;

    // Fade out the current music
    for (float t = 0; t < duration; t += Time.deltaTime)
    {
        audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
        yield return null;
    }

    audioSource.volume = 0;
    audioSource.clip = newClip;
    audioSource.Play();

    // Fade in the new music
    for (float t = 0; t < duration; t += Time.deltaTime)
    {
        audioSource.volume = Mathf.Lerp(0, startVolume, t / duration);
        yield return null;
    }

    audioSource.volume = startVolume;
}

}
