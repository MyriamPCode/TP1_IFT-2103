using TMPro;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance { get; private set; }
    public InputManager inputManager;
    public TMP_Text[] actionTexts;

    private void Start()
    {
        Debug.Log($"SettingsWindow.Instance dans Start: {SettingsWindow.Instance}");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
            Debug.Log("SettingsWindow instance créée");
        }
        else
        {
            Destroy(gameObject); // Détruire les doublons
            Debug.LogWarning("Une autre instance de SettingsWindow a été détruite");
        }
    }

    // Méthodes pour gérer votre fenêtre de paramètres
    public void OpenSettings()
    {
        if (inputManager == null)
        {
            Debug.LogError("inputManager est nul lors de l'ouverture des paramètres.");
            return;
        }

        Debug.Log("OpenSettings() appelé, état avant activation : " + gameObject.activeSelf);
        Debug.Log($"Nombre de mappages de touches : {inputManager.keyBindings.Count}");
        UpdateKeyBindingsDisplay();
        gameObject.SetActive(true);

    }

    public void CloseSettings()
    {
        SaveSettings();
        InputManager.Instance.SaveKeyBindings();
        gameObject.SetActive(false);
    }

    private void UpdateKeyBindingsDisplay()
    {
        Debug.Log("Mise à jour des mappages de touches");

        if (inputManager == null)
        {
            Debug.LogError("InputManager est nul !");
            return; // Quittez la méthode si inputManager est nul
        }

        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            if (i < actionTexts.Length)
            {
                if (inputManager.keyBindings[i] != null)
                {
                    actionTexts[i].text = $"{inputManager.keyBindings[i].actionName}: {inputManager.keyBindings[i].key}";
                }
                else
                {
                    Debug.LogWarning($"Mappage de touche à l'indice {i} est nul.");
                }
            }
            else
            {
                Debug.LogWarning($"Indice {i} dépasse la taille du tableau actionTexts.");
            }
        }
    }

    public void ChangeKeyBinding(string action, KeyCode newKey)
    {
        foreach (var binding in inputManager.keyBindings)
        {
            if (binding.actionName == action)
            {
                binding.key = newKey; // Change la touche associée
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
        PlayerPrefs.Save(); // Sauvegarde les paramètres
        Debug.Log("Paramètres sauvegardés");
    }

    public void OnChangeKeyButtonClick(string actionName)
    {
        InputManager.Instance.ReassignKey(actionName); // Appeler la méthode de réassignation
    }
}

