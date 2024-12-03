using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters; // Liste des mod�les de personnages

    void Start()
    {
        // Charger l'index du personnage choisi
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par d�faut, 0

        // D�sactiver tous les personnages
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Activer le personnage s�lectionn�
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characters.Length)
        {
            characters[selectedCharacterIndex].SetActive(true); // Active le personnage choisi
        }
    }
}
