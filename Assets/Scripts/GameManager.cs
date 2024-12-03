using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters; // Liste des modèles de personnages

    void Start()
    {
        // Charger l'index du personnage choisi
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par défaut, 0

        // Désactiver tous les personnages
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Activer le personnage sélectionné
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characters.Length)
        {
            characters[selectedCharacterIndex].SetActive(true); // Active le personnage choisi
        }
    }
}
