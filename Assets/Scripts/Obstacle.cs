using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PixelManPlant_10 : MonoBehaviour
{
    private bool player1HasEntered = false; 
    private bool player2HasEntered = false;  

    void Start()
    {
        BoxCollider2D physicalCollider = gameObject.AddComponent<BoxCollider2D>();
        physicalCollider.offset = new Vector2(0f, -0.003f);
        physicalCollider.size = new Vector2(0.09f, 0.098f);
        physicalCollider.isTrigger = false; 

        BoxCollider2D triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.offset = new Vector2(0f, -0.003f);
        triggerCollider.size = new Vector2(0.09f, 0.098f);
        triggerCollider.isTrigger = true; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);

        if (other.gameObject.name == "Player1" && !player1HasEntered)
        {
            player1HasEntered = true;
            LoadNextScene();
        }
        else if (other.gameObject.name == "Player2" && !player2HasEntered)
        {
            player2HasEntered = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Main Scene")
        {
            SceneManager.LoadScene("SecondScene");
        }
        else if (currentSceneName == "Online Main Scene")
        {
            SceneManager.LoadScene("Online SecondScene");
        }
    }
}
