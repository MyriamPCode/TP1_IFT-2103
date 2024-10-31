using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    public GameObject settingsWindowPrefab;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        Debug.Log("Tentative d'ouverture de la fenêtre de paramètres");
        if (SettingsWindow.Instance != null)
        {
            Debug.Log("L'instance de SettingsWindow est valide");
            SettingsWindow.Instance.OpenSettings();
        }
        else
        {
            GameObject settingsWindowInstance = Instantiate(settingsWindowPrefab);
            DontDestroyOnLoad(settingsWindowInstance);
            Debug.LogWarning("SettingsWindow.Instance est null");
        }
    }

    public void CloseSettingsWindow()
    {
        if (SettingsWindow.Instance != null)
        {
            SettingsWindow.Instance.CloseSettings(); 
        }
    }
}
