using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCharacterDisplay : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;  // Référence au SpriteRenderer du Player
    public Sprite[] characterSprites;  // Tableau des sprites des personnages

    private int selectedCharacterIndex;

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);  // Par défaut, 0 si aucun personnage n'est sélectionné

        // Assurez-vous que l'index est valide
        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characterSprites.Length)
        {
            selectedCharacterIndex = 0;  // Si l'index est invalide, réinitialisez-le à 0
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  // Sauvegardez le nouvel index valide
            PlayerPrefs.Save();
        }

        // Applique le sprite correspondant au joueur
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        if (characterSprites != null && characterSprites.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterSprites.Length)
        {
            if (playerSpriteRenderer != null)
            {
                playerSpriteRenderer.sprite = characterSprites[selectedCharacterIndex];
            }
            else
            {
                Debug.LogWarning("Le SpriteRenderer du joueur n'est pas assigné.");
            }
        }
        else
        {
            Debug.LogWarning("Le tableau des sprites de personnages est vide ou l'index est invalide.");
        }
    }
}
