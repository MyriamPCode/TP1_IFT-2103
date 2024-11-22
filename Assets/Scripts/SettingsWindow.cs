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

    public TMP_Dropdown joueur1KeyboardLayoutDropdown;
    public TMP_Dropdown joueur2KeyboardLayoutDropdown;

    private void Start()
    {
        Debug.Log($"SettingsWindow.Instance dans Start: {SettingsWindow.Instance}");
        joueur1KeyboardLayoutDropdown.onValueChanged.AddListener(OnJoueur1KeyboardLayoutChanged);
        joueur2KeyboardLayoutDropdown.onValueChanged.AddListener(OnJoueur2KeyboardLayoutChanged);

        LoadKeyboardPreferences();
    }

    private void LoadKeyboardPreferences()
    {
        if (inputManager == null)
        {
            Debug.LogError("inputManager est null dans LoadKeyboardPreferences!");
            return;
        }

        // V�rifier si les dropdowns sont assign�s
        if (joueur1KeyboardLayoutDropdown == null || joueur2KeyboardLayoutDropdown == null)
        {
            Debug.LogError("Les Dropdowns pour les claviers ne sont pas assign�s !");
            return;
        }

        // Charger les pr�f�rences de clavier pour chaque joueur
        inputManager.LoadKeyboardPreferenceForPlayer(1);  // Charge les pr�f�rences pour le joueur 1
        inputManager.LoadKeyboardPreferenceForPlayer(2);  // Charge les pr�f�rences pour le joueur 2

        // Affecter les valeurs aux dropdowns
        joueur1KeyboardLayoutDropdown.value = (inputManager.currentLayoutForPlayer1 == KeyboardLayout.AZERTY) ? 0 : 1;
        joueur2KeyboardLayoutDropdown.value = (inputManager.currentLayoutForPlayer2 == KeyboardLayout.AZERTY) ? 0 : 1;
    }

    private void OnJoueur1KeyboardLayoutChanged(int index)
    {
        KeyboardLayout layout = (index == 0) ? KeyboardLayout.AZERTY : KeyboardLayout.QWERTY;
        InputManager.Instance.SwitchKeyboardLayoutForPlayer(layout, 1);
        InputManager.Instance.SaveKeyboardPreferenceForPlayer(1);
    }

    private void OnJoueur2KeyboardLayoutChanged(int index)
    {
        KeyboardLayout layout = (index == 0) ? KeyboardLayout.AZERTY : KeyboardLayout.QWERTY;
        InputManager.Instance.SwitchKeyboardLayoutForPlayer(layout, 2);
        InputManager.Instance.SaveKeyboardPreferenceForPlayer(2);
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

        LoadKeyboardPreferences();
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
            //actionTexts[i].text = $"{playerLabel} - {binding.actionName} ({keyName})"; (j'ai modifié parce que c'était moche)
            actionTexts[i].text = $"{keyName}";

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

