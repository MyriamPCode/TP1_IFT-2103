using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -9.81f;      
    public float jumpForce = 5f;        
    public LayerMask groundLayer;        

    private float verticalVelocity = 0f; 
    private bool isGrounded;


    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector2(horizontalMovement, 0f));
    }
    void Update()
    {
        // Vérifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        // Appliquer la gravité
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump")) // La touche espace
            {
                verticalVelocity = jumpForce; // Applique la force de saut
            }
            else
            {
                verticalVelocity = 0; 
            }
        }
        else
        {
            // Appliquer la gravité
            verticalVelocity += gravity * Time.deltaTime; // La gravité continue à s'appliquer
        }

        // Appliquer le mouvement vertical
        transform.Translate(new Vector2(0, verticalVelocity * Time.deltaTime));


        /*
        // Réinitialiser la position si le joueur est tombé en dessous d'un certain seuil 
        if (transform.position.y < -10f)
        {
            transform.position = new Vector2(transform.position.x, 0);
            verticalVelocity = 0;
        }
        */

        // Appliquer une force normale si le joueur est au sol
        if (isGrounded && verticalVelocity < 0)
        {
            // Simuler une force normale en réajustant la position
            transform.position = new Vector2(transform.position.x, Mathf.Floor(transform.position.y));
            verticalVelocity = 0; // Réinitialiser la vitesse verticale pour éviter un rebond
        }
    }
}
