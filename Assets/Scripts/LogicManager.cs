using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject victoryScreen;
    public AudioClip victorySound;
    public AudioSource audioSource;
    public ParticleSystem fireworks;

    public GameObject settingsWindowPrefab;

    public GameObject player1HUD;
    public GameObject player2HUD;
    private GameObject player1;
    private GameObject player2;

    [System.Serializable]
    public class PlayerConfig
    {
        public GameObject playerPrefab;
    }

    public PlayerConfig player1Config;
    public PlayerConfig player2Config;

    private GameObject player1Instance;
    private GameObject player2Instance;

    

    private void Start()
    {
        InitializePlayers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void InitializePlayers()
    {
        player1Instance = Instantiate(player1Config.playerPrefab);
        player1 = player1Instance;
        
        player2Instance = Instantiate(player2Config.playerPrefab);
        player2 = player2Instance;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Victory()
    {
        Debug.Log("Victoire !");

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Aucun écran de victoire assigné !");
        }

        DisablePlayerHUDs();
        DisablePlayerMovement();
        PlayFireworks();
        PlayVictorySound();
    }

    private void DisablePlayerHUDs()
    {
        if (player1HUD != null) player1HUD.SetActive(false);
        if (player2HUD != null) player2HUD.SetActive(false);
    }

    private void DisablePlayerMovement()
    {
        if (player1 != null)
        {
            Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
            if (rb1 != null)
            {
                rb1.velocity = Vector2.zero;
                rb1.isKinematic = true;
            }
        }

        if (player2 != null)
        {
            Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
            {
                rb2.velocity = Vector2.zero;
                rb2.isKinematic = true;
            }
        }
    }

    private void PlayFireworks()
    {
        if (fireworks != null)
        {
            fireworks.Play();
        }
        else
        {
            Debug.LogWarning("Aucun système de particules de feux d'artifice assigné !");
        }
    }

    private void PlayVictorySound()
    {
        if (victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
        else
        {
            Debug.LogWarning("Aucun son de victoire assigné !");
        }
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

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
