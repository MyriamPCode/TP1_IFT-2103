using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyboardLayout
{
    AZERTY,
    QWERTY
}

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance { get; private set; }

    public KeyboardLayout currentLayout = KeyboardLayout.QWERTY;  // Par défaut, QWERTY
    private Dictionary<string, KeyCode> keyBindingsAZERTY = new Dictionary<string, KeyCode>();
    private Dictionary<string, KeyCode> keyBindingsQWERTY = new Dictionary<string, KeyCode>();

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

        // Initialisation des mappages AZERTY
        keyBindingsAZERTY["MoveLeft"] = KeyCode.Q; // Touche Q pour "MoveLeft" sur un clavier AZERTY
        keyBindingsAZERTY["MoveRight"] = KeyCode.D; // Touche D pour "MoveRight" sur un clavier AZERTY
        keyBindingsAZERTY["Jump"] = KeyCode.Space;

        // Initialisation des mappages QWERTY
        keyBindingsQWERTY["MoveLeft"] = KeyCode.A; // Touche A pour "MoveLeft" sur un clavier QWERTY
        keyBindingsQWERTY["MoveRight"] = KeyCode.D; // Touche D pour "MoveRight" sur un clavier QWERTY
        keyBindingsQWERTY["Jump"] = KeyCode.Space;
    }

    // Récupérer la touche pour une action spécifique en fonction du type de clavier
    public KeyCode GetKeyForAction(string action)
    {
        if (currentLayout == KeyboardLayout.AZERTY)
        {
            return keyBindingsAZERTY[action];
        }
        else // QWERTY
        {
            return keyBindingsQWERTY[action];
        }
    }

    // Méthode pour changer la disposition du clavier
    public void SwitchToAZERTY()
    {
        currentLayout = KeyboardLayout.AZERTY;
    }

    public void SwitchToQWERTY()
    {
        currentLayout = KeyboardLayout.QWERTY;
    }
}
