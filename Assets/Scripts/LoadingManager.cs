using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar; // Barre de progression
    public TextMeshProUGUI loadingText; // Texte indiquant le chargement (optionnel)

    // Méthode pour charger une nouvelle scène avec un écran de chargement
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Coroutine pour charger la scène de manière asynchrone
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Démarrer le chargement de la scène
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Désactive le changement de scène automatique à la fin du chargement
        operation.allowSceneActivation = false;

        // Met à jour la barre de progression
        while (!operation.isDone)
        {
            // L'avancement est donné par operation.progress (entre 0 et 0.9)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Mettre à jour la barre de progression
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            // Mettre à jour le texte (optionnel)
            if (loadingText != null)
            {
                loadingText.text = "Chargement... " + (progress * 100).ToString("F0") + "%";
            }

            // Vérifie si le chargement est terminé
            if (operation.progress >= 0.9f)
            {
                // Une fois prêt, active la scène (par exemple, après un clic)
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
