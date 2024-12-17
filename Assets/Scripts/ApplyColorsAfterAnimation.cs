using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColorsAfterAnimation : MonoBehaviour
{
    public CharacterCustomization characterCustomization;  // R�f�rence au script CharacterCustomization

    // Cette m�thode sera appel�e apr�s chaque animation
    public void OnAnimationEnd()
    {
        characterCustomization.ApplyColors();  // R�applique les couleurs de personnalisation
    }
}
