using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    [Header("Plateforme")]
    public GameObject platformPrefab;
    public int platformCount = 4;
    public float minDistanceX = 3f;
    public float maxDistanceX = 5f;
    public float minDistanceY = -0.3f;
    public float maxDistanceY = 0.2f;
    public float minX = 10f;
    public float maxX = 20f;
    public float minY = -2.5f;
    public float maxY = 1f;

    [Header("Thèmes")]
    [Tooltip("Si coché, force le thème Halloween. Sinon, thème choisi aléatoirement.")]
    public bool forceHalloweenTheme = false;
    private bool isHalloweenTheme;
    public GameObject halloweenBackground;
    public GameObject normalBackground;
    public GameObject halloweenClouds;
    public GameObject normalClouds;

    [Header("Objets spéciaux")]
    public Transform respawnPoint;
    public GameObject victoryPointPrefab;
    public GameObject enemyPrefab;
    public GameObject pnjPrefab;
    public GameObject obstaclePrefab;

    private Vector2 currentPosition;
    private Vector2 lastPlatformPosition;
    private List<Vector2> platformPositions = new List<Vector2>();

    void Start()
    {
        Debug.Log("Début de la génération des plateformes.");
        ChooseRandomTheme();
        DebugThemeChoice();
        StartCoroutine(GeneratePlatformsCoroutine());
    }

    IEnumerator GeneratePlatformsCoroutine()
    {
        Debug.Log("Début de la coroutine de génération.");
        
        ApplyTheme();
        Debug.Log("Thème appliqué.");

        PlaceFirstPlatform();
        Debug.Log("Première plateforme placée.");
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < platformCount - 2; i++)
        {
            Debug.Log($"Placement de la plateforme intermédiaire {i + 1}.");
            PlaceIntermediatePlatform(i);
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Placement de la dernière plateforme...");
        PlaceLastPlatformAtMaxX();
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Placement du point de victoire...");
        PlaceVictoryPoint();
    }

    void ChooseRandomTheme()
    {
        isHalloweenTheme = forceHalloweenTheme || Random.value > 0.5f;
    }

    void PlaceFirstPlatform()
    {
        Vector2 firstPlatformPosition = new Vector2(respawnPoint.position.x, respawnPoint.position.y - 2f);
        currentPosition = firstPlatformPosition;
        lastPlatformPosition = firstPlatformPosition;

        Instantiate(platformPrefab, firstPlatformPosition, Quaternion.identity);
        platformPositions.Add(firstPlatformPosition);

        Debug.Log($"Première plateforme placée à la position : {firstPlatformPosition}");
    }

    void PlaceIntermediatePlatform(int index)
    {
        float distanceX = Random.Range(minDistanceX, maxDistanceX);
        float distanceY = Random.Range(minDistanceY, maxDistanceY);

        Vector2 newPosition = currentPosition + new Vector2(distanceX, distanceY);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        currentPosition = newPosition;
        lastPlatformPosition = newPosition;

        Instantiate(platformPrefab, currentPosition, Quaternion.identity);
        platformPositions.Add(currentPosition);

        if (index == 0 && isHalloweenTheme)
        {
            Vector2 enemyPosition = new Vector2(currentPosition.x, currentPosition.y + 1.5f);
            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
            Debug.Log($"Ennemi placé sur la plateforme intermédiaire à la position : {enemyPosition}");
        }
        else if (index == 1 && isHalloweenTheme)
        {
            Vector2 obstaclePosition = new Vector2(currentPosition.x, currentPosition.y + 1.5f);
            Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
            Debug.Log($"Obstacle placé sur la plateforme intermédiaire à la position : {obstaclePosition}");
        }

        Debug.Log($"Plateforme intermédiaire {index + 1} placée à la position : {currentPosition}");
    }

    void PlaceLastPlatformAtMaxX()
    {
        Vector2 lastPlatformPositionAtMaxX = new Vector2(maxX, Mathf.Clamp(currentPosition.y, minY, maxY));

        if (platformPrefab != null)
        {
            Instantiate(platformPrefab, lastPlatformPositionAtMaxX, Quaternion.identity);
            lastPlatformPosition = lastPlatformPositionAtMaxX;
            platformPositions.Add(lastPlatformPosition);
            Debug.Log($"Dernière plateforme placée à la position : {lastPlatformPosition}");
        }
        else
        {
            Debug.LogError("Le prefab de la plateforme est manquant !");
        }
    }

    void PlaceVictoryPoint()
    {
        Vector2 victoryPosition = new Vector2(lastPlatformPosition.x, lastPlatformPosition.y + 2f);

        if (victoryPointPrefab != null)
        {
            Instantiate(victoryPointPrefab, victoryPosition, Quaternion.identity);
            Debug.Log($"Point de victoire placé à la position : {victoryPosition}");
        }
        else
        {
            Debug.LogError("Le prefab du point de victoire est manquant !");
        }
    }

    void ApplyTheme()
    {
        halloweenBackground.SetActive(false);
        normalBackground.SetActive(false);
        halloweenClouds.SetActive(false);
        normalClouds.SetActive(false);
        pnjPrefab.SetActive(false);

        if (isHalloweenTheme)
        {
            halloweenBackground.SetActive(true);
            halloweenClouds.SetActive(true);
            pnjPrefab.SetActive(true);
        }
        else
        {
            normalBackground.SetActive(true);
            normalClouds.SetActive(true);
        }
    }

    void DebugThemeChoice()
    {
        Debug.Log(isHalloweenTheme ? "Thème choisi : Halloween" : "Thème choisi : Normal");
    }
}
