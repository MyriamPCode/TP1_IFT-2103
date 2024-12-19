using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Joueur 1")]
    public GameObject player1;          // GameObject du joueur 1
    public GameObject player1HUD;      // HUD du joueur 1

    [Header("Joueur 2")]
    public GameObject player2;          // GameObject du joueur 2
    public GameObject player2HUD;      // HUD du joueur 2

    private void Start()
    {
        // Active les joueurs et leurs HUDs
        if (player1 != null && player1HUD != null)
        {
            player1.SetActive(true);   // Active l'objet du joueur 1
            player1HUD.SetActive(true); // Active le HUD du joueur 1
        }
        else
        {
            Debug.LogError("Player 1 ou son HUD n'est pas assigné !");
        }

        if (player2 != null && player2HUD != null)
        {
            player2.SetActive(true);   // Active l'objet du joueur 2
            player2HUD.SetActive(true); // Active le HUD du joueur 2
        }
        else
        {
            Debug.LogError("Player 2 ou son HUD n'est pas assigné !");
        }
    }
}
