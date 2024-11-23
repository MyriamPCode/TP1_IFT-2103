using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance { get; private set; }
    public InputManager inputManager;
    public TMP_Text[] actionTexts;
    public Button[] changeKeyButtons;
    public int actionIndex;

    public Dropdown joueur1KeyboardLayoutDropdown;

    private void OnEnable()
    {
        // Abonnez-vous à l'événement de chargement de la scène
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Se désabonner de l'événement lorsque le script est désactivé
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cette méthode sera appelée à chaque fois qu'une scène est chargée
        if (InputManager.Instance == null)
        {
            Debug.LogError("L'instance de InputManager a été détruite lors du changement de scène !");
        }
        else
        {
            Debug.Log("InputManager est toujours présent.");
        }
    }

    private void Start()
    {
        Debug.Log($"SettingsWindow.Instance dans Start: {SettingsWindow.Instance}");

        if (InputManager.Instance == null)
        {
            Debug.LogError("L'instance de InputManager est nulle!");
            return;
        }

        // Vérifier si le joueur1KeyboardLayoutDropdown est assigné
        if (joueur1KeyboardLayoutDropdown == null)
        {
            // Rechercher le CanvasJoueur1 dans la scène
            GameObject canvasJoueur1 = GameObject.Find("CanvasJoueur1");
            if (canvasJoueur1 != null)
            {
                // Trouver le Dropdown dans le CanvasJoueur1
                joueur1KeyboardLayoutDropdown = canvasJoueur1.transform.Find("Joueur1KeyboardLayoutDropdown").GetComponent<Dropdown>();

                if (joueur1KeyboardLayoutDropdown == null)
                {
                    Debug.LogError("Le Dropdown Joueur1 n'a pas été trouvé dans le CanvasJoueur1 !");
                }
                else
                {
                    Debug.Log("Dropdown Joueur1 trouvé dans le CanvasJoueur1.");
                }
            }
            else
            {
                Debug.LogError("Le CanvasJoueur1 n'a pas été trouvé dans la scène.");
            }
        }
        else
        {
            Debug.Log("Le Dropdown Joueur1 est déjà assigné.");
        }

        // Ajouter le listener si le Dropdown est correctement assigné
        if (joueur1KeyboardLayoutDropdown != null)
        {
            joueur1KeyboardLayoutDropdown.onValueChanged.AddListener(OnJoueur1KeyboardLayoutChanged);
        }
        else
        {
            Debug.LogError("Le Dropdown Joueur1 est toujours null, impossible d'ajouter le listener.");
        }

        // Charger les préférences de clavier
        LoadKeyboardPreferences();
    }

    private void LoadKeyboardPreferences()
    {
        if (inputManager == null)
        {
            Debug.LogError("inputManager est null dans LoadKeyboardPreferences!");
            return;
        }

        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager n'est pas initialisé ! Assurez-vous qu'il est bien présent dans la scène.");
            return;  // Sortir de la fonction si InputManager n'est pas initialisé
        }

        inputManager.LoadKeyboardPreferenceForPlayer(1);  // Charger les préférences pour le joueur 1

        // Debugging : Vérifier si le clavier a bien été chargé
        Debug.Log($"Clavier du joueur 1 chargé : {inputManager.currentLayoutForPlayer1}");

        // Mettre à jour le dropdown avec la valeur de préférence
        joueur1KeyboardLayoutDropdown.value = (inputManager.currentLayoutForPlayer1 == KeyboardLayout.QWERTY) ? 0 : 1;

    }

    private void OnJoueur1KeyboardLayoutChanged(int index)
    {
        KeyboardLayout layout = (index == 0) ? KeyboardLayout.QWERTY : KeyboardLayout.QWERTY;
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
            Debug.Log("SettingsWindow instance cr��e");
        }
        else
        {
            Destroy(gameObject); 
            Debug.LogWarning("Une autre instance de SettingsWindow a �t� d�truite");
        }
    }


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
        PlayerPrefs.Save();
        Debug.Log("Param�tres sauvegard�s");
    }


    public void OnChangeKeyButtonClick(string actionName, int index)
    {
        int playerID = inputManager.keyBindings[index].playerID;

        InputManager.Instance.ReassignKey(actionName, playerID);

        SettingsWindow.Instance.UpdateKeyBindingsDisplay();
    }

   
}

