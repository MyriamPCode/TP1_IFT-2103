using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -9.81f;      
    public float jumpForce = 5f;        
    public LayerMask groundLayer;        

    private float verticalVelocity = 0f; 
    private bool isGrounded;             

    void Update()
    {
        // V�rifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        // Appliquer la gravit�
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
            // Appliquer la gravit�
            verticalVelocity += gravity * Time.deltaTime; // La gravit� continue � s'appliquer
        }

        // Appliquer le mouvement vertical
        transform.Translate(new Vector2(0, verticalVelocity * Time.deltaTime));


        /*
        // R�initialiser la position si le joueur est tomb� en dessous d'un certain seuil 
        if (transform.position.y < -10f)
        {
            transform.position = new Vector2(transform.position.x, 0);
            verticalVelocity = 0;
        }
        */
    }
}
