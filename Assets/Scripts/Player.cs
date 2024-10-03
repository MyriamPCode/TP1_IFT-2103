using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -9.81f;      
    public float jumpForce = 5f;        
    public LayerMask groundLayer;

    public float radius = 0.5f;


    private float verticalVelocity = 0f; 
    private bool isGrounded;
    private Rigidbody2D rb; // Declaration du Rigidbody2D

    private void Start()
    {
        // Ajouter un Rigidbody2D par code
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // Desactiver la gravite par defaut
        rb.freezeRotation = true; // desactiver la rotation
        rb.mass = 2;
        rb.drag = 2;
    }

    private void FixedUpdate()
    {
        // Vérifier si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.16f, 0), 0.17f, groundLayer);

        // Gérer le mouvement horizontal
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;
        velocity.x = horizontalMovement * moveSpeed;
        rb.velocity = velocity;

        // Gérer la gravité
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
        }
        else
        {
            verticalVelocity = 0; // Réinitialiser la vitesse verticale si au sol
        }
    }

    void Update()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        Debug.Log("Is Grounded: " + isGrounded + " Position: " + transform.position);

        CheckCollisions();
    }



    private void CheckCollisions()
    {
        // R�cup�rer la position du centre du collider
        Vector2 center = transform.position;
        //Vector2 center = (Vector2)transform.position + new Vector2(0, -radius);

        // V�rifier les collisions avec d'autres objets
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Ignorer le collider du joueur
            {
                Debug.Log("Collision avec : " + collider.gameObject.name);
                // Logique de collision ici (ex: r�duire la vie, rebondir, etc.)
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualiser le collider dans l'�diteur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
