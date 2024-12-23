using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerByName : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "HPlayer1")
        {
            Debug.Log("Player 1 has entered the door.");
            LoadPlayerScene("HPlayer1");
        }
        else if (other.gameObject.name == "HPlayer2")
        {
            Debug.Log("Player 2 has entered the door.");
            LoadPlayerScene("HPlayer2");
        }
    }

    private void LoadPlayerScene(string playerName)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "SecondScene")
        {
            if (playerName == "HPlayer1")
            {
                SceneManager.LoadScene("Main Scene");
            }
            else if (playerName == "HPlayer2")
            {
                SceneManager.LoadScene("Main Scene");
            }
        }
        else if (currentSceneName == "Online SecondScene")
        {
            if (playerName == "HPlayer1")
            {
                SceneManager.LoadScene("Online Main Scene");
            }
            else if (playerName == "HPlayer2")
            {
                SceneManager.LoadScene("Online Main Scene");
            }
        }
    }
}
