using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public Image[] characterImages;  // Tableau de toutes les images des personnages
    private int selectedCharacterIndex = 0;  // L'index du personnage s�lectionn�

    // R�f�rences aux boutons gauche et droit
    public Button leftButton;
    public Button rightButton;

    private bool isSwitchingCharacter = false;

    void Start()
    {
        // Charger le personnage s�lectionn� (si un choix a �t� sauvegard�)
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par d�faut 0 si aucun choix

        // D�sactiver toutes les images sauf celle du personnage s�lectionn�
        UpdateCharacterDisplay();

        // Ajouter les �couteurs pour les boutons
        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
    }

    // Fonction pour aller au personnage suivant (fl�che droite)
    public void NextCharacter()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characterImages.Length)
        {
            selectedCharacterIndex = 0; // Revenir au premier personnage si on d�passe
        }
        UpdateCharacterDisplay();
    }

    // Fonction pour aller au personnage pr�c�dent (fl�che gauche)
    public void PreviousCharacter()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterImages.Length - 1; // Revenir au dernier personnage si on descend
        }
        UpdateCharacterDisplay();
    }

    // Mettre � jour l'affichage du personnage
    void UpdateCharacterDisplay()
    {
        // D�sactiver toutes les images de personnages
        foreach (Image img in characterImages)
        {
            img.gameObject.SetActive(false);
        }

        // Activer l'image du personnage s�lectionn�
        characterImages[selectedCharacterIndex].gameObject.SetActive(true);

        // Sauvegarder la s�lection du personnage
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save(); // Sauvegarder imm�diatement
    }
}
