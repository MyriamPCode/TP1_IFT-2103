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

    private void PlayMusicForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (sceneMusicDict.TryGetValue(currentSceneName, out AudioClip musicClip))
        {
            if (audioSource.clip != musicClip)
            {
                audioSource.clip = musicClip;
                audioSource.Play();
                Debug.Log($"Playing music for scene: {currentSceneName}");
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
}
