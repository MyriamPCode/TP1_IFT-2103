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
    public Transform torsoTransform;

    public Slider positionSliderX;  // Slider pour la position X des jambes ou du torse
    public Slider positionSliderY;  // Slider pour la position Y des jambes ou du torse
    public Slider scaleSliderX;     // Slider pour l'�chelle X des jambes ou du torse
    public Slider scaleSliderY;     // Slider pour l'�chelle Y des jambes ou du torse
    public Slider rotationSlider;   // Slider pour la rotation autour de l'axe Y (ou un autre axe)

    public float maxPositionValue = 5f;
    public float maxRotationValue = 360f;
    public float maxScaleValue = 2f;

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
        float positionX = positionSliderX.value; // Lire la valeur du slider pour X
        float positionY = positionSliderY.value; // Lire la valeur du slider pour Y
        legsTransform.position = new Vector3(positionX, positionY, legsTransform.position.z); // Appliquer la nouvelle position
    }

    // D�finir l'�chelle des jambes
    public void SetLegsScale()
    {
        float scaleX = scaleSliderX.value; // Lire la valeur du slider pour l'�chelle X
        float scaleY = scaleSliderY.value; // Lire la valeur du slider pour l'�chelle Y
        legsTransform.localScale = new Vector3(scaleX, scaleY, legsTransform.localScale.z); // Appliquer la nouvelle �chelle
    }

    // D�finir la rotation des jambes
    public void SetLegsRotation()
    {
        float rotationY = rotationSlider.value; // Lire la valeur du slider pour la rotation Y
        legsTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Appliquer la rotation
    }

    // R�p�ter les m�mes m�thodes pour le torse
    public void SetTorsoPosition()
    {
        float positionX = positionSliderX.value; // Lire la valeur du slider pour X
        float positionY = positionSliderY.value; // Lire la valeur du slider pour Y
        torsoTransform.position = new Vector3(positionX, positionY, torsoTransform.position.z); // Appliquer la nouvelle position
    }

    public void SetTorsoScale()
    {
        float scaleX = scaleSliderX.value; // Lire la valeur du slider pour l'�chelle X
        float scaleY = scaleSliderY.value; // Lire la valeur du slider pour l'�chelle Y
        torsoTransform.localScale = new Vector3(scaleX, scaleY, torsoTransform.localScale.z); // Appliquer la nouvelle �chelle
    }

    public void SetTorsoRotation()
    {
        float rotationY = rotationSlider.value; // Lire la valeur du slider pour la rotation Y
        torsoTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Appliquer la rotation
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

        // Attacher les m�thodes aux sliders pour qu'elles soient appel�es � chaque modification
        positionSliderX.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        positionSliderY.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        scaleSliderX.onValueChanged.AddListener(delegate { SetLegsScale(); });
        scaleSliderY.onValueChanged.AddListener(delegate { SetLegsScale(); });
        rotationSlider.onValueChanged.AddListener(delegate { SetLegsRotation(); });

        // R�p�ter l'attachement des m�thodes pour le torse
        positionSliderX.onValueChanged.AddListener(delegate { SetTorsoPosition(); });
        positionSliderY.onValueChanged.AddListener(delegate { SetTorsoPosition(); });
        scaleSliderX.onValueChanged.AddListener(delegate { SetTorsoScale(); });
        scaleSliderY.onValueChanged.AddListener(delegate { SetTorsoScale(); });
        rotationSlider.onValueChanged.AddListener(delegate { SetTorsoRotation(); });

        LoadCustomization();
    }
        public void SaveCustomization()
        {
            // Sauvegarder les valeurs de position, �chelle, rotation et skins
            customizationData.legsPositionX = legsTransform.position.x;
            customizationData.legsPositionY = legsTransform.position.y;
            customizationData.torsoPositionX = torsoTransform.position.x;
            customizationData.torsoPositionY = torsoTransform.position.y;

            customizationData.legsScaleX = legsTransform.localScale.x;
            customizationData.legsScaleY = legsTransform.localScale.y;
            customizationData.torsoScaleX = torsoTransform.localScale.x;
            customizationData.torsoScaleY = torsoTransform.localScale.y;

            customizationData.legsRotation = legsTransform.rotation.eulerAngles.y;
            customizationData.torsoRotation = torsoTransform.rotation.eulerAngles.y;

            customizationData.skinIndex = currentSkinIndex;
            customizationData.torsoSkinIndex = currentTorsoIndex;
            customizationData.legsSkinIndex = currentLegsIndex;

            // Sauvegarder dans le PlayerPrefs (si tu veux le faire avec PlayerPrefs)
            PlayerPrefs.SetInt("LegsPositionX", Mathf.FloorToInt(customizationData.legsPositionX));
            PlayerPrefs.SetInt("LegsPositionY", Mathf.FloorToInt(customizationData.legsPositionY));
            PlayerPrefs.SetInt("TorsoPositionX", Mathf.FloorToInt(customizationData.torsoPositionX));
            PlayerPrefs.SetInt("TorsoPositionY", Mathf.FloorToInt(customizationData.torsoPositionY));
            PlayerPrefs.SetInt("LegsScaleX", Mathf.FloorToInt(customizationData.legsScaleX * 100));
            PlayerPrefs.SetInt("LegsScaleY", Mathf.FloorToInt(customizationData.legsScaleY * 100));
            PlayerPrefs.SetInt("TorsoScaleX", Mathf.FloorToInt(customizationData.torsoScaleX * 100));
            PlayerPrefs.SetInt("TorsoScaleY", Mathf.FloorToInt(customizationData.torsoScaleY * 100));
            PlayerPrefs.SetInt("LegsRotation", Mathf.FloorToInt(customizationData.legsRotation));
            PlayerPrefs.SetInt("TorsoRotation", Mathf.FloorToInt(customizationData.torsoRotation));
            PlayerPrefs.SetInt("SkinIndex", customizationData.skinIndex);
            PlayerPrefs.SetInt("TorsoSkinIndex", customizationData.torsoSkinIndex);
            PlayerPrefs.SetInt("LegsSkinIndex", customizationData.legsSkinIndex);
        }

        public void LoadCustomization()
        {
            // Charger les valeurs sauvegard�es si elles existent
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

            // Appliquer les valeurs charg�es
            legsTransform.position = new Vector3(customizationData.legsPositionX, customizationData.legsPositionY, legsTransform.position.z);
            torsoTransform.position = new Vector3(customizationData.torsoPositionX, customizationData.torsoPositionY, torsoTransform.position.z);
            legsTransform.localScale = new Vector3(customizationData.legsScaleX, customizationData.legsScaleY, legsTransform.localScale.z);
            torsoTransform.localScale = new Vector3(customizationData.torsoScaleX, customizationData.torsoScaleY, torsoTransform.localScale.z);
            legsTransform.rotation = Quaternion.Euler(0f, customizationData.legsRotation, 0f);
            torsoTransform.rotation = Quaternion.Euler(0f, customizationData.torsoRotation, 0f);

            // Appliquer les skins
            ChangeSkin(skins[customizationData.skinIndex]);
            ChangeTorso(torsoSkins[customizationData.torsoSkinIndex]);
            ChangeLegs(legsSkins[customizationData.legsSkinIndex]);
        }

    public void OnSaveButtonClicked()
    {
        SaveCustomization();
    }

}
