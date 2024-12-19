using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuivi : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError(gameObject.name + " n'a pas de joueur assign�.");
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
