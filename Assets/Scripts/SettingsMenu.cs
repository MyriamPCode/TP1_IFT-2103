using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public InputManager inputManager;
    public Button[] changeKeyButtons;

    private void Start()
    {
        
    }

    public void OpenSettings()
    {
        UpdateKeyBindingsDisplay();
    }

    public void CloseSettings()
    {
        SaveSettings();
        InputManager.Instance.SaveKeyBindings();
    }

    public void UpdateKeyBindingsDisplay()
    {
        
        /*
        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            actionTexts[i].text = $"{inputManager.keyBindings[i].actionName}: {inputManager.keyBindings[i].key}";

            // Capturer l'index dans une variable locale
            int index = i;
            changeKeyButtons[i].onClick.RemoveAllListeners(); // Supprimez les anciens listeners
            changeKeyButtons[i].onClick.AddListener(() => OnChangeKeyButtonClick(inputManager.keyBindings[index].actionName));
        }
        */
        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            KeyBinding binding = inputManager.keyBindings[i];
            string keyName = binding.key.ToString();  // Le nom de la touche associ�e � l'action
            string playerLabel = (binding.playerID == 1) ? "Joueur 1" : "Joueur 2";  // Affiche quel joueur utilise cette touche

            // Capturer l'index dans une variable locale pour l'utiliser dans le listener
            int index = i;
            changeKeyButtons[i].onClick.RemoveAllListeners();  // Supprimer les anciens listeners
            changeKeyButtons[i].onClick.AddListener(() => OnChangeKeyButtonClick(binding.actionName, index));
        }
    }

    public void ChangeKeyBinding(string action, KeyCode newKey)
    {
        foreach (var binding in inputManager.keyBindings)
        {
            if (binding.actionName == action)
            {
                binding.key = newKey; // Change la touche associ�e
                Debug.Log($"Changement de la touche pour {action} en {newKey}");
                break;
            }
        }
    }

    public void SaveSettings()
    {
        foreach (var binding in inputManager.keyBindings)
        {
            PlayerPrefs.SetString(binding.actionName, binding.key.ToString());
        }
        PlayerPrefs.Save(); // Sauvegarde les param�tres
        Debug.Log("Param�tres sauvegard�s");
    }

    /*
    public void OnChangeKeyButtonClick(string actionName)
    {
        InputManager.Instance.ReassignKey(actionName);
        InputManager.Instance.SaveKeyBindings();
    }
    */

    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        int playerID = inputManager.keyBindings[index].playerID;

        // Appeler ReassignKey en passant actionName et playerID
        InputManager.Instance.ReassignKey(actionName, playerID);

        // Mettre � jour l'affichage des touches
        //SettingsWindow.Instance.UpdateKeyBindingsDisplay();
    }
}

