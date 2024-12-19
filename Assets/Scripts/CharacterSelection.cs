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
    private Animator animator;

    public Image[] charactersPlayer2;
    public Image[] characterImagesPlayer2;
    public Button leftButtonPlayer2;
    public Button rightButtonPlayer2;
    private int selectedCharacterIndexPlayer2 = 0;
    private Animator animatorPlayer2;

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        selectedCharacterIndexPlayer2 = PlayerPrefs.GetInt("SelectedCharacterPlayer2", 0);

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characters.Length)
        {
            selectedCharacterIndex = 0;
        }
        if (selectedCharacterIndexPlayer2 < 0 || selectedCharacterIndexPlayer2 >= charactersPlayer2.Length)
        {
            selectedCharacterIndexPlayer2 = 0;
        }

        animator = characters[selectedCharacterIndex].GetComponent<Animator>();
        animatorPlayer2 = charactersPlayer2[selectedCharacterIndexPlayer2].GetComponent<Animator>();

        UpdateCharacterDisplay();

        leftButton.onClick.AddListener(() => PreviousCharacter(1));
        rightButton.onClick.AddListener(() => NextCharacter(1));
        leftButtonPlayer2.onClick.AddListener(() => PreviousCharacter(2));
        rightButtonPlayer2.onClick.AddListener(() => NextCharacter(2));
    }

    public void NextCharacter(int player)
    {
        if (player == 1)
        {
            selectedCharacterIndex++;
            if (selectedCharacterIndex >= characters.Length)
            {
                selectedCharacterIndex = 0;
            }
        }
        else if (player == 2)
        {
            selectedCharacterIndexPlayer2++;
            if (selectedCharacterIndexPlayer2 >= charactersPlayer2.Length)
            {
                selectedCharacterIndexPlayer2 = 0;
            }
        }
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter(int player)
    {
        if (player == 1)
        {
            selectedCharacterIndex--;
            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex = characters.Length - 1;
            }
        }
        else if (player == 2)
        {
            selectedCharacterIndexPlayer2--;
            if (selectedCharacterIndexPlayer2 < 0)
            {
                selectedCharacterIndexPlayer2 = charactersPlayer2.Length - 1;
            }
        }
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        //Joueur 1
        foreach (Image img in characters)
        {
            img.gameObject.SetActive(false);
        }

        characters[selectedCharacterIndex].gameObject.SetActive(true);

        foreach (Image img in characterImages)
        {
            img.gameObject.SetActive(false);
        }

        characterImages[selectedCharacterIndex].gameObject.SetActive(true);

        //Joueur 2
        foreach (Image img in charactersPlayer2)
        {
            img.gameObject.SetActive(false);
        }
        charactersPlayer2[selectedCharacterIndexPlayer2].gameObject.SetActive(true);

        foreach (Image img in characterImagesPlayer2)
        {
            img.gameObject.SetActive(false);
        }
        characterImagesPlayer2[selectedCharacterIndexPlayer2].gameObject.SetActive(true);

        // Sauvegarder l'index du personnage sélectionné
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.SetInt("SelectedCharacterPlayer2", selectedCharacterIndexPlayer2);
        PlayerPrefs.Save();

        SetCharacterAnimatorController(1, selectedCharacterIndex);
        SetCharacterAnimatorController(2, selectedCharacterIndexPlayer2);
    }

    void SetCharacterAnimatorController(int player, int characterIndex)
    {
        Animator characterAnimator = null;

        if (player == 1)
        {
            characterAnimator = characters[selectedCharacterIndex].GetComponent<Animator>();
            if (characterAnimator != null)
            {
                switch (characterIndex)
                {
                    case 0:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Roi_Animations");
                        break;
                    case 1:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PersoVert_Animations");
                        break;
                    case 2:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Chauve_Animations");
                        break;
                    default:
                        break;
                }
            }
        }
        else if (player == 2)
        {
            characterAnimator = charactersPlayer2[selectedCharacterIndexPlayer2].GetComponent<Animator>();
            if (characterAnimator != null)
            {
                switch (characterIndex)
                {
                    case 0:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Roi_Animations");
                        break;
                    case 1:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PersoVert_Animations");
                        break;
                    case 2:
                        characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Chauve_Animations");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

