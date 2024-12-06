using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public Image[] characterImages; 
    private int selectedCharacterIndex = 0; 

    public Button leftButton;
    public Button rightButton;

    private bool isSwitchingCharacter = false;

    private Animator animator;

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        animator = characters[selectedCharacterIndex].GetComponent<Animator>();

        UpdateCharacterDisplay();

        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
    }

    public void NextCharacter()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characterImages.Length)
        {
            selectedCharacterIndex = 0; 
        }
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterImages.Length - 1; 
        }
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[selectedCharacterIndex].SetActive(true);

        foreach (Image img in characterImages)
        {
            img.gameObject.SetActive(false);
        }

        characterImages[selectedCharacterIndex].gameObject.SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save(); 
    }

    void SetCharacterAnimatorController(int characterIndex)
    {
        // Charger et appliquer l'Animator Controller correspondant au personnage sélectionné
        switch (characterIndex)
        {
            case 0:
                animator.runtimeAnimatorController = Resources.Load("Character1_Animator") as RuntimeAnimatorController;
                break;
            case 1:
                animator.runtimeAnimatorController = Resources.Load("Character2_Animator") as RuntimeAnimatorController;
                break;
            // Ajoutez des cases pour d'autres personnages si nécessaire
            default:
                break;
        }
    }
}
