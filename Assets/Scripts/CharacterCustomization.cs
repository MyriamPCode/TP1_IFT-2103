using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public Image avatarImage;  // L'image UI représentant l'avatar
    private Material avatarMaterial;

    public Color playerSkinColor = Color.white;
    public Color playerClothesColor = Color.blue;
    public Color playerHairColor = Color.black;

    void Start()
    {
        // Assurez-vous que l'image a un matériau appliqué avec le shader
        avatarMaterial = avatarImage.material;
        LoadColors();
        ApplyColors();
    }

    public void ApplyColors()
    {
        // Appliquez les couleurs sur le matériau
        avatarMaterial.SetColor("_SkinColor", playerSkinColor);
        avatarMaterial.SetColor("_ClothesColor", playerClothesColor);
        avatarMaterial.SetColor("_HairColor", playerHairColor);
    }

    public void ResetColors()
    {
        playerSkinColor = Color.white;        // Remet la couleur de peau par défaut (blanc)
        playerClothesColor = Color.blue;      // Remet la couleur des vêtements par défaut (bleu)
        playerHairColor = Color.black;        // Remet la couleur des cheveux par défaut (noir)

        ApplyColors();  // Applique les couleurs par défaut au matériau
    }

    public void ChangeSkinColor(Color newSkinColor)
    {
        playerSkinColor = newSkinColor;
        ApplyColors();
        SaveColors();
    }

    public void ChangeClothesColor(Color newClothesColor)
    {
        playerClothesColor = newClothesColor;
        ApplyColors();
        SaveColors();
    }

    public void ChangeHairColor(Color newHairColor)
    {
        playerHairColor = newHairColor;
        ApplyColors();
        SaveColors();
    }

    public void SetSkinColorToRed()
    {
        ChangeSkinColor(Color.red);
    }

    public void SetClothesColorToGreen()
    {
        ChangeClothesColor(Color.green);
    }

    public void SetHairColorToBlue()
    {
        ChangeHairColor(Color.blue);
    }

    void SaveColors()
    {
        PlayerPrefs.SetString("SkinColor", ColorUtility.ToHtmlStringRGBA(playerSkinColor));
        PlayerPrefs.SetString("ClothesColor", ColorUtility.ToHtmlStringRGBA(playerClothesColor));
        PlayerPrefs.SetString("HairColor", ColorUtility.ToHtmlStringRGBA(playerHairColor));
        PlayerPrefs.Save();
    }

    void LoadColors()
    {
        if (PlayerPrefs.HasKey("SkinColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("SkinColor"), out playerSkinColor);
        }
        if (PlayerPrefs.HasKey("ClothesColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("ClothesColor"), out playerClothesColor);
        }
        if (PlayerPrefs.HasKey("HairColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("HairColor"), out playerHairColor);
        }
    }
}

