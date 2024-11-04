using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance { get; private set; }
    public InputManager inputManager;
    public TMP_Text[] actionTexts;
    public Button[] changeKeyButtons;
    public int actionIndex;

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

        //Debug.Log("OpenSettings() appel�, �tat avant activation : " + gameObject.activeSelf);
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

    public void UpdateKeyBindingsDisplay()
    {
        if (inputManager.keyBindings.Count > actionTexts.Length || inputManager.keyBindings.Count > changeKeyButtons.Length)
        {
            Debug.LogError("Les tableaux actionTexts ou changeKeyButtons ne sont pas correctement configur�s. V�rifiez leurs tailles.");
            return; // Sortir de la m�thode si les tailles ne correspondent pas
        }

        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            actionTexts[i].text = $"{inputManager.keyBindings[i].actionName}: {inputManager.keyBindings[i].key}";

            // Capturer l'index dans une variable locale
            int index = i;
            changeKeyButtons[i].onClick.RemoveAllListeners(); // Supprimez les anciens listeners
            changeKeyButtons[i].onClick.AddListener(() => OnChangeKeyButtonClick(inputManager.keyBindings[index].actionName));
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
        InputManager.Instance.ReassignKey(actionName);
        InputManager.Instance.SaveKeyBindings();
    }
}

