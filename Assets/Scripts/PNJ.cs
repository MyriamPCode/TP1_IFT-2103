using UnityEngine;
using TMPro;  
using System.Collections; 

public class DialogueNPC : MonoBehaviour
{
    private bool joueurProche = false;  
    private bool dialogueActif = false; 

    public TMP_Text texteDialogue;  
    public CanvasGroup npcCanvasGroup; 

    private string[] dialogue = {
        "Vous êtes entré dans un mystérieux niveau d'Halloween, essayez de vous en échapper !",
    };

    private int ligneActuelle = 0; 

    void Update()
    {
        if (joueurProche && Input.GetKeyDown(KeyCode.E) && !dialogueActif)
        {
            dialogueActif = true;
            AfficherDialogue(dialogue[ligneActuelle]);
            ligneActuelle++;
            Invoke("FinDialogue", 1.5f); 
        }
    }

    void AfficherDialogue(string texte)
    {
        texteDialogue.text = texte; 
    }

    void FinDialogue()
    {
        texteDialogue.text = " ";  
        dialogueActif = false;  
        StartCoroutine(FadeOutNpc());  
    }

    private IEnumerator FadeOutNpc()
    {
        float dureeDisparition = 1f;  
        float alphaDepart = npcCanvasGroup.alpha;  

        for (float t = 0; t < dureeDisparition; t += Time.deltaTime)
        {
            npcCanvasGroup.alpha = Mathf.Lerp(alphaDepart, 0, t / dureeDisparition);
            yield return null;  
        }

        npcCanvasGroup.alpha = 0;  
        Destroy(gameObject);  
    }

    private void OnTriggerEnter2D(Collider2D autre)
    {
        joueurProche = true;
        if (texteDialogue.text == "Appuyez sur E" || texteDialogue.text == "")
            texteDialogue.text = "Appuyez sur E";
        Debug.Log("Vous êtes proche du PNJ, appuyez sur E pour dialoguer");
    }

    private void OnTriggerExit2D(Collider2D autre)
    {
        joueurProche = false;
        if (texteDialogue.text == "Appuyez sur E")
            texteDialogue.text = "";
        Debug.Log("Vous avez quitté la zone du PNJ");
    }
}
