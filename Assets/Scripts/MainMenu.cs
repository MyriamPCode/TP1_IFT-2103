using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject subMenu;

    private void Start()
    {
        subMenu.SetActive(false);
    }

    public void ShowSubMenu()
    {
        subMenu.SetActive(true);
    }

    public void StartLocalGame()
    {
        SceneLoader.LoadScene("MenuLocal");
    }

    public void StartOnlineGame()
    {
        SceneLoader.LoadScene("MenuEnLigne");  // Charger la scène du menu en ligne
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
