using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public InputManager inputManager;
    public TMP_Text[] actionTexts;
    public Button[] changeKeyButtons;
    public int actionIndex;

    private void Start()
    {
        Debug.Log("SettingsMenu Start()");
    }

    private void Awake()
    {
        // Supprime la logique de DontDestroyOnLoad et Instance
        // Le script se comporte maintenant comme un comportement normal attaché à un GameObject dans la scène
        Debug.Log("SettingsMenu Awake()");
    }

    // Méthodes pour gérer votre fenêtre de paramètres
    public void OpenSettings()
    {
        if (inputManager == null)
        {
            Debug.LogError("inputManager est nul lors de l'ouverture des paramètres.");
            return;
        }

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
            Debug.LogError("Les tableaux actionTexts ou changeKeyButtons ne sont pas correctement configurés. Vérifiez leurs tailles.");
            return;
        }

        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            KeyBinding binding = inputManager.keyBindings[i];
            string keyName = binding.key.ToString();
            string playerLabel = (binding.playerID == 1) ? "Joueur 1" : "Joueur 2"; 

            actionTexts[i].text = $"{playerLabel} - {binding.actionName} ({keyName})";

            int index = i;
            changeKeyButtons[i].onClick.RemoveAllListeners();
            changeKeyButtons[i].onClick.AddListener(() => OnChangeKeyButtonClick(binding.actionName, index));
        }
    }

    public void ChangeKeyBinding(string action, KeyCode newKey)
    {
        foreach (var binding in inputManager.keyBindings)
        {
            if (binding.actionName == action)
            {
                binding.key = newKey;
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
        PlayerPrefs.Save();
        Debug.Log("Paramètres sauvegardés");
    }

    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        int playerID = inputManager.keyBindings[index].playerID;
        InputManager.Instance.ReassignKey(actionName, playerID);
        UpdateKeyBindingsDisplay();
    }
}
