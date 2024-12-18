using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextGlowEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textMeshPro;
    private bool isHovered = false;

    public Color glowColor = Color.white;
    public float pulseSpeed = 2f;
    public float maxGlowAlpha = 0.8f;
    public float minGlowAlpha = 0.2f;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI introuvable !");
            return;
        }

        textMeshPro.outlineColor = glowColor;
        textMeshPro.outlineWidth = 0.2f;
    }

    private void Update()
    {
        if (!isHovered)
        {
            // Easing
            float alpha = Mathf.Lerp(minGlowAlpha, maxGlowAlpha, Mathf.PingPong(Time.time * pulseSpeed, 1f));
            SetOutlineAlpha(alpha);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        SetOutlineAlpha(maxGlowAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    private void SetOutlineAlpha(float alpha)
    {
        Color currentColor = textMeshPro.outlineColor;
        textMeshPro.outlineColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }
}
