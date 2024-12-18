using UnityEngine;

public class PixelParticleController : MonoBehaviour
{
    private ParticleSystem particleSystem;        // Référence au Particle System
    private ParticleSystem.MainModule mainModule; // Module principal pour modifier les propriétés
    private ParticleSystem.ColorOverLifetimeModule colorModule;

    [Header("Vitesse des Particules")]
    public float minSpeed = 0.5f;    // Vitesse minimale
    public float maxSpeed = 2f;      // Vitesse maximale

    [Header("Taille des Particules")]
    public float minSize = 0.1f;
    public float maxSize = 0.3f;

    [Header("Couleurs des Particules")]
    public Gradient gradientColors;  // Gradient pour des couleurs variées

    [Header("Emission")]
    public int emissionRate = 20;    // Particules émises par seconde

    private void Start()
    {
        // Récupérer le Particle System
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("Aucun Particle System trouvé !");
            return;
        }

        // Récupérer les modules pour configurer les particules
        mainModule = particleSystem.main;
        colorModule = particleSystem.colorOverLifetime;

        ConfigureParticles();
    }

    private void ConfigureParticles()
    {
        // Vitesse aléatoire
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(minSpeed, maxSpeed);

        // Taille aléatoire
        mainModule.startSize = new ParticleSystem.MinMaxCurve(minSize, maxSize);

        // Couleurs variées avec un gradient
        colorModule.enabled = true;
        colorModule.color = new ParticleSystem.MinMaxGradient(gradientColors);

        // Emission continue
        var emission = particleSystem.emission;
        emission.rateOverTime = emissionRate;

        // Boucle indéfinie
        mainModule.loop = true;

        Debug.Log("Configuration des particules réussie !");
    }
}
