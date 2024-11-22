using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerByName : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player1")
        {
            Debug.Log("Player 1 has entered the door.");
            LoadPlayerScene("Player1");
        }
        else if (other.gameObject.name == "Player2")
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
            if (playerName == "Player1")
            {
                SceneManager.LoadScene("Main Scene");
            }
            else if (playerName == "Player2")
            {
                SceneManager.LoadScene("Main Scene");
            }
        }
        else if (currentSceneName == "Online SecondScene")
        {
            if (playerName == "Player1")
            {
                SceneManager.LoadScene("Online Main Scene");
            }
            else if (playerName == "Player2")
            {
                SceneManager.LoadScene("Online Main Scene");
            }
        }
    }
}
