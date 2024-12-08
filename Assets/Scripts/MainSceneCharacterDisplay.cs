using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCharacterDisplay : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;  
    public Sprite[] characterSprites;
    public Animator playerAnimator;
    public RuntimeAnimatorController[] characterAnimators;

    private int selectedCharacterIndex;

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);  

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characterSprites.Length)
        {
            selectedCharacterIndex = 0; 
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  
            PlayerPrefs.Save();
        }

        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        if (characterSprites != null && characterSprites.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterSprites.Length)
        {
            // Mettre à jour le sprite du joueur
            if (playerSpriteRenderer != null)
            {
                playerSpriteRenderer.sprite = characterSprites[selectedCharacterIndex];
            }
            else
            {
                Debug.LogWarning("Le SpriteRenderer du joueur n'est pas assigné.");
            }

            // Mettre à jour l'Animator Controller du joueur
            if (playerAnimator != null && characterAnimators != null && characterAnimators.Length > selectedCharacterIndex)
            {
                playerAnimator.runtimeAnimatorController = characterAnimators[selectedCharacterIndex];
            }
            else
            {
                Debug.LogWarning("Le tableau des Animator Controllers est vide ou l'index est invalide.");
            }
        }
        else
        {
            Debug.LogWarning("Le tableau des sprites de personnages est vide ou l'index est invalide.");
        }
    }
}
