using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PartieLocale : MonoBehaviour
{
    public string levelToLoad;
    public Dropdown controllerDropdown;
    public InputManager inputManager;

    private void Start()
    {
        if (inputManager == null)
        {
            Debug.LogError("InputManager n'est pas assigné dans l'inspecteur !");
        }
        else
        {
            Debug.Log("InputManager est bien assigné.");
        }



        // Si une préférence a déjà été enregistrée, chargez-la dans le Dropdown
        LoadKeyboardPreference();

        // Ajouter un listener pour capturer les changements du dropdown
        controllerDropdown.onValueChanged.AddListener(OnControllerChanged);
    }

    public void OnControllerChanged(int index)
    {
        // Enregistrez la préférence de clavier dans les PlayerPrefs
        string layout = (index == 0) ? "QWERTY" : "AZERTY";
        PlayerPrefs.SetString("KeyboardLayout", layout);
        PlayerPrefs.Save();

        // Mettez à jour l'InputManager pour appliquer le changement
        InputManager.Instance.SwitchKeyboardLayoutForPlayer(
            layout == "QWERTY" ? KeyboardLayout.QWERTY : KeyboardLayout.AZERTY, 1
        );
        InputManager.Instance.SwitchKeyboardLayoutForPlayer(
            layout == "QWERTY" ? KeyboardLayout.QWERTY : KeyboardLayout.AZERTY, 2
        );
    }

    private void LoadKeyboardPreference()
    {
        string layout = PlayerPrefs.GetString("KeyboardLayout", "QWERTY");
        int dropdownIndex = (layout == "QWERTY") ? 0 : 1; // 0 pour QWERTY, 1 pour AZERTY
        controllerDropdown.value = dropdownIndex;
    }

    public void StartGame()
    {
        SceneLoader.LoadScene(levelToLoad);
    }
    public void ReturnMenu()
    {
        SceneLoader.LoadScene("MainMenu");
    }
}
