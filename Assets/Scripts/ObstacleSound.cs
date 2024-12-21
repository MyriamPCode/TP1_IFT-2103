using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySound : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the audio source
    public Transform player1;        // Reference to Player1
    public Transform player2;        // Reference to Player2
    public Transform obstacle;       // Reference to the Obstacle (Target Position)
    public float maxDistance = 3f;   // Maximum distance for the sound to play

    private void Start()
    {
        // Validate AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned. Disabling ProximitySound.");
            this.enabled = false;
            return;
        }

        // Check if at least one player exists
        if (player1 == null && player2 == null)
        {
            Debug.LogWarning("No players found. Disabling ProximitySound.");
            this.enabled = false;
            return;
        }

        // Check if obstacle is assigned
        if (obstacle == null)
        {
            Debug.LogError("Obstacle is not assigned. Disabling ProximitySound.");
            this.enabled = false;
            return;
        }

        Debug.Log($"ProximitySound initialized for object: {obstacle.name}");
        Debug.Log($"Obstacle Position: {obstacle.position}");
    }

    private void Update()
    {
        // Ensure AudioSource is available
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing. Skipping Update.");
            return;
        }

        // Validate player positions
        if (player1 == null && player2 == null)
        {
            Debug.LogWarning("Both players are missing. Disabling ProximitySound.");
            this.enabled = false;
            return;
        }

        // Log player and obstacle positions
        Debug.Log($"Player1 Position: {player1.position}");
        Debug.Log($"Player2 Position: {player2.position}");
        Debug.Log($"Obstacle Position: {obstacle.position}");

        // Calculate distances to players
        float distanceToPlayer1 = player1 != null ? Vector3.Distance(obstacle.position, player1.position) : float.MaxValue;
        float distanceToPlayer2 = player2 != null ? Vector3.Distance(obstacle.position, player2.position) : float.MaxValue;

        // Determine the closest distance
        float closestDistance = Mathf.Min(distanceToPlayer1, distanceToPlayer2);

        Debug.Log($"Closest distance to players: {closestDistance}");

        // Adjust volume and play sound if within range
        if (closestDistance <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Adjust volume based on distance
            float volume = Mathf.Clamp01(1 - (closestDistance / maxDistance));
            audioSource.volume = volume;
        }
        else
        {
            // Stop sound if out of range
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
