using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;
using System;


public class CharacterCustomization : MonoBehaviour
{

    public SpriteResolver legsResolver;

    public SpriteLibraryAsset[] skins;
    public SpriteLibraryAsset[] torsoSkins;
    public SpriteLibraryAsset[] legsSkins;

    private int currentSkinIndex = 0;  // L'indice du skin actuel dans le tableau
    private int currentTorsoIndex = 0; // L'indice du torse actuel
    private int currentLegsIndex = 0;

    public void SetPremierPersonnage()
    {
        ChangeSkin(skins[0]);
    }

    public void SetDeuxiemePersonnage()
    {
        ChangeSkin(skins[1]);
    }

    public void SetTorsoSkin()
    {
        
        ChangeTorso(torsoSkins[2]);
        
    }

    public void SetLegsSkin()
    {        
        ChangeLegs(legsSkins[2]);
       
    }

    public void ChangeSkin(SpriteLibraryAsset skin)
    {
        GetComponent<SpriteLibrary>().spriteLibraryAsset = skin;
    }

    public void ChangeLegs(SpriteLibraryAsset legsSkins)
    {
        GetComponent<SpriteLibrary>().spriteLibraryAsset = legsSkins;
    }

    public void ChangeTorso(SpriteLibraryAsset torsoSkins)
    {
        GetComponent<SpriteLibrary>().spriteLibraryAsset = torsoSkins;
    }

    /*
    public void SetLegsSkin(int index)
    {
        if (index >= 0 && index < legsSkins.Length)
        {
            legsResolver.spriteLibraryAsset = legsSkins[index];
        }
        else
        {
            Debug.LogWarning("Index de skin de jambes invalide");
        }
    }
    */

    public void NextSkin()
    {
        currentSkinIndex++;  // Incrémenter l'indice pour passer au skin suivant
        if (currentSkinIndex >= skins.Length)  // Si on dépasse le tableau
        {
            currentSkinIndex = 0;  // Revenir au premier skin
        }
        ChangeSkin(skins[currentSkinIndex]);  // Appliquer le skin suivant
    }

    public void PreviousSkin()
    {
        currentSkinIndex--;  // Décrémenter l'indice pour revenir au skin précédent
        if (currentSkinIndex < 0)  // Si l'indice devient inférieur à 0
        {
            currentSkinIndex = skins.Length - 1;  // Revenir au dernier skin
        }
        ChangeSkin(skins[currentSkinIndex]);  // Appliquer le skin précédent
    }

    public void NextTorso()
    {
        currentTorsoIndex++;  // Incrémenter l'indice pour passer au torse suivant
        if (currentTorsoIndex >= torsoSkins.Length)  // Si on dépasse le tableau
        {
            currentTorsoIndex = 0;  // Revenir au premier torse
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);  // Appliquer le torse suivant
    }

    // Méthodes pour passer aux jambes suivantes (flèche droite)
    public void NextLegs()
    {
        currentLegsIndex++;  // Incrémenter l'indice pour passer aux jambes suivantes
        if (currentLegsIndex >= legsSkins.Length)  // Si on dépasse le tableau
        {
            currentLegsIndex = 0;  // Revenir aux premières jambes
        }
        ChangeLegs(legsSkins[currentLegsIndex]);  // Appliquer les jambes suivantes
    }

    // Méthodes pour revenir au torse précédent (flèche gauche)
    public void PreviousTorso()
    {
        currentTorsoIndex--;  // Décrémenter l'indice pour revenir au torse précédent
        if (currentTorsoIndex < 0)  // Si l'indice devient inférieur à 0
        {
            currentTorsoIndex = torsoSkins.Length - 1;  // Revenir au dernier torse
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);  // Appliquer le torse précédent
    }

    // Méthodes pour revenir aux jambes précédentes (flèche gauche)
    public void PreviousLegs()
    {
        currentLegsIndex--;  // Décrémenter l'indice pour revenir aux jambes précédentes
        if (currentLegsIndex < 0)  // Si l'indice devient inférieur à 0
        {
            currentLegsIndex = legsSkins.Length - 1;  // Revenir aux dernières jambes
        }
        ChangeLegs(legsSkins[currentLegsIndex]);  // Appliquer les jambes précédentes
    }
}

