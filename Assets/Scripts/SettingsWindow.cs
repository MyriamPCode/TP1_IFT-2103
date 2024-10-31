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
        
        Debug.Log("OpenSettings() appel�, �tat avant activation : " + gameObject.activeSelf);
        gameObject.SetActive(true);

    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}
