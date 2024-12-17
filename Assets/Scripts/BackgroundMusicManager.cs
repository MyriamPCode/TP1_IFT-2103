using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;   // 场景名称
        public AudioClip musicClip; // 对应的背景音乐
    }

    public List<SceneMusic> sceneMusicList = new List<SceneMusic>(); // 场景和音乐列表
    private Dictionary<string, AudioClip> sceneMusicDict; // 字典用于快速查找
    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton 防止重复实例化
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持该对象在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 销毁重复的实例
            return;
        }

        // 确保 AudioSource 存在
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        // 初始化音乐字典
        sceneMusicDict = new Dictionary<string, AudioClip>();
        foreach (var sceneMusic in sceneMusicList)
        {
            if (!sceneMusicDict.ContainsKey(sceneMusic.sceneName))
            {
                sceneMusicDict.Add(sceneMusic.sceneName, sceneMusic.musicClip);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
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

        // 查找当前场景对应的音乐
        if (sceneMusicDict.TryGetValue(currentSceneName, out AudioClip musicClip))
        {
            if (audioSource.clip != musicClip) // 如果音乐不同，则切换音乐
            {
                audioSource.clip = musicClip;
                audioSource.Play();
                Debug.Log($"Playing music for scene: {currentSceneName}");
            }
        }
        else
        {
            Debug.LogWarning($"No background music assigned for scene: {currentSceneName}");
            audioSource.Stop(); // 停止播放
        }
    }
}
