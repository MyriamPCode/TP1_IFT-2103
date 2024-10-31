using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance { get; private set; }

    private void Start()
    {
        Debug.Log($"SettingsWindow.Instance dans Start: {SettingsWindow.Instance}");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
            Debug.Log("SettingsWindow instance créée");
        }
        else
        {
            Destroy(gameObject); // Détruire les doublons
            Debug.LogWarning("Une autre instance de SettingsWindow a été détruite");
        }
    }

    // Méthodes pour gérer votre fenêtre de paramètres
    public void OpenSettings()
    {
        
        Debug.Log("OpenSettings() appelé, état avant activation : " + gameObject.activeSelf);
        gameObject.SetActive(true);

    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}
