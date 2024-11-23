using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueNPC : MonoBehaviour
{
    private bool joueurProche = false;  // Si le joueur est proche
    private bool dialogueActif = false;  // Si le dialogue est en cours

    public TMP_Text texteDialogue;  // Le composant TextMeshPro pour afficher le dialogue
    public CanvasGroup npcCanvasGroup;  // CanvasGroup pour le NPC, utilisé pour l'effet de disparition

    private string[] dialogue = {
        "Vous êtes entré dans un mystérieux niveau d'Halloween, essayez de vous en échapper !",
    };

    private int ligneActuelle = 0;  // Le numéro de la ligne actuelle du dialogue

    void Update()
    {
        // Lorsque le joueur est proche et appuie sur la touche E, commence le dialogue
        if (joueurProche && Input.GetKeyDown(KeyCode.E) && !dialogueActif)
        {
            dialogueActif = true;
            AfficherDialogue(dialogue[ligneActuelle]);  // Affiche le dialogue actuel
            ligneActuelle++;
            Invoke("FinDialogue", 1.5f);  // Fin du dialogue après 2 secondes
        }

    }

    // Afficher le texte du dialogue
    void AfficherDialogue(string texte)
    {
        texteDialogue.text = texte;  // Met à jour le texte du dialogue
    }

    // Terminer le dialogue
    void FinDialogue()
    {
        texteDialogue.text = " ";  // Efface le texte du dialogue
        dialogueActif = false;  // Réinitialise l'état du dialogue
        StartCoroutine(FadeOutNpc());  // Lance la disparition progressive du NPC
    }

    // Coroutine pour faire disparaître progressivement le NPC
    private IEnumerator FadeOutNpc()
    {
        float dureeDisparition = 1f;  // Durée de la disparition progressive
        float alphaDepart = npcCanvasGroup.alpha;  // Alpha initial du NPC

        for (float t = 0; t < dureeDisparition; t += Time.deltaTime)
        {
            npcCanvasGroup.alpha = Mathf.Lerp(alphaDepart, 0, t / dureeDisparition);  // Lerp pour ajuster l'alpha
            yield return null;  // Attends la prochaine frame
        }

        npcCanvasGroup.alpha = 0;  // S'assure que l'alpha est à 0 à la fin
        Destroy(gameObject);  // Détruit le NPC après qu'il soit complètement invisible
    }

    // Lorsque le joueur entre dans la zone de déclenchement
    private void OnTriggerEnter2D(Collider2D autre)
    {
        joueurProche = true;
        if (texteDialogue.text == "Appuyez sur E" || texteDialogue.text == "")
            texteDialogue.text = "Appuyez sur E";
        Debug.Log("Vous êtes proche du PNJ, appuyez sur E pour dialoguer");
    }

    // Lorsque le joueur quitte la zone de déclenchement
    private void OnTriggerExit2D(Collider2D autre)
    {
        joueurProche = false;
        if (texteDialogue.text == "Appuyez sur E")
            texteDialogue.text = "";
        Debug.Log("Vous avez quitté la zone du PNJ");
    }
}
