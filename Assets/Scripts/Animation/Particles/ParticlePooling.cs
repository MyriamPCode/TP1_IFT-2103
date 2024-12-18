using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    [System.Serializable]
    public class ParticleType
    {
        public string typeName;
        public GameObject particlePrefab;  // Le prefab de particules à pooler
        public int poolSize = 10;          // Taille initiale du pool
    }

    public List<ParticleType> particleTypes; // Liste des types de particules
    private Dictionary<string, Queue<GameObject>> particlePools; // Dictionnaire de pools

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        particlePools = new Dictionary<string, Queue<GameObject>>();

        foreach (ParticleType type in particleTypes)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < type.poolSize; i++)
            {
                GameObject particle = Instantiate(type.particlePrefab, transform);
                particle.SetActive(false);
                pool.Enqueue(particle);
            }

            particlePools[type.typeName] = pool;
        }
    }

    public GameObject GetParticle(string typeName, Vector3 position, Quaternion rotation)
    {
        if (!particlePools.ContainsKey(typeName))
        {
            Debug.LogError($"Particle type '{typeName}' not found in pools.");
            return null;
        }

        GameObject particle = particlePools[typeName].Dequeue();
        particle.SetActive(true);
        particle.transform.position = position;
        particle.transform.rotation = rotation;

        // Désactive automatiquement l'objet après un certain temps
        StartCoroutine(ReturnToPool(typeName, particle, 2f)); // 2 secondes par défaut

        return particle;
    }

    private System.Collections.IEnumerator ReturnToPool(string typeName, GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        particle.SetActive(false);
        particlePools[typeName].Enqueue(particle);
    }
}
