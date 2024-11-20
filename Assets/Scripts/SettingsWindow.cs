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

    public TMP_Dropdown keyboardLayoutDropdown;

    private void Start()
    {
        Debug.Log($"SettingsWindow.Instance dans Start: {SettingsWindow.Instance}");
        keyboardLayoutDropdown.onValueChanged.AddListener(OnKeyboardLayoutChanged);

        LoadKeyboardPreference();
    }

    private void LoadKeyboardPreference()
    {
        if (inputManager != null)
        {
            inputManager.LoadKeyboardPreference();  // Charger la pr�f�rence du clavier dans InputManager
        }
    }

    private void OnKeyboardLayoutChanged(int index)
    {
        // V�rifier l'index s�lectionn� et appliquer la disposition du clavier correspondante
        if (index == 0)  // 0 pour AZERTY
        {
            InputManager.Instance.SwitchKeyboardLayout(KeyboardLayout.AZERTY);
        }
        else  // 1 pour QWERTY
        {
            InputManager.Instance.SwitchKeyboardLayout(KeyboardLayout.QWERTY);
        }

        // Sauvegarder la pr�f�rence de disposition du clavier
        InputManager.Instance.SaveKeyboardPreference();
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
            KeyBinding binding = inputManager.keyBindings[i];
            string keyName = binding.key.ToString();  // Le nom de la touche associ�e � l'action
            string playerLabel = (binding.playerID == 1) ? "Joueur 1" : "Joueur 2";  // Affiche quel joueur utilise cette touche

            // Afficher le nom de l'action, le joueur et la touche associ�e
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


    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        int playerID = inputManager.keyBindings[index].playerID;

        // Appeler ReassignKey en passant actionName et playerID
        InputManager.Instance.ReassignKey(actionName, playerID);

        // Mettre � jour l'affichage des touches
        SettingsWindow.Instance.UpdateKeyBindingsDisplay();
    }
}

