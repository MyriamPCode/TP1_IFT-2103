using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;
using System;


public class CharacterCustomization : MonoBehaviour
{
    public CharacterCustomizationData customizationData;

    public SpriteLibraryAsset[] skins;
    public SpriteLibraryAsset[] torsoSkins;
    public SpriteLibraryAsset[] legsSkins;

    private int currentSkinIndex = 0;
    private int currentTorsoIndex = 0;
    private int currentLegsIndex = 0;

    public Transform legsTransform;
    public Transform armsTransform;

    public Slider armsPositionSliderX;  // Slider pour la position X des bras
    public Slider armsPositionSliderY;  // Slider pour la position Y des bras
    public Slider armsScaleSliderX;     // Slider pour l'�chelle X des bras
    public Slider armsScaleSliderY;     // Slider pour l'�chelle Y des bras
    public Slider armsRotationSlider;

    public Slider positionSliderX;  // Slider pour la position X des jambes 
    public Slider positionSliderY;  // Slider pour la position Y des jambes 
    public Slider scaleSliderX;     // Slider pour l'�chelle X des jambes 
    public Slider scaleSliderY;     // Slider pour l'�chelle Y des jambes 
    public Slider rotationSlider;   

    public float maxPositionValue = 5f;
    public float maxRotationValue = 360f;
    public float maxScaleValue = 2f;

    public GameObject head;
    public GameObject torso;
    public GameObject arms;
    public GameObject legs;

    public Sprite headSprite;
    public Sprite torsoSprite;
    public Sprite armsSprite;
    public Sprite legsSprite;

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


    public void NextSkin()
    {
        currentSkinIndex++;
        if (currentSkinIndex >= skins.Length)
        {
            currentSkinIndex = 0;
        }
        ChangeSkin(skins[currentSkinIndex]);
    }

    public void PreviousSkin()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skins.Length - 1;
        }
        ChangeSkin(skins[currentSkinIndex]);
    }

    public void NextTorso()
    {
        currentTorsoIndex++;
        if (currentTorsoIndex >= torsoSkins.Length)
        {
            currentTorsoIndex = 0;
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);
    }

    public void NextLegs()
    {
        currentLegsIndex++;
        if (currentLegsIndex >= legsSkins.Length)
        {
            currentLegsIndex = 0;
        }
        ChangeLegs(legsSkins[currentLegsIndex]);
    }

    public void PreviousTorso()
    {
        currentTorsoIndex--;
        if (currentTorsoIndex < 0)
        {
            currentTorsoIndex = torsoSkins.Length - 1;
        }
        ChangeTorso(torsoSkins[currentTorsoIndex]);
    }

    public void PreviousLegs()
    {
        currentLegsIndex--;
        if (currentLegsIndex < 0)
        {
            currentLegsIndex = legsSkins.Length - 1;
        }
        ChangeLegs(legsSkins[currentLegsIndex]);
    }

    public void SetLegsPosition()
    {
        float positionX = positionSliderX.value; 
        float positionY = positionSliderY.value; 
        legsTransform.position = new Vector3(positionX, positionY, legsTransform.position.z); 
    }

    public void SetLegsScale()
    {
        float scaleX = scaleSliderX.value; 
        float scaleY = scaleSliderY.value; 
        legsTransform.localScale = new Vector3(scaleX, scaleY, legsTransform.localScale.z); 
    }

    public void SetLegsRotation()
    {
        float rotationY = rotationSlider.value; 
        legsTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); 
    }

    public void SetArmsPosition()
    {
        float positionX = armsPositionSliderX.value; 
        float positionY = armsPositionSliderY.value; 
        armsTransform.position = new Vector3(positionX, positionY, armsTransform.position.z);
    }

    public void SetArmsScale()
    {
        float scaleX = armsScaleSliderX.value; 
        float scaleY = armsScaleSliderY.value; 
        armsTransform.localScale = new Vector3(scaleX, scaleY, armsTransform.localScale.z); 
    }

    public void SetArmsRotation()
    {
        float rotationY = armsRotationSlider.value; 
        armsTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); 
    }

    void Start()
    {

        positionSliderX.minValue = -maxPositionValue;
        positionSliderX.maxValue = maxPositionValue;
        positionSliderY.minValue = -maxPositionValue;
        positionSliderY.maxValue = maxPositionValue;

        scaleSliderX.minValue = 0f;
        scaleSliderX.maxValue = maxScaleValue;
        scaleSliderY.minValue = 0f;
        scaleSliderY.maxValue = maxScaleValue;

        rotationSlider.minValue = 0f;
        rotationSlider.maxValue = maxRotationValue;

        armsScaleSliderX.minValue = 0f;
        armsScaleSliderX.maxValue = 2f;  
        armsScaleSliderY.minValue = 0f;
        armsScaleSliderY.maxValue = 2f;

        positionSliderX.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        positionSliderY.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        scaleSliderX.onValueChanged.AddListener(delegate { SetLegsScale(); });
        scaleSliderY.onValueChanged.AddListener(delegate { SetLegsScale(); });
        rotationSlider.onValueChanged.AddListener(delegate { SetLegsRotation(); });

        armsPositionSliderX.onValueChanged.AddListener(delegate { SetArmsPosition(); });
        armsPositionSliderY.onValueChanged.AddListener(delegate { SetArmsPosition(); });
        armsScaleSliderX.onValueChanged.AddListener(delegate { SetArmsScale(); });
        armsScaleSliderY.onValueChanged.AddListener(delegate { SetArmsScale(); });
        armsRotationSlider.onValueChanged.AddListener(delegate { SetArmsRotation(); });

        head.GetComponent<SpriteRenderer>().sprite = headSprite;
        torso.GetComponent<SpriteRenderer>().sprite = torsoSprite;
        arms.GetComponent<SpriteRenderer>().sprite = armsSprite;
        legs.GetComponent<SpriteRenderer>().sprite = legsSprite;

        LoadCustomization();
    }
        public void SaveCustomization()
        {
            customizationData.legsPositionX = legsTransform.position.x;
            customizationData.legsPositionY = legsTransform.position.y;
            
            customizationData.legsScaleX = legsTransform.localScale.x;
            customizationData.legsScaleY = legsTransform.localScale.y;


            customizationData.legsRotation = legsTransform.rotation.eulerAngles.y;

            customizationData.skinIndex = currentSkinIndex;
            customizationData.torsoSkinIndex = currentTorsoIndex;
            customizationData.legsSkinIndex = currentLegsIndex;

        customizationData.armsPositionX = armsTransform.position.x;
        customizationData.armsPositionY = armsTransform.position.y;

        customizationData.armsScaleX = armsTransform.localScale.x;
        customizationData.armsScaleY = armsTransform.localScale.y;

        customizationData.armsRotation = armsTransform.rotation.eulerAngles.y;

        PlayerPrefs.SetInt("ArmsPositionX", Mathf.FloorToInt(customizationData.armsPositionX));
        PlayerPrefs.SetInt("ArmsPositionY", Mathf.FloorToInt(customizationData.armsPositionY));
        PlayerPrefs.SetInt("ArmsScaleX", Mathf.FloorToInt(customizationData.armsScaleX * 100));
        PlayerPrefs.SetInt("ArmsScaleY", Mathf.FloorToInt(customizationData.armsScaleY * 100));
        PlayerPrefs.SetInt("ArmsRotation", Mathf.FloorToInt(customizationData.armsRotation));

        PlayerPrefs.SetInt("LegsPositionX", Mathf.FloorToInt(customizationData.legsPositionX));
            PlayerPrefs.SetInt("LegsPositionY", Mathf.FloorToInt(customizationData.legsPositionY));
            PlayerPrefs.SetInt("LegsScaleX", Mathf.FloorToInt(customizationData.legsScaleX * 100));
            PlayerPrefs.SetInt("LegsScaleY", Mathf.FloorToInt(customizationData.legsScaleY * 100));
            PlayerPrefs.SetInt("LegsRotation", Mathf.FloorToInt(customizationData.legsRotation));
            PlayerPrefs.SetInt("SkinIndex", customizationData.skinIndex);
            PlayerPrefs.SetInt("LegsSkinIndex", customizationData.legsSkinIndex);

        }

        public void LoadCustomization()
        {
            if (PlayerPrefs.HasKey("LegsPositionX"))
            {
                customizationData.legsPositionX = PlayerPrefs.GetInt("LegsPositionX");
                customizationData.legsPositionY = PlayerPrefs.GetInt("LegsPositionY");
                customizationData.torsoPositionX = PlayerPrefs.GetInt("TorsoPositionX");
                customizationData.torsoPositionY = PlayerPrefs.GetInt("TorsoPositionY");
                customizationData.legsScaleX = PlayerPrefs.GetInt("LegsScaleX") / 100f;
                customizationData.legsScaleY = PlayerPrefs.GetInt("LegsScaleY") / 100f;
                customizationData.torsoScaleX = PlayerPrefs.GetInt("TorsoScaleX") / 100f;
                customizationData.torsoScaleY = PlayerPrefs.GetInt("TorsoScaleY") / 100f;
                customizationData.legsRotation = PlayerPrefs.GetInt("LegsRotation");
                customizationData.torsoRotation = PlayerPrefs.GetInt("TorsoRotation");
                customizationData.skinIndex = PlayerPrefs.GetInt("SkinIndex");
                customizationData.torsoSkinIndex = PlayerPrefs.GetInt("TorsoSkinIndex");
                customizationData.legsSkinIndex = PlayerPrefs.GetInt("LegsSkinIndex");
            }

        if (PlayerPrefs.HasKey("ArmsPositionX"))
        {
            customizationData.armsPositionX = PlayerPrefs.GetInt("ArmsPositionX");
            customizationData.armsPositionY = PlayerPrefs.GetInt("ArmsPositionY");
            customizationData.armsScaleX = PlayerPrefs.GetInt("ArmsScaleX") / 100f;
            customizationData.armsScaleY = PlayerPrefs.GetInt("ArmsScaleY") / 100f;
            customizationData.armsRotation = PlayerPrefs.GetInt("ArmsRotation");

            armsTransform.position = new Vector3(customizationData.armsPositionX, customizationData.armsPositionY, armsTransform.position.z);
            armsTransform.localScale = new Vector3(customizationData.armsScaleX, customizationData.armsScaleY, armsTransform.localScale.z);
            armsTransform.rotation = Quaternion.Euler(0f, customizationData.armsRotation, 0f);
        }

        legsTransform.position = new Vector3(customizationData.legsPositionX, customizationData.legsPositionY, legsTransform.position.z);
            legsTransform.localScale = new Vector3(customizationData.legsScaleX, customizationData.legsScaleY, legsTransform.localScale.z);
            legsTransform.rotation = Quaternion.Euler(0f, customizationData.legsRotation, 0f);

            ChangeSkin(skins[customizationData.skinIndex]);
            ChangeTorso(torsoSkins[customizationData.torsoSkinIndex]);
            ChangeLegs(legsSkins[customizationData.legsSkinIndex]);
        }

    public void OnSaveButtonClicked()
    {
        SaveCustomization();
    }

}
