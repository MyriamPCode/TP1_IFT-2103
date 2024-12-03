using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters; // Liste des modèles de personnages

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par défaut, 0

        // Vérification si l'index est dans la bonne plage
        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characters.Length)
        {
            selectedCharacterIndex = 0;  // Réinitialiser à 0 si l'index est invalide
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  // Sauvegarder l'index valide
            PlayerPrefs.Save();
        }

        // Désactiver tous les personnages
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Activer le personnage sélectionné
        characters[selectedCharacterIndex].SetActive(true);
    }
}
