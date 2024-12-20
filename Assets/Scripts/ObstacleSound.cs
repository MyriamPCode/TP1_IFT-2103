using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySound : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player1;
    public Transform player2;
    public float maxDistance = 3f;

    private void Start()
    {
        // Check if player1 or player2 exists
        if (player1 == null && player2 == null)
        {
            Debug.Log("No players found. Disabling ProximitySound.");
            // Disable this script if no players are present
            this.enabled = false;
            return;
        }
    }


    private void Update()
    {
        // Calculate distances between the object and the players
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        // Find the closest distance
        float closestDistance = Mathf.Min(distanceToPlayer1, distanceToPlayer2);

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
