using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject victoryScreen;

    public GameObject settingsWindowPrefab;

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
        DisablePlayerMovement(player1Instance);
        DisablePlayerMovement(player2Instance);
        victoryScreen.SetActive(true);
    }

    private void DisablePlayerMovement(GameObject player)
    {
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        Player playerScript1 = player.GetComponent<Player>();
        if (playerScript1 != null)
        {
            playerScript1.moveSpeed = 0f;
            playerScript1.jumpForce = 0f;
        }
        Player playerScript2 = player.GetComponent<Player>();
        if (playerScript2 != null)
        {
            playerScript2.moveSpeed = 0f;
            playerScript2.jumpForce = 0f;
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
