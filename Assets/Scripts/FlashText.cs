using System.Collections;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    public float flashInterval = 0.5f; // Intervalle entre chaque clignotement en secondes
    private GameObject textObject;

    private void Start()
    {
        textObject = gameObject;
        textObject.SetActive(true); // Rendre le texte visible au départ
        StartCoroutine(FlashTextCoroutine());
    }

    private IEnumerator FlashTextCoroutine()
    {
        while (true) // Cette boucle clignote indéfiniment
        {
            textObject.SetActive(false); // Désactiver le texte
            yield return new WaitForSeconds(flashInterval); // Attendre un intervalle

            textObject.SetActive(true); // Réactiver le texte
            yield return new WaitForSeconds(flashInterval); // Attendre un autre intervalle
        }
    }
}
