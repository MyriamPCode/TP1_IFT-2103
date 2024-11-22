using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerByName : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player 1 has entered the door. Loading Player 1's scene...");
            SceneManager.LoadScene("Main Scene"); 
        }
        
        else if (other.gameObject.name == "Player2")
        {
            Debug.Log("Player 2 has entered the door. Loading Player 2's scene...");
            SceneManager.LoadScene("Main Scene"); 
        }
    }
}
