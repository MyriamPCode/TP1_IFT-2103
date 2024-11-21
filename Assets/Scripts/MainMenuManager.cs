using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button buttonLocal;
    public Button buttonHost;
    public Button buttonJoin;

    private void Start()
    {
        buttonLocal.gameObject.SetActive(false);
        buttonHost.gameObject.SetActive(false);
        buttonJoin.gameObject.SetActive(false);
        buttonLocal.onClick.AddListener(() => StartLocalGame());
        buttonHost.onClick.AddListener(() => StartOnlineGame());
        buttonJoin.onClick.AddListener(() => StartOnlineGame());
    }

    public void ShowSubMenu()
    {
        buttonLocal.gameObject.SetActive(true);
        buttonHost.gameObject.SetActive(true);
        buttonJoin.gameObject.SetActive(true);
    }

    public void StartLocalGame()
    {
        Debug.Log("Chargement du menu local");
        SceneLoader.LoadScene("MenuLocal");
    }

    public void StartOnlineGame()
    {
        Debug.Log("Chargement du menu en ligne");
        SceneLoader.LoadScene("MenuEnLigne");
    }

    public void OpenSettings()
    {
        Debug.Log("Chargement des paramètres");
        SceneLoader.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
