using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCustomizationData", menuName = "Character/CustomizationData")]
public class CharacterCustomizationData : ScriptableObject
{
    public float legsPositionX;
    public float legsPositionY;
    public float torsoPositionX;
    public float torsoPositionY;

    public float legsScaleX;
    public float legsScaleY;
    public float torsoScaleX;
    public float torsoScaleY;

    public float legsRotation;
    public float torsoRotation;

    public int skinIndex;
    public int torsoSkinIndex;
    public int legsSkinIndex;
}