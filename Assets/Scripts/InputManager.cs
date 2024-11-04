using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class KeyBinding
{
    public string actionName;
    public KeyCode key;
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public List<KeyBinding> keyBindings = new List<KeyBinding>();

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
        LoadKeyBindings(); // Charger les mappages au démarrage
        if (keyBindings.Count == 0)
        {
            // Ajouter des mappages par défaut si aucun n'existe
            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.A });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.D });
        }
    }

    public void ProcessInput()
    {
        foreach (var binding in keyBindings)
        {
            if (Input.GetKey(binding.key))
            {
                // Gérer l'action en fonction du nom
                HandleAction(binding.actionName);
            }
        }
    }

    private void HandleAction(string action)
    {
        switch (action)
        {
            case "MoveLeft":
                // Code pour avancer
                break;
            case "MoveRight":
                // Code pour reculer
                break;
                // Ajoutez d'autres actions
        }
    }

    public void ReassignKey(string actionName)
    {
        // Attendre que l'utilisateur appuie sur une nouvelle touche
        StartCoroutine(WaitForKeyPress(actionName));
    }

    private IEnumerator WaitForKeyPress(string actionName)
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
            if (binding.actionName == actionName)
            {
                binding.key = newKey;
                Debug.Log($"Changement de la touche pour {actionName} en {newKey}");
                break;
            }
        }
        SettingsWindow.Instance.UpdateKeyBindingsDisplay();
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
}