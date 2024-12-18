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

    private void Start()
    {
        joueur1KeyboardLayoutDropdown.onValueChanged.AddListener(OnJoueur1KeyboardLayoutChanged);

        LoadKeyboardPreferences();
    }

    private void LoadKeyboardPreferences()
    {
        inputManager.LoadKeyboardPreferenceForPlayer(1);  

        joueur1KeyboardLayoutDropdown.value = (inputManager.currentLayoutForPlayer1 == KeyboardLayout.AZERTY) ? 0 : 1;
    }

    private void OnJoueur1KeyboardLayoutChanged(int index)
    {
        KeyboardLayout layout = (index == 0) ? KeyboardLayout.AZERTY : KeyboardLayout.QWERTY;
        InputManager.Instance.SwitchKeyboardLayoutForPlayer(layout, 1);
        InputManager.Instance.SaveKeyboardPreferenceForPlayer(1);
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenSettings()
    {
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
            return; 
        }

    
        for (int i = 0; i < inputManager.keyBindings.Count; i++)
        {
            KeyBinding binding = inputManager.keyBindings[i];
            string keyName = binding.key.ToString();  
            string playerLabel = (binding.playerID == 1) ? "Joueur 1" : "Joueur 2";  

            actionTexts[i].text = $"{keyName}";

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
    }


    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        int playerID = inputManager.keyBindings[index].playerID;

        InputManager.Instance.ReassignKey(actionName, playerID);

        SettingsWindow.Instance.UpdateKeyBindingsDisplay();
    }   
}

