using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;

[System.Serializable]
public class KeyBinding
{
    public string actionName;
    public KeyCode key;
    public int playerID;
    public string gamepadButton;
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public List<KeyBinding> keyBindings = new List<KeyBinding>();
    public Player player1;
    public Player2 player2;

    public enum InputDeviceType { Keyboard, Gamepad }

    // On garde une référence à quel périphérique chaque joueur utilise
    public InputDeviceType player1InputDevice = InputDeviceType.Keyboard;
    public InputDeviceType player2InputDevice = InputDeviceType.Keyboard;

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
        // Initialisation des mappages de touches
        if (keyBindings.Count == 0)
        {
            InitializeDefaultKeyBindings();
        }
    }

    private void InitializeDefaultKeyBindings()
    {
        keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.A, playerID = 1 });
        keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.D, playerID = 1 });
        keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Space, playerID = 1 });

        keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.LeftArrow, playerID = 2 });
        keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.RightArrow, playerID = 2 });
        keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Return, playerID = 2 });
    }

    private void Update()
    {
        ProcessInput();
    }

    public void SetPlayerInputDevice(int playerIndex, InputDeviceType deviceType)
    {
        if (playerIndex == 1)
        {
            player1InputDevice = deviceType;
        }
        else if (playerIndex == 2)
        {
            player2InputDevice = deviceType;
        }
    }

    public void ProcessInput()
    {
        // Gestion des entrées pour le joueur 1
        if (player1InputDevice == InputDeviceType.Keyboard)
        {
            HandleKeyboardInput(player1);
        }
        else if (player1InputDevice == InputDeviceType.Gamepad)
        {
            HandleGamepadInput(player1);
        }

        // Gestion des entrées pour le joueur 2
        if (player2InputDevice == InputDeviceType.Keyboard)
        {
            HandleKeyboardInput(player2);
        }
        else if (player2InputDevice == InputDeviceType.Gamepad)
        {
            HandleGamepadInput(player2);
        }
    }

    private void HandleKeyboardInput(Player player)
    {
        foreach (var binding in keyBindings)
        {
            if (binding.playerID == player.playerIndex && Input.GetKey(binding.key))
            {
                HandleAction(binding.actionName, player);
            }
        }
    }

    private void HandleKeyboardInput(Player2 player2)
    {
        foreach (var binding in keyBindings)
        {
            if (binding.playerID == player2.playerIndex && Input.GetKey(binding.key))
            {
                HandleAction(binding.actionName, player2);
            }
        }
    }

    private void HandleGamepadInput(Player player)
    {
        foreach (var binding in keyBindings)
        {
            if (binding.playerID == player.playerIndex && !string.IsNullOrEmpty(binding.gamepadButton))
            {
                if (Input.GetButton(binding.gamepadButton)) // Utiliser un nom de bouton valide
                {
                    HandleAction(binding.actionName, player);
                }
            }
        }
    }

    private void HandleGamepadInput(Player2 player2)
    {
        foreach (var binding in keyBindings)
        {
            if (binding.playerID == player2.playerIndex && !string.IsNullOrEmpty(binding.gamepadButton))
            {
                if (Input.GetButton(binding.gamepadButton)) // Utiliser un nom de bouton valide
                {
                    HandleAction(binding.actionName, player2);
                }
            }
        }
    }

    private void HandleAction(string action, Player player)
    {
        switch (action)
        {
            case "MoveLeft":
                player.MovePlayer(Vector2.left);
                break;
            case "MoveRight":
                player.MovePlayer(Vector2.right);
                break;
            case "Jump":
                player.Jump();
                break;
        }
    }

    private void HandleAction(string action, Player2 player2)
    {
        switch (action)
        {
            case "MoveLeft":
                player2.MovePlayer(Vector2.left);
                break;
            case "MoveRight":
                player2.MovePlayer(Vector2.right);
                break;
            case "Jump":
                player2.Jump();
                break;
        }
    }

    public void SaveKeyBindings()
    {
        foreach (var binding in keyBindings)
        {
            PlayerPrefs.SetString(binding.actionName + "_" + binding.playerID, binding.key.ToString());
            PlayerPrefs.SetString(binding.actionName + "_button_" + binding.playerID, binding.gamepadButton);
        }
        PlayerPrefs.Save();
    }

    private void LoadKeyBindings()
    {
        foreach (var binding in keyBindings)
        {
            if (PlayerPrefs.HasKey(binding.actionName + "_" + binding.playerID))
            {
                string keyString = PlayerPrefs.GetString(binding.actionName + "_" + binding.playerID);
                if (System.Enum.TryParse(keyString, out KeyCode newKey))
                {
                    binding.key = newKey;
                }
            }
            if (PlayerPrefs.HasKey(binding.actionName + "_button_" + binding.playerID))
            {
                binding.gamepadButton = PlayerPrefs.GetString(binding.actionName + "_button_" + binding.playerID);
            }
        }
    }

    public void ChangeKeyBinding(string action, KeyCode newKey, int playerID)
    {
        var binding = keyBindings.Find(b => b.actionName == action && b.playerID == playerID);
        if (binding != null)
        {
            binding.key = newKey;
            Debug.Log($"Changement de la touche pour {action} en {newKey}");
        }
    }

    public void ChangeGamepadBinding(string action, string newButton, int playerID)
    {
        var binding = keyBindings.Find(b => b.actionName == action && b.playerID == playerID);
        if (binding != null)
        {
            binding.gamepadButton = newButton;
            Debug.Log($"Changement du bouton pour {action} en {newButton}");
        }
    }

    public void ReassignKey(string action, int playerID)
    {
        // Appelle une méthode qui commence à capturer l'entrée (touche du clavier ou bouton manette)
        StartCoroutine(CaptureInputForReassign(action, playerID));
    }

    private IEnumerator CaptureInputForReassign(string action, int playerID)
    {
        bool inputCaptured = false;

        while (!inputCaptured)
        {
            // Vérifier si une touche du clavier est pressée
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        // Mettre à jour la touche dans keyBindings
                        var binding = keyBindings.Find(b => b.actionName == action && b.playerID == playerID);
                        if (binding != null)
                        {
                            binding.key = key;
                            inputCaptured = true;
                            Debug.Log($"Nouvelle touche assignée : {key} pour {action}");
                        }
                        break;
                    }
                }
            }

            // Vérifier si un bouton de manette est pressé (on vérifie les 20 boutons possibles ici)
            for (int i = 0; i < 20; i++) // Limité à 20 boutons (pour une manette Xbox, par exemple)
            {
                string buttonName = $"Joystick{playerID}Button{i}";
                if (Input.GetButtonDown(buttonName))
                {
                    // Mettre à jour le bouton de la manette dans keyBindings
                    var binding = keyBindings.Find(b => b.actionName == action && b.playerID == playerID);
                    if (binding != null)
                    {
                        binding.gamepadButton = buttonName;
                        inputCaptured = true;
                        Debug.Log($"Nouveau bouton assigné : {buttonName} pour {action}");
                    }
                    break;
                }
            }

            yield return null; // Attendre une autre frame
        }
    }
}