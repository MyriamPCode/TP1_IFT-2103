using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Reference to the AudioSource components for both players' footstep sounds
    public AudioSource player1FootstepsSound;
    public AudioSource player2FootstepsSound;
    private Player1 player1; // Player 1's script
    private Player2 player2; // Player 2's script
    
    private void Awake()
    {
        Debug.Log("Footstep manager started");
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();

        if (player1 == null && player2 == null)
        {
            Debug.LogWarning("No players found. Switching to default sound behavior.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update Player 1's footsteps
        if (player1 != null && player1FootstepsSound != null)
        {
            UpdateFootstepSound(player1, player1FootstepsSound, KeyCode.A, KeyCode.D, -1f, "Player 1");
        }
        else
        {
            Debug.LogWarning("Player 1 or its footstep sound is missing");
        }

        // Update Player 2's footsteps
        if (player2 != null && player2FootstepsSound != null)
        {
            UpdateFootstepSound(player2, player2FootstepsSound, KeyCode.LeftArrow, KeyCode.RightArrow, 1f, "Player 2");
        }
        else
        {
            Debug.LogWarning("Player 2 or its footstep sound is missing");
        }
    }

    private void UpdateFootstepSound(object player, AudioSource footstepsSound, KeyCode leftKey, KeyCode rightKey, float defaultPan, string playerName)
    {
        // Cast player to the appropriate type and check the isGrounded property
        bool isGrounded = false;
        if (player is Player1 p1)
        {
            isGrounded = p1.isGrounded;
        }
        else if (player is Player2 p2)
        {
            isGrounded = p2.isGrounded;
        }

        // Check if the player is on the ground and pressing movement keys
        if (isGrounded && (Input.GetKey(leftKey) || Input.GetKey(rightKey)))
        {
            // Enable the AudioSource to play the footstep sound
            footstepsSound.enabled = true;

            // Set stereo pan based on the player's horizontal input
            if (Input.GetKey(leftKey))
            {
                footstepsSound.panStereo = -1f; // Full left channel
            }
            else if (Input.GetKey(rightKey))
            {
                footstepsSound.panStereo = 1f; // Full right channel
            }
            else
            {
                footstepsSound.panStereo = defaultPan; // Default channel
            }
        }
        else
        {

            footstepsSound.enabled = false;
        }
    }
}
