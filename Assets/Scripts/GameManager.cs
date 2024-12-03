using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characters; // Liste des mod�les de personnages

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par d�faut, 0

        // V�rification si l'index est dans la bonne plage
        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characters.Length)
        {
            selectedCharacterIndex = 0;  // R�initialiser � 0 si l'index est invalide
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  // Sauvegarder l'index valide
            PlayerPrefs.Save();
        }

        // D�sactiver tous les personnages
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Activer le personnage s�lectionn�
        characters[selectedCharacterIndex].SetActive(true);
    }
}
