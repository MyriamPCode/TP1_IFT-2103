using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;


public class CharacterCustomization : MonoBehaviour
{
    public SpriteLibraryAsset[] skins;
    
    public void SetPremierPersonnage()
    {
        ChangeSkin(skins[0]);
    }

    public void SetDeuxiemePersonnage()
    {
        ChangeSkin(skins[1]);
    }

    public void ChangeSkin(SpriteLibraryAsset skin)
    {
        GetComponent<SpriteLibrary>().spriteLibraryAsset = skin;
    }
}

