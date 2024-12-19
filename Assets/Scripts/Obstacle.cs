using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isTriggered = false;
    private bool player1HasEntered = false; 
    private bool player2HasEntered = false;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        if (other.gameObject.CompareTag("Respawn") && !player1HasEntered)
        {
            isTriggered = true;
            player1HasEntered = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            float delay = audioSource != null ? audioSource.clip.length : 1f;
            LoadNextScene();
        }
        
        else if (other.gameObject.CompareTag("Respawn") && !player2HasEntered)
        {
            isTriggered = true;
            player2HasEntered = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            float delay = audioSource != null ? audioSource.clip.length : 1f;
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
