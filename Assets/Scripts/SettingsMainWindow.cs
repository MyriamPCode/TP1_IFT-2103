using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMainWindow : MonoBehaviour
{
    public static SettingsMainWindow Instance { get; private set; }

    private void Start()
    {
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenSettings()
    {
        gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        SaveSettings();
        gameObject.SetActive(false);
    }

    

    public void SaveSettings()
    {
    }
}

