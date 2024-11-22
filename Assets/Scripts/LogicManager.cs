using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public Player player;
    public GameObject victoryScreen;
    //public TextMeshProUGUI victoryText;
    public GameObject settingsWindowPrefab;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
    public void restartGame()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void mainMenu()
    {
        Resume();
        SceneLoader.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Victory()
    {

        if (player != null)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.moveSpeed = 0f;
            player.jumpForce = 0f;
        }
        
        victoryScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonSettings()
{
    if (SettingsWindow.Instance != null)
    {
        SettingsWindow.Instance.OpenSettings();
    }
    else
    {
        Debug.LogWarning("SettingsWindow n'est pas encore instancié. Création de l'instance...");
        
        // Recherche d'un prefab SettingsWindow
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

}
