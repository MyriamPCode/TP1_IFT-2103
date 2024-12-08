using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public Image[] characters;
    public Image[] characterImages; 
    private int selectedCharacterIndex = 0; 

    public Button leftButton;
    public Button rightButton;

    //private bool isSwitchingCharacter = false;

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
        foreach (Image img in characters)
        {
            img.gameObject.SetActive(false);
        }

        // Activer l'image du personnage sélectionné
        characters[selectedCharacterIndex].gameObject.SetActive(true);

        // Désactiver toutes les images d'affichage
        foreach (Image img in characterImages)
        {
            img.gameObject.SetActive(false);
        }

        // Activer l'image correspondante dans characterImages
        characterImages[selectedCharacterIndex].gameObject.SetActive(true);

        // Sauvegarder l'index du personnage sélectionné
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
    }

    void SetCharacterAnimatorController(int characterIndex)
    {
        switch (characterIndex)
        {
            case 0:
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Roi_Animations");
                break;
            case 1:
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PersoVert_Animations");
                break;
            case 2:
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Chauve_Animations");
                break;
            default:
                break;
        }
    }
}
