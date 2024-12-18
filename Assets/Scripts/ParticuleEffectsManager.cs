using UnityEngine;

public class ParticleEffectsManager : MonoBehaviour
{
    public GameObject fireworksPrefab; // Prefab pour les feux d'artifice
    public GameObject trailParticlesPrefab; // Prefab pour la traînée de pixels

    private GameObject trailParticlesInstance; // Instance des particules de traînée

    void Start()
    {
        // Instanciez la traînée et désactivez-la initialement
        if (trailParticlesPrefab != null)
        {
            trailParticlesInstance = Instantiate(trailParticlesPrefab, Vector3.zero, Quaternion.identity);
            trailParticlesInstance.SetActive(false);
        }
    }

    void Update()
    {
        // Gestion des feux d'artifice au clic
        if (Input.GetMouseButtonDown(0)) // Bouton gauche de la souris
        {
            SpawnFireworksAtMouse();
        }

        // Gestion de la traînée de pixels
        UpdateTrailParticles();
    }

    private void SpawnFireworksAtMouse()
    {
        if (fireworksPrefab == null) return;

        // Position de la souris
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distance par rapport à la caméra
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Instanciation des feux d'artifice
        Instantiate(fireworksPrefab, worldPosition, Quaternion.identity);
    }

    private void UpdateTrailParticles()
    {
        if (trailParticlesInstance == null) return;

        // Position de la souris
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distance par rapport à la caméra
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Activer et suivre la souris
        trailParticlesInstance.SetActive(true);
        trailParticlesInstance.transform.position = worldPosition;
    }
}
