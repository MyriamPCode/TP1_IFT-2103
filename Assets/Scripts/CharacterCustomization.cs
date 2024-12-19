using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;


public class CharacterCustomization : MonoBehaviour
{

    public SpriteResolver legsResolver;

    public SpriteLibraryAsset[] skins;
    public SpriteLibraryAsset[] torsoSkins;
    public SpriteLibraryAsset[] legsSkins;

    public void SetPremierPersonnage()
    {
        ChangeSkin(skins[0]);
    }

    public void SetDeuxiemePersonnage()
    {
        ChangeSkin(skins[1]);
    }

    public void SetTorsoSkin(int index)
    {
        ChangeLegs(torsoSkins[1]);
    }

    public void SetLegsSkin()
    {
        ChangeLegs(legsSkins[1]);
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
}

