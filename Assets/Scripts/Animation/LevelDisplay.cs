using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Main Scene")
        {
            levelText.text = "Niveau 1";
        }
        else if (sceneName == "SecondScene")
        {
            levelText.text = "Niveau Halloween";
        }
        else
        {
            levelText.text = "Niveau Inconnu";
        }
    }
}
