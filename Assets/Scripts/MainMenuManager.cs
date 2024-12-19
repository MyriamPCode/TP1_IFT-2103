using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button buttonLocal;
    public Button buttonJoin;
    public Button buttonHost;
    public GameObject settingsWindowPrefab;

    private void Start()
    {
        buttonLocal.gameObject.SetActive(false);
        buttonHost.gameObject.SetActive(false);
        buttonJoin.gameObject.SetActive(false);
        settingsWindowPrefab.gameObject.SetActive(false);
    }

    public void ShowSubMenu()
    {
        buttonLocal.gameObject.SetActive(true);
        buttonHost.gameObject.SetActive(true);
        buttonJoin.gameObject.SetActive(true);
    }

    public void StartLocalGame()
    {
        SceneLoader.LoadScene("MenuLocal");
    }

    public void ButtonSettings()
    {
        if (SettingsMainWindow.Instance != null)
        {
            SettingsMainWindow.Instance.OpenSettings();
        }
        else
        {
            Debug.LogWarning("SettingsWindow n'est pas encore instancié. Création de l'instance...");

            if (settingsWindowPrefab != null)
            {
                GameObject settingsWindow = Instantiate(settingsWindowPrefab);
                settingsWindow.SetActive(true);
            }
            else
            {
                Debug.LogError("settingsWindowPrefab n'est pas assigné !");
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
