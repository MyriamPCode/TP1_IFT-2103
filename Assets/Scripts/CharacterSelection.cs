using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public Image[] characterImages;  // Tableau de toutes les images des personnages
    private int selectedCharacterIndex = 0;  // L'index du personnage sélectionné

    // Références aux boutons gauche et droit
    public Button leftButton;
    public Button rightButton;

    private bool isSwitchingCharacter = false;

    void Start()
    {
        // Charger le personnage sélectionné (si un choix a été sauvegardé)
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Par défaut 0 si aucun choix

        // Désactiver toutes les images sauf celle du personnage sélectionné
        UpdateCharacterDisplay();

        // Ajouter les écouteurs pour les boutons
        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
    }

    // Fonction pour aller au personnage suivant (flèche droite)
    public void NextCharacter()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characterImages.Length)
        {
            selectedCharacterIndex = 0; // Revenir au premier personnage si on dépasse
        }
        UpdateCharacterDisplay();
    }

    // Fonction pour aller au personnage précédent (flèche gauche)
    public void PreviousCharacter()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterImages.Length - 1; // Revenir au dernier personnage si on descend
        }
        UpdateCharacterDisplay();
    }

    // Mettre à jour l'affichage du personnage
    void UpdateCharacterDisplay()
    {
        // Désactiver toutes les images de personnages
        foreach (Image img in characterImages)
        {
            img.gameObject.SetActive(false);
        }

        // Activer l'image du personnage sélectionné
        characterImages[selectedCharacterIndex].gameObject.SetActive(true);

        // Sauvegarder la sélection du personnage
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save(); // Sauvegarder immédiatement
    }
}
