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
    public int playerID;
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public KeyboardLayout currentLayoutForPlayer1 = KeyboardLayout.QWERTY;
    public KeyboardLayout currentLayoutForPlayer2 = KeyboardLayout.QWERTY;

    public List<KeyBinding> keyBindings = new List<KeyBinding>();
    public HPlayer1 player1; 
    public HPlayer2 player2;

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
            Debug.LogWarning("Une autre instance d'InputManager a �t� d�truite");
        }
    }

    private void Start()
    {
        LoadKeyBindings();
        if (keyBindings.Count == 0)
        {
            AddKeyBindings(KeyboardLayout.QWERTY);
        }
    }

    private void AddKeyBindings(KeyboardLayout layout)
    {
        keyBindings.Clear();
        if (layout == KeyboardLayout.QWERTY)
        {
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
            if (Input.GetKey(binding.key))
            {
                HandleAction(binding.actionName, binding.playerID);
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
                    HPlayer1.Instance.MovePlayer(Vector2.left);
                    break;
                case "MoveRight":
                    HPlayer1.Instance.MovePlayer(Vector2.right);
                    break;
                case "Jump":
                    HPlayer1.Instance.Jump(); 
                    break;
            }
        }
        else if (playerID == 2)
        {
            switch (action)
            {
                case "MoveLeft":
                    HPlayer2.Instance.MovePlayer(Vector2.left);
                    break;
                case "MoveRight":
                    HPlayer2.Instance.MovePlayer(Vector2.right);
                    break;
                case "Jump":
                    HPlayer2.Instance.Jump(); 
                    break;
            }
        }
    }


    public void ReassignKey(string actionName, int playerID)
    {
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
                if (Input.GetKeyDown(key) && key != KeyCode.Escape)
                {
                    newKey = key;
                    keyPressed = true;
                    break;
                }
            }
            yield return null;
        }

        foreach (var binding in keyBindings)
        {
            if (binding.actionName == actionName && binding.playerID == playerID)
            {
                binding.key = newKey;
                Debug.Log($"Changement de la touche pour {actionName} du joueur {playerID} en {newKey}");
                break;
            }
        }
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

    public void LoadKeyboardPreferenceForPlayer(int playerID)
    {
        string key = $"KeyboardLayout_Player{playerID}";
        string layout = PlayerPrefs.GetString(key, "AZERTY");

        if (playerID == 1)
        {
            currentLayoutForPlayer1 = (layout == "AZERTY") ? KeyboardLayout.AZERTY : KeyboardLayout.QWERTY;
        }
        else if (playerID == 2)
        {
            currentLayoutForPlayer2 = (layout == "AZERTY") ? KeyboardLayout.AZERTY : KeyboardLayout.QWERTY;
        }

        Debug.Log($"Pr�f�rence de clavier pour le joueur {playerID} charg�e : {layout}");
    }

    public void SaveKeyboardPreferenceForPlayer(int playerID)
    {
        int layout = (playerID == 1) ? (int)currentLayoutForPlayer1 : (int)currentLayoutForPlayer2;
        PlayerPrefs.SetInt($"KeyboardLayoutPlayer{playerID}", layout);
        PlayerPrefs.Save();
    }

    public void SwitchKeyboardLayoutForPlayer(KeyboardLayout layout, int playerID)
    {
        if (playerID == 1)
        {
            currentLayoutForPlayer1 = layout;
        }
        else if (playerID == 2)
        {
            currentLayoutForPlayer2 = layout;
        }

        AddKeyBindings(layout);
    }
}