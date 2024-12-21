using UnityEngine;

public class MouseTrailController : MonoBehaviour
{
    public ParticleSystem trailParticles; // Référence au système de particules
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (trailParticles != null)
        {
            Debug.Log("Particle System trouvé et activé !");
            trailParticles.Play(); // S'assurer que les particules sont activées
        }
        else
        {
            Debug.LogWarning("Aucun Particle System assigné !");
        }
    }

    private void Update()
    {
        // Récupérer la position de la souris
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;

        // Convertir en coordonnées du monde
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // Distance correcte par rapport à la caméra
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Forcer Z à 0
        worldPosition.z = -20;

        // Appliquer la position
        trailParticles.transform.position = worldPosition;
        Debug.LogWarning($"Trail position: {worldPosition}");
        Debug.LogWarning($"Mouse position: {mousePosition}");
    }
}
