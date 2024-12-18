using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffectWithText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    private Color originalButtonColor;

    private TMP_Text buttonText;
    private Color originalTextColor;

    public float hoverTransparency = 0.5f;
    public float transitionSpeed = 0.2f;

    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
            originalButtonColor = buttonImage.color;

        buttonText = GetComponentInChildren<TMP_Text>();
        originalTextColor = buttonText.color;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToTransparency(hoverTransparency));
        }

        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToTransparency(1f));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    private System.Collections.IEnumerator FadeToTransparency(float targetAlpha)
    {
        float currentButtonAlpha = buttonImage.color.a;
        float currentTextAlpha = buttonText != null ? buttonText.color.a : 1f;
        float time = 0;

        // Easing
        while (time < transitionSpeed)
        {
            time += Time.deltaTime;

            float newButtonAlpha = Mathf.Lerp(currentButtonAlpha, targetAlpha, time / transitionSpeed);
            buttonImage.color = new Color(originalButtonColor.r, originalButtonColor.g, originalButtonColor.b, newButtonAlpha);

            float newTextAlpha = Mathf.Lerp(currentTextAlpha, targetAlpha, time / transitionSpeed);
            buttonText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, newTextAlpha);

            yield return null;
        }

        buttonImage.color = new Color(originalButtonColor.r, originalButtonColor.g, originalButtonColor.b, targetAlpha);
        buttonText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, targetAlpha);
    }
}
