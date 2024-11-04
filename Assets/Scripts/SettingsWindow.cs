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
            Debug.Log("SettingsWindow instance cr��e");
        }
        else
        {
            Destroy(gameObject); // D�truire les doublons
            Debug.LogWarning("Une autre instance de SettingsWindow a �t� d�truite");
        }
    }

    // M�thodes pour g�rer votre fen�tre de param�tres
    public void OpenSettings()
    {
        if (inputManager == null)
        {
            Debug.LogError("inputManager est nul lors de l'ouverture des param�tres.");
            return;
        }

        Debug.Log("OpenSettings() appel�, �tat avant activation : " + gameObject.activeSelf);
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
        Debug.Log("Mise � jour des mappages de touches");

        if (inputManager == null)
        {
            Debug.LogError("InputManager est nul !");
            return; // Quittez la m�thode si inputManager est nul
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
                    Debug.LogWarning($"Mappage de touche � l'indice {i} est nul.");
                }
            }
            else
            {
                Debug.LogWarning($"Indice {i} d�passe la taille du tableau actionTexts.");
            }
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

    public void OnChangeKeyButtonClick(string actionName)
    {
        InputManager.Instance.ReassignKey(actionName); // Appeler la m�thode de r�assignation
    }
}

