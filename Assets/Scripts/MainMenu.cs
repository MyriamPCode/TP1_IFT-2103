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

    public void StartOnlineGame()
    {
        SceneLoader.LoadScene("MenuEnLigne");
    }

    public void OpenSettings()
    {
        SceneLoader.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
