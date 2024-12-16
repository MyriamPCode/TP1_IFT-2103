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
        ApplyColors();
    }

    void ApplyColors()
    {
        // Appliquez les couleurs sur le matériau
        avatarMaterial.SetColor("_SkinColor", playerSkinColor);
        avatarMaterial.SetColor("_ClothesColor", playerClothesColor);
        avatarMaterial.SetColor("_HairColor", playerHairColor);
    }

    public void ChangeSkinColor(Color newSkinColor)
    {
        playerSkinColor = newSkinColor;
        ApplyColors();
    }

    public void ChangeClothesColor(Color newClothesColor)
    {
        playerClothesColor = newClothesColor;
        ApplyColors();
    }

    public void ChangeHairColor(Color newHairColor)
    {
        playerHairColor = newHairColor;
        ApplyColors();
    }

    public void SetSkinColorToRed()
    {
        ChangeSkinColor(Color.red);
    }
}

