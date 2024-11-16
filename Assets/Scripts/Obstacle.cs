using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PixelManPlant_10 : MonoBehaviour
{
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

    private bool player1HasEntered = false;  // 玩家 1 是否触发过场景切换
    private bool player2HasEntered = false;  // 玩家 2 是否触发过场景切换

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);

        // 如果是玩家 1 并且玩家 1 没有触发过场景
        if (other.gameObject.name == "Player" && !player1HasEntered)
        {
            player1HasEntered = true;
            Debug.Log("Player 1 triggered the block. Loading next scene...");
            SceneManager.LoadScene("SecondScene"); // 切换场景
        }
        // 如果是玩家 2 并且玩家 2 没有触发过场景
        else if (other.gameObject.name == "Player2" && !player2HasEntered)
        {
            player2HasEntered = true;
            Debug.Log("Player 2 triggered the block. Loading next scene...");
            SceneManager.LoadScene("SecondScene"); // 切换场景
        }
    }
}

