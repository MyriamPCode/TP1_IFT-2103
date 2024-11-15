using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public LoadingManager loadingManager;

    public void StartGame()
    {
        loadingManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsGame()
    {
        SceneManager.LoadScene("Settings");
    }

}
