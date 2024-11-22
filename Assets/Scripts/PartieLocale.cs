using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartieLocale : MonoBehaviour
{
    public string levelToLoad;
    public string levelToLoad2;

    public void StartGame()
    {
        SceneLoader.LoadScene(levelToLoad);
    }
    public void ReturnMenu()
    {
        SceneLoader.LoadScene(levelToLoad2);
    }
}
