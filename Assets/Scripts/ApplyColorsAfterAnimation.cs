using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColorsAfterAnimation : MonoBehaviour
{
    public CharacterCustomization characterCustomization;  // Référence au script CharacterCustomization

    // Cette méthode sera appelée après chaque animation
    public void OnAnimationEnd()
    {
        characterCustomization.ApplyColors();  // Réapplique les couleurs de personnalisation
    }
}
