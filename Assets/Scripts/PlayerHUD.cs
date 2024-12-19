using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public SpriteRenderer[] heartRenderers; // Les SpriteRenderers des cœurs
    public Sprite fullHeartSprite;         // Sprite pour un cœur plein
    public Sprite emptyHeartSprite;        // Sprite pour un cœur vide

    public int maxHealth = 3;              // Santé maximale
    private int currentHealth;             // Santé actuelle

    private void Start()
    {
        currentHealth = maxHealth;         // Initialise la santé
        UpdateHearts();                    // Met à jour les sprites au démarrage
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;               // Réduit la santé
            UpdateHearts();                // Met à jour les cœurs

            if (currentHealth <= 0)
            {
                Debug.Log("Le joueur est KO !");
                // Vous pouvez ajouter une logique ici, comme un respawn ou une fin de partie
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
