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
        currentSkinIndex++;  // Incr�menter l'indice pour passer au skin suivant
        if (currentSkinIndex >= skins.Length)  // Si on d�passe le tableau
        {
            currentSkinIndex = 0;  // Revenir au premier skin
        }
        ChangeSkin(skins[currentSkinIndex]);  // Appliquer le skin suivant
    }

    public void PreviousSkin()
    {
        currentSkinIndex--;  // D�cr�menter l'indice pour revenir au skin pr�c�dent
        if (currentSkinIndex < 0)  // Si l'indice devient inf�rieur � 0
        {
            currentSkinIndex = skins.Length - 1;  // Revenir au dernier skin
        }
        ChangeSkin(skins[currentSkinIndex]);  // Appliquer le skin pr�c�dent
    }

    public void NextTorso()
    {
        currentTorsoIndex++;  // Incr�menter l'indice pour passer au torse suivant
        if (currentTorsoIndex >= torsoSkins.Length)  // Si on d�passe le tableau
        {
            currentTorsoIndex = 0;  // Revenir au premier torse
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);  // Appliquer le torse suivant
    }

    // M�thodes pour passer aux jambes suivantes (fl�che droite)
    public void NextLegs()
    {
        currentLegsIndex++;  // Incr�menter l'indice pour passer aux jambes suivantes
        if (currentLegsIndex >= legsSkins.Length)  // Si on d�passe le tableau
        {
            currentLegsIndex = 0;  // Revenir aux premi�res jambes
        }
        ChangeLegs(legsSkins[currentLegsIndex]);  // Appliquer les jambes suivantes
    }

    // M�thodes pour revenir au torse pr�c�dent (fl�che gauche)
    public void PreviousTorso()
    {
        currentTorsoIndex--;  // D�cr�menter l'indice pour revenir au torse pr�c�dent
        if (currentTorsoIndex < 0)  // Si l'indice devient inf�rieur � 0
        {
            currentTorsoIndex = torsoSkins.Length - 1;  // Revenir au dernier torse
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);  // Appliquer le torse pr�c�dent
    }

    // M�thodes pour revenir aux jambes pr�c�dentes (fl�che gauche)
    public void PreviousLegs()
    {
        currentLegsIndex--;  // D�cr�menter l'indice pour revenir aux jambes pr�c�dentes
        if (currentLegsIndex < 0)  // Si l'indice devient inf�rieur � 0
        {
            currentLegsIndex = legsSkins.Length - 1;  // Revenir aux derni�res jambes
        }
        ChangeLegs(legsSkins[currentLegsIndex]);  // Appliquer les jambes pr�c�dentes
    }
}

