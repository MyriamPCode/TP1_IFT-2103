using UnityEngine;

public class VictoryFireworks : MonoBehaviour
{
    public ParticleSystem[] fireworks; // Liste des feux d'artifice
    public AudioClip fireworkSound;    // Son des feux d'artifice
    private AudioSource audioSource;

    private void Start()
    {
        // Récupère l'AudioSource ou l'ajoute s'il n'existe pas
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Déclenche les feux d'artifice
        StartFireworks();
    }

    private void StartFireworks()
    {
        foreach (var firework in fireworks)
        {
            firework.Play(); // Joue le feu d'artifice
            if (fireworkSound != null)
            {
                audioSource.PlayOneShot(fireworkSound); // Joue le son associé
            }
        }
    }
}
