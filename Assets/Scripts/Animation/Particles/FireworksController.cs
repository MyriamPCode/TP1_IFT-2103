using UnityEngine;

public class FireworksController : MonoBehaviour
{
    public ParticleSystem fireworks;

    public void PlayFireworks()
    {
        if (fireworks != null)
        {
            fireworks.Play();
        }
        else
        {
            Debug.LogWarning("Aucun Particle System assigné pour les feux d'artifice !");
        }
    }
}
