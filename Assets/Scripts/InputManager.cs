using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;

[System.Serializable]
public class KeyBinding
{
    public string actionName;
    public KeyCode key;
    public int playerID;
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public KeyboardLayout currentLayout = KeyboardLayout.QWERTY;

    public List<KeyBinding> keyBindings = new List<KeyBinding>();
    public Player player1; 
    public Player2 player2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadKeyBindings();
        if (keyBindings.Count == 0)
        {
            // Mappages de touches par défaut pour QWERTY
            AddKeyBindings(KeyboardLayout.QWERTY);
        }
    }

    private void AddKeyBindings(KeyboardLayout layout)
    {
        keyBindings.Clear();  // Réinitialiser les mappages existants
        if (layout == KeyboardLayout.QWERTY)
        {
            // Ajout des touches QWERTY
            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.A, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.D, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Space, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "Interaction", key = KeyCode.E, playerID = 1 });

            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.LeftArrow, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.RightArrow, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Return, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "Interaction", key = KeyCode.E, playerID = 2 });
        }
        else if (layout == KeyboardLayout.AZERTY)
        {
            // Ajout des touches AZERTY
            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.Q, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.D, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Space, playerID = 1 });
            keyBindings.Add(new KeyBinding { actionName = "Interaction", key = KeyCode.E, playerID = 1 });

            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.LeftArrow, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.RightArrow, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Return, playerID = 2 });
            keyBindings.Add(new KeyBinding { actionName = "Interaction", key = KeyCode.E, playerID = 2 });
        }
    }

    private void Update()
    {
        ProcessInput();
    }

    public void ProcessInput()
    {
        foreach (var binding in keyBindings)
        {
            if (Input.GetKey(binding.key)) // Si la touche est pressée
            {
                HandleAction(binding.actionName, binding.playerID); // Appeler la fonction selon l'action et le joueur
            }
        }
    }

    private void HandleAction(string action, int playerID)
    {
        
        if (playerID == 1)
        {
            switch (action)
            {
                case "MoveLeft":
                    Player.Instance.MovePlayer(Vector2.left); // Utiliser une instance pour Player1
                    break;
                case "MoveRight":
                    Player.Instance.MovePlayer(Vector2.right); // Utiliser une instance pour Player1
                    break;
                case "Jump":
                    Player.Instance.Jump(); 
                    break;
            }
        }
        else if (playerID == 2)
        {
            switch (action)
            {
                case "MoveLeft":
                    Player2.Instance.MovePlayer(Vector2.left); // Utiliser une instance pour Player2
                    break;
                case "MoveRight":
                    Player2.Instance.MovePlayer(Vector2.right); // Utiliser une instance pour Player2
                    break;
                case "Jump":
                    Player2.Instance.Jump(); 
                    break;
            }
        }
    }


    public void ReassignKey(string actionName, int playerID)
    {
        // Attendre que l'utilisateur appuie sur une nouvelle touche
        StartCoroutine(WaitForKeyPress(actionName, playerID));
    }

    private IEnumerator WaitForKeyPress(string actionName, int playerID)
    {
        bool keyPressed = false;
        KeyCode newKey = KeyCode.None;

        while (!keyPressed)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key) && key != KeyCode.Escape) // Évitez d'utiliser Échap pour réassignation
                {
                    newKey = key;
                    keyPressed = true;
                    break;
                }
            }
            yield return null;
        }

        // Mettre à jour la clé dans les mappages
        foreach (var binding in keyBindings)
        {
            /*if (binding.actionName == actionName)
             */
            if (binding.actionName == actionName && binding.playerID == playerID)
            {
                binding.key = newKey;
                Debug.Log($"Changement de la touche pour {actionName} du joueur {playerID} en {newKey}");
                break;
            }
        }
        SettingsWindow.Instance.UpdateKeyBindingsDisplay();
        SaveKeyBindings();
    }

    public void SaveKeyBindings()
    {
        foreach (var binding in keyBindings)
        {
            PlayerPrefs.SetString(binding.actionName, binding.key.ToString());
        }
        PlayerPrefs.Save();
    }

    private void LoadKeyBindings()
    {
        foreach (var binding in keyBindings)
        {
            if (PlayerPrefs.HasKey(binding.actionName))
            {
                string keyString = PlayerPrefs.GetString(binding.actionName);
                if (System.Enum.TryParse(keyString, out KeyCode newKey))
                {
                    binding.key = newKey;
                }
            }
        }
    }

    public void LoadKeyboardPreference()
    {
        int layout = PlayerPrefs.GetInt("KeyboardLayout", (int)KeyboardLayout.QWERTY);
        if (layout == (int)KeyboardLayout.AZERTY)
        {
            SwitchKeyboardLayout(KeyboardLayout.AZERTY);
        }
        else
        {
            SwitchKeyboardLayout(KeyboardLayout.QWERTY);
        }
    }
    public void SaveKeyboardPreference()
    {
        PlayerPrefs.SetInt("KeyboardLayout", (int)currentLayout);
        PlayerPrefs.Save();
    }

    public void SwitchKeyboardLayout(KeyboardLayout layout)
    {
        currentLayout = layout;
        AddKeyBindings(layout);
        SaveKeyboardPreference();  // Sauvegarder immédiatement après le changement de disposition
    }
}