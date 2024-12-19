using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;
using System;


public class CharacterCustomization : MonoBehaviour
{

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
    public Slider scaleSliderX;     // Slider pour l'échelle X des jambes ou du torse
    public Slider scaleSliderY;     // Slider pour l'échelle Y des jambes ou du torse
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

    // Définir l'échelle des jambes
    public void SetLegsScale()
    {
        float scaleX = scaleSliderX.value; // Lire la valeur du slider pour l'échelle X
        float scaleY = scaleSliderY.value; // Lire la valeur du slider pour l'échelle Y
        legsTransform.localScale = new Vector3(scaleX, scaleY, legsTransform.localScale.z); // Appliquer la nouvelle échelle
    }

    // Définir la rotation des jambes
    public void SetLegsRotation()
    {
        float rotationY = rotationSlider.value; // Lire la valeur du slider pour la rotation Y
        legsTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Appliquer la rotation
    }

    // Répéter les mêmes méthodes pour le torse
    public void SetTorsoPosition()
    {
        float positionX = positionSliderX.value; // Lire la valeur du slider pour X
        float positionY = positionSliderY.value; // Lire la valeur du slider pour Y
        torsoTransform.position = new Vector3(positionX, positionY, torsoTransform.position.z); // Appliquer la nouvelle position
    }

    public void SetTorsoScale()
    {
        float scaleX = scaleSliderX.value; // Lire la valeur du slider pour l'échelle X
        float scaleY = scaleSliderY.value; // Lire la valeur du slider pour l'échelle Y
        torsoTransform.localScale = new Vector3(scaleX, scaleY, torsoTransform.localScale.z); // Appliquer la nouvelle échelle
    }

    public void SetTorsoRotation()
    {
        float rotationY = rotationSlider.value; // Lire la valeur du slider pour la rotation Y
        torsoTransform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Appliquer la rotation
    }

    void Start()
    {
        // Initialiser les sliders avec des valeurs maximales et minimales
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

        // Attacher les méthodes aux sliders pour qu'elles soient appelées à chaque modification
        positionSliderX.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        positionSliderY.onValueChanged.AddListener(delegate { SetLegsPosition(); });
        scaleSliderX.onValueChanged.AddListener(delegate { SetLegsScale(); });
        scaleSliderY.onValueChanged.AddListener(delegate { SetLegsScale(); });
        rotationSlider.onValueChanged.AddListener(delegate { SetLegsRotation(); });

        // Répéter l'attachement des méthodes pour le torse
        positionSliderX.onValueChanged.AddListener(delegate { SetTorsoPosition(); });
        positionSliderY.onValueChanged.AddListener(delegate { SetTorsoPosition(); });
        scaleSliderX.onValueChanged.AddListener(delegate { SetTorsoScale(); });
        scaleSliderY.onValueChanged.AddListener(delegate { SetTorsoScale(); });
        rotationSlider.onValueChanged.AddListener(delegate { SetTorsoRotation(); });
    }
}

