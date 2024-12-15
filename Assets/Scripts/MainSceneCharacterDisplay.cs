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

    public SpriteRenderer player2SpriteRenderer;  
    public Sprite[] player2Sprites;  
    public Animator player2Animator;  
    public RuntimeAnimatorController[] player2Animators;
    private int selectedCharacterIndexPlayer2;

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        selectedCharacterIndexPlayer2 = PlayerPrefs.GetInt("SelectedCharacterPlayer2", 0);

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characterSprites.Length)
        {
            selectedCharacterIndex = 0; 
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);  
            PlayerPrefs.Save();
        }

        if (selectedCharacterIndexPlayer2 < 0 || selectedCharacterIndexPlayer2 >= player2Sprites.Length)
        {
            selectedCharacterIndexPlayer2 = 0;  
            PlayerPrefs.SetInt("SelectedCharacterPlayer2", selectedCharacterIndexPlayer2);  
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

        if (player2Sprites != null && player2Sprites.Length > 0 && selectedCharacterIndexPlayer2 >= 0 && selectedCharacterIndexPlayer2 < player2Sprites.Length)
        {
            if (player2SpriteRenderer != null)
            {
                player2SpriteRenderer.sprite = player2Sprites[selectedCharacterIndexPlayer2];
            }

            if (player2Animator != null && player2Animators != null && player2Animators.Length > selectedCharacterIndexPlayer2)
            {
                player2Animator.runtimeAnimatorController = player2Animators[selectedCharacterIndexPlayer2];
            }
        }
    }
}
