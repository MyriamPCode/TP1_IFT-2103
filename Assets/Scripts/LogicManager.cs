using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject victoryScreen;

    public GameObject settingsWindowPrefab;

    private GameObject hplayer1;
    private GameObject hplayer2;
    private GameObject aiplayer1;
    private GameObject aiplayer2;

    // Configuration des joueurs
    [System.Serializable]
    public class PlayerConfig
    {
        public bool isHuman; // Si true, le joueur est humain, sinon c'est une IA.
        public GameObject playerPrefab; // Préfab à utiliser pour ce joueur.
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
        // Instanciation des joueurs en fonction de leur configuration
        if (player1Config.isHuman)
        {
            player1Instance = Instantiate(player1Config.playerPrefab);
            hplayer1 = player1Instance;
        }
        else
        {
            player1Instance = Instantiate(player1Config.playerPrefab);
            aiplayer1 = player1Instance;
        }

        if (player2Config.isHuman)
        {
            player2Instance = Instantiate(player2Config.playerPrefab);
            hplayer2 = player2Instance;
        }
        else
        {
            player2Instance = Instantiate(player2Config.playerPrefab);
            aiplayer2 = player2Instance;
        }
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

        HPlayer1 playerScript1 = player.GetComponent<HPlayer1>();
        if (playerScript1 != null)
        {
            playerScript1.moveSpeed = 0f;
            playerScript1.jumpForce = 0f;
        }
        HPlayer2 playerScript2 = player.GetComponent<HPlayer2>();
        if (playerScript2 != null)
        {
            playerScript2.moveSpeed = 0f;
            playerScript2.jumpForce = 0f;
        }

        AIPlayer1 aiScript1 = player.GetComponent<AIPlayer1>();
        if (aiScript1 != null)
        {
            aiScript1.moveSpeed = 0f;
            aiScript1.jumpForce = 0f;
        }
        AIPlayer2 aiScript2 = player.GetComponent<AIPlayer2>();
        if (aiScript2 != null)
        {
            aiScript2.moveSpeed = 0f;
            aiScript2.jumpForce = 0f;
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
