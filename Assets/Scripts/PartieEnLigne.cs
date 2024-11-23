using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartieEnLigne : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.LoadScene("Online Main Scene");
    }
    public void ReturnMenu()
    {
        SceneLoader.LoadScene("MainMenu");
    }
}
