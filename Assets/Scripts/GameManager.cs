using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters; 

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); 

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characters.Length)
        {
            selectedCharacterIndex = 0;  
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  
            PlayerPrefs.Save();
        }

        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[selectedCharacterIndex].SetActive(true);
    }
}
