using UnityEngine;
using UnityEngine.UI;  // Importer l'espace de noms UI

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;  // Le composant Text pour afficher le texte du dialogue
    public GameObject dialoguePanel;  // Le panneau (fond) du dialogue

    private bool playerIsNear = false;  // Si le joueur est proche du PNJ

    // Contenu du dialogue
    private string[] dialogue = {
        "Vous êtes entré dans un niveau d'Halloween magique, essayez de vous échapper !",
    };

    private int currentLine = 0;  // Numéro de la ligne de dialogue actuelle

    void Start()
    {
        dialoguePanel.SetActive(false);  // Le panneau de dialogue est caché au début du jeu
    }

    void Update()
    {
        // Lorsque le joueur est proche et appuie sur la touche E, commencer le dialogue
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            if (currentLine < dialogue.Length)
            {
                ShowDialogue(dialogue[currentLine]);  // Afficher le dialogue actuel
                currentLine++;
            }
            else
            {
                EndDialogue();  // Si le dialogue est terminé, le finir
            }
        }
    }

    // Afficher le dialogue
    void ShowDialogue(string text)
    {
        dialogueText.text = text;  // Mettre à jour le texte
        dialoguePanel.SetActive(true);  // Afficher le panneau de dialogue
    }

    // Finir le dialogue
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);  // Cacher le panneau de dialogue
        currentLine = 0;  // Réinitialiser le numéro de la ligne
    }

    // Lorsque le joueur entre dans la zone de déclenchement
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        playerIsNear = true;
        Debug.Log("Appuyez sur E pour dialoguer avec le PNJ");
    
    }

    // Lorsque le joueur quitte la zone de déclenchement
    private void OnTriggerExit2D(Collider2D other)
    {

        playerIsNear = false;
        Debug.Log("Vous avez quitté la zone du PNJ");
        
    }
}
