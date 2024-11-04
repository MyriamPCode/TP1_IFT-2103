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
        LoadKeyBindings();
        if (keyBindings.Count == 0)
        {
            keyBindings.Add(new KeyBinding { actionName = "MoveLeft", key = KeyCode.A });
            keyBindings.Add(new KeyBinding { actionName = "MoveRight", key = KeyCode.D });
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
            if (Input.GetKey(binding.key))
            {
                Debug.Log($"Action d�clench�e : {binding.actionName} avec la touche {binding.key}");
                HandleAction(binding.actionName);
            }
        }
    }

    private void HandleAction(string action)
    {
        switch (action)
        {
            case "MoveLeft":
                Player.Instance.MovePlayer(Vector2.left);
                break;
            case "MoveRight":
                Player.Instance.MovePlayer(Vector2.right);
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
                if (Input.GetKeyDown(key) && key != KeyCode.Escape) // �vitez d'utiliser �chap pour r�assignation
                {
                    newKey = key;
                    keyPressed = true;
                    break;
                }
            }
            yield return null;
        }

        // Mettre � jour la cl� dans les mappages
        foreach (var binding in keyBindings)
        {
            if (binding.actionName == actionName)
            {
                binding.key = newKey;
                Debug.Log($"Changement de la touche pour {actionName} en {newKey}");
                SaveKeyBindings();
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