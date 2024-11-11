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

        //Debug.Log("OpenSettings() appelé, état avant activation : " + gameObject.activeSelf);
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
            return; // Sortir de la méthode si les tailles ne correspondent pas
        }

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
            string keyName = binding.key.ToString();  // Le nom de la touche associée à l'action
            string playerLabel = (binding.playerID == 1) ? "Joueur 1" : "Joueur 2";  // Affiche quel joueur utilise cette touche

            // Afficher le nom de l'action, le joueur et la touche associée
            actionTexts[i].text = $"{playerLabel} - {binding.actionName} ({keyName})";

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

    /*
    public void OnChangeKeyButtonClick(string actionName)
    {
        InputManager.Instance.ReassignKey(actionName);
        InputManager.Instance.SaveKeyBindings();
    }
    */

    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        InputManager.Instance.ReassignKey(actionName);  // Réassigner la touche pour l'action
        UpdateKeyBindingsDisplay();  // Mettre à jour l'affichage des touches
    }
}

