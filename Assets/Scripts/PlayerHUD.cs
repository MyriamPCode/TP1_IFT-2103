using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public int playerIndex;
    public SpriteRenderer[] heartRenderers;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        string playerName = PlayerPrefs.GetString($"PlayerName{playerIndex}", $"Joueur {playerIndex}");

        if (playerNameText != null)
        {
            playerNameText.text = playerName; // Affiche le nom dans le HUD
        }
        else
        {
            Debug.LogWarning($"Texte pour le nom du joueur {playerIndex} non assigné !");
        }

        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();

            if (currentHealth <= 0)
            {
                Debug.Log("Le joueur est KO !");
            }
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < heartRenderers.Length; i++)
        {
            // Change le sprite en fonction de la santé restante
            heartRenderers[i].sprite = i < currentHealth ? fullHeartSprite : emptyHeartSprite;
        }
    }
}
