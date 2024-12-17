using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerByName : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Player 1 has entered the door.");
            LoadPlayerScene("Player1");
        }
        else if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Player 2 has entered the door.");
            LoadPlayerScene("Player2");
        }
    }

    private void LoadPlayerScene(string playerName)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "SecondScene")
        {
            SceneManager.LoadScene("Main Scene");
        }
        else if (currentSceneName == "Online SecondScene")
        {
            SceneManager.LoadScene("Online Main Scene");
        }
    }
}
