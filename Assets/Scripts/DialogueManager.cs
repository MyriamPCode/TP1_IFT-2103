using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialoguePanel;

    private bool playerIsNear = false;

    private string[] dialogue = {
        "Vous êtes entré dans un niveau d'Halloween magique, essayez de vous échapper !",
    };

    private int currentLine = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            if (currentLine < dialogue.Length)
            {
                ShowDialogue(dialogue[currentLine]);
                currentLine++;
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void ShowDialogue(string text)
    {
        dialogueText.text = text;
        dialoguePanel.SetActive(true);
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentLine = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        playerIsNear = true;
        Debug.Log("Appuyez sur E pour dialoguer avec le PNJ");
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        playerIsNear = false;
        Debug.Log("Vous avez quitté la zone du PNJ");
        
    }
}
