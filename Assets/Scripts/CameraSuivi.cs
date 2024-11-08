using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuivi : MonoBehaviour
{
    public Transform player;  // Le joueur que cette caméra doit suivre
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        if (player != null)
        {
            Debug.Log(gameObject.name + " suit le joueur : " + player.name);
        }
        else
        {
            Debug.LogError(gameObject.name + " n'a pas de joueur assigné.");
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculer la position souhaitée de la caméra
            Vector3 desiredPosition = player.position + offset;
            // Lerp entre la position actuelle et la position souhaitée pour un mouvement fluide
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Appliquer la position calculée à la caméra
            transform.position = smoothedPosition;
        }
    }
}
