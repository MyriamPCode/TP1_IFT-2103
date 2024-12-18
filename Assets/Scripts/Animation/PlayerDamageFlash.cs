using System.Collections;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float flashDuration = 0.1f;
    public int flashCount = 3;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer non trouvé sur le personnage !");
            return;
        }
        originalColor = spriteRenderer.color;
    }

    public void FlashOnRespawn()
    {
        StartCoroutine(FlashEffect(Color.white));
    }

    public void FlashOnDamage()
    {
        StartCoroutine(FlashEffect(Color.red));
    }

    private IEnumerator FlashEffect(Color flashColor)
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
