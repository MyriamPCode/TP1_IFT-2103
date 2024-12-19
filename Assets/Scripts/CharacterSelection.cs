using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public GameObject character1;  // GameObject pour le personnage 1
    public GameObject character2;

    public SpriteLibraryAsset[] characterSkins;  // index 0 pour le personnage 1, index 1 pour le personnage 2

    // Index du personnage actuel
    private int currentCharacterIndex = 0;  // 0 pour personnage 1, 1 pour personnage 2

    // Référence à la SpriteLibrary du personnage
    private SpriteLibrary spriteLibrary;

    // Références aux boutons gauche et droite
    public Button leftButton;
    public Button rightButton;

    public Image[] charactersPlayer2;
    public Image[] characterImagesPlayer2;
    public Button leftButtonPlayer2;
    public Button rightButtonPlayer2;
    private int selectedCharacterIndexPlayer2 = 0;
    private Animator animatorPlayer2;

    void Start()
    {
        // Initialiser la SpriteLibrary
        spriteLibrary = GetComponent<SpriteLibrary>();

        // Mettre à jour l'affichage du personnage au début
        UpdateCharacter();

        // Ajouter des écouteurs pour les boutons gauche et droite
        leftButton.onClick.AddListener(OnLeftArrowClicked);
        rightButton.onClick.AddListener(OnRightArrowClicked);

        selectedCharacterIndexPlayer2 = PlayerPrefs.GetInt("SelectedCharacterPlayer2", 0);

        if (selectedCharacterIndexPlayer2 < 0 || selectedCharacterIndexPlayer2 >= charactersPlayer2.Length)
        {
            selectedCharacterIndexPlayer2 = 0;
        }

        animatorPlayer2 = charactersPlayer2[selectedCharacterIndexPlayer2].GetComponent<Animator>();

        UpdateCharacterDisplay();

        leftButtonPlayer2.onClick.AddListener(() => PreviousCharacter(2));
        rightButtonPlayer2.onClick.AddListener(() => NextCharacter(2));
    }

    // Fonction appelée lorsque la flèche gauche est cliquée
    public void OnLeftArrowClicked()
    {
        currentCharacterIndex = (currentCharacterIndex - 1 + characterSkins.Length) % characterSkins.Length;  // Passe au personnage précédent
        UpdateCharacter();
    }

    // Fonction appelée lorsque la flèche droite est cliquée
    public void OnRightArrowClicked()
    {
        currentCharacterIndex = (currentCharacterIndex + 1) % characterSkins.Length;  // Passe au personnage suivant
        UpdateCharacter();
    }

    // Fonction pour mettre à jour le personnage selon l'index actuel
    void UpdateCharacter()
    {
        // Vérification des GameObjects (personnages)
        if (character1 == null || character2 == null)
        {
            Debug.LogError("Les GameObjects des personnages ne sont pas assignés !");
            return;
        }

        // Vérification des SpriteLibraryAssets
        if (characterSkins == null || characterSkins.Length == 0)
        {
            Debug.LogError("Les SpriteLibraryAssets ne sont pas assignées !");
            return;
        }

        // Assurer que spriteLibrary est bien initialisé
        SpriteLibrary spriteLibrary = character1.GetComponent<SpriteLibrary>();
        if (spriteLibrary == null)
        {
            Debug.LogError("Le SpriteLibrary n'est pas attaché au personnage !");
            return;
        }

        // Affecter la nouvelle SpriteLibraryAsset
        spriteLibrary.spriteLibraryAsset = characterSkins[currentCharacterIndex];

        // Vérification de la partie du corps à changer (exemple avec le torse)
        SpriteResolver spriteResolver = character1.GetComponent<SpriteResolver>();
        /*
        if (spriteResolver != null)
        {
            spriteResolver.ResolveSpriteToSpriteRenderer("Torso");  // Assurez-vous que "Torso" est un label valide
        }
        else
        {
            Debug.LogError("Le SpriteResolver pour le personnage n'est pas attaché !");
        }*/

        // Activer ou désactiver les personnages
        if (currentCharacterIndex == 0)
        {
            character1.SetActive(true);
            character2.SetActive(false);
        }
        else
        {
            character1.SetActive(false);
            character2.SetActive(true);
        }
    }

    public void NextCharacter(int player)
    {
        if (player == 2)
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
        if (player == 2)
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

        PlayerPrefs.SetInt("SelectedCharacterPlayer2", selectedCharacterIndexPlayer2);
        PlayerPrefs.Save();

        SetCharacterAnimatorController(2, selectedCharacterIndexPlayer2);
    }

    void SetCharacterAnimatorController(int player, int characterIndex)
    {
        Animator characterAnimator = null;

        if (player == 2)
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
            else
            {
                Debug.LogWarning("Animator component missing on player 2 character " + selectedCharacterIndexPlayer2);
            }
        }
    }
}

