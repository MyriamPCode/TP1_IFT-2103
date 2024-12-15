using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    // Joueur 1
    public SpriteRenderer player1SkinRenderer;
    public SpriteRenderer player1ClothesRenderer;
    public SpriteRenderer player1HairRenderer;

    public Color player1SkinColor = Color.white;
    public Color player1ClothesColor = Color.blue;
    public Color player1HairColor = Color.black;

    // Joueur 2
    public SpriteRenderer player2SkinRenderer;
    public SpriteRenderer player2ClothesRenderer;
    public SpriteRenderer player2HairRenderer;

    public Color player2SkinColor = Color.white;
    public Color player2ClothesColor = Color.blue;
    public Color player2HairColor = Color.black;

    // Boutons pour changer la couleur
    public Button player1ChangeSkinColorButton;
    public Button player1ChangeClothesColorButton;
    public Button player1ChangeHairColorButton;

    public Button player2ChangeSkinColorButton;
    public Button player2ChangeClothesColorButton;
    public Button player2ChangeHairColorButton;

    void Start()
    {
        // Attacher des événements de changement de couleur pour chaque joueur
        player1ChangeSkinColorButton.onClick.AddListener(ChangePlayer1SkinColor);
        player1ChangeClothesColorButton.onClick.AddListener(ChangePlayer1ClothesColor);
        player1ChangeHairColorButton.onClick.AddListener(ChangePlayer1HairColor);

        player2ChangeSkinColorButton.onClick.AddListener(ChangePlayer2SkinColor);
        player2ChangeClothesColorButton.onClick.AddListener(ChangePlayer2ClothesColor);
        player2ChangeHairColorButton.onClick.AddListener(ChangePlayer2HairColor);

        // Appliquer les couleurs initiales
        ApplyColors();
    }

    // Change les couleurs du joueur 1
    void ChangePlayer1SkinColor()
    {
        player1SkinColor = new Color(Random.value, Random.value, Random.value); // Exemple : couleur aléatoire
        ApplyColors();
    }

    void ChangePlayer1ClothesColor()
    {
        player1ClothesColor = new Color(Random.value, Random.value, Random.value);
        ApplyColors();
    }

    void ChangePlayer1HairColor()
    {
        player1HairColor = new Color(Random.value, Random.value, Random.value);
        ApplyColors();
    }

    // Change les couleurs du joueur 2
    void ChangePlayer2SkinColor()
    {
        player2SkinColor = new Color(Random.value, Random.value, Random.value);
        ApplyColors();
    }

    void ChangePlayer2ClothesColor()
    {
        player2ClothesColor = new Color(Random.value, Random.value, Random.value);
        ApplyColors();
    }

    void ChangePlayer2HairColor()
    {
        player2HairColor = new Color(Random.value, Random.value, Random.value);
        ApplyColors();
    }

    // Applique les couleurs aux deux joueurs
    void ApplyColors()
    {
        // Appliquer les couleurs au joueur 1
        if (player1SkinRenderer != null)
            player1SkinRenderer.color = player1SkinColor;
        if (player1ClothesRenderer != null)
            player1ClothesRenderer.color = player1ClothesColor;
        if (player1HairRenderer != null)
            player1HairRenderer.color = player1HairColor;

        // Appliquer les couleurs au joueur 2
        if (player2SkinRenderer != null)
            player2SkinRenderer.color = player2SkinColor;
        if (player2ClothesRenderer != null)
            player2ClothesRenderer.color = player2ClothesColor;
        if (player2HairRenderer != null)
            player2HairRenderer.color = player2HairColor;
    }

    void SaveCharacterColors()
    {
        // Sauvegarder les couleurs dans PlayerPrefs
        PlayerPrefs.SetFloat("Player1SkinR", player1SkinColor.r);
        PlayerPrefs.SetFloat("Player1SkinG", player1SkinColor.g);
        PlayerPrefs.SetFloat("Player1SkinB", player1SkinColor.b);

        PlayerPrefs.SetFloat("Player1ClothesR", player1ClothesColor.r);
        PlayerPrefs.SetFloat("Player1ClothesG", player1ClothesColor.g);
        PlayerPrefs.SetFloat("Player1ClothesB", player1ClothesColor.b);

        PlayerPrefs.SetFloat("Player1HairR", player1HairColor.r);
        PlayerPrefs.SetFloat("Player1HairG", player1HairColor.g);
        PlayerPrefs.SetFloat("Player1HairB", player1HairColor.b);

        PlayerPrefs.SetFloat("Player2SkinR", player2SkinColor.r);
        PlayerPrefs.SetFloat("Player2SkinG", player2SkinColor.g);
        PlayerPrefs.SetFloat("Player2SkinB", player2SkinColor.b);

        PlayerPrefs.SetFloat("Player2ClothesR", player2ClothesColor.r);
        PlayerPrefs.SetFloat("Player2ClothesG", player2ClothesColor.g);
        PlayerPrefs.SetFloat("Player2ClothesB", player2ClothesColor.b);

        PlayerPrefs.SetFloat("Player2HairR", player2HairColor.r);
        PlayerPrefs.SetFloat("Player2HairG", player2HairColor.g);
        PlayerPrefs.SetFloat("Player2HairB", player2HairColor.b);

        PlayerPrefs.Save();
    }

    void LoadCharacterColors()
    {
        player1SkinColor = new Color(PlayerPrefs.GetFloat("Player1SkinR", 1), PlayerPrefs.GetFloat("Player1SkinG", 1), PlayerPrefs.GetFloat("Player1SkinB", 1));
        player1ClothesColor = new Color(PlayerPrefs.GetFloat("Player1ClothesR", 1), PlayerPrefs.GetFloat("Player1ClothesG", 1), PlayerPrefs.GetFloat("Player1ClothesB", 1));
        player1HairColor = new Color(PlayerPrefs.GetFloat("Player1HairR", 1), PlayerPrefs.GetFloat("Player1HairG", 1), PlayerPrefs.GetFloat("Player1HairB", 1));

        player2SkinColor = new Color(PlayerPrefs.GetFloat("Player2SkinR", 1), PlayerPrefs.GetFloat("Player2SkinG", 1), PlayerPrefs.GetFloat("Player2SkinB", 1));
        player2ClothesColor = new Color(PlayerPrefs.GetFloat("Player2ClothesR", 1), PlayerPrefs.GetFloat("Player2ClothesG", 1), PlayerPrefs.GetFloat("Player2ClothesB", 1));
        player2HairColor = new Color(PlayerPrefs.GetFloat("Player2HairR", 1), PlayerPrefs.GetFloat("Player2HairG", 1), PlayerPrefs.GetFloat("Player2HairB", 1));

        ApplyColors();
    }
}
