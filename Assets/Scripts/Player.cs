using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float gravity = -9.81f;      
    public float jumpForce = 3f;        
    public LayerMask groundLayer;

    public float radius = 0.5f;

    private float verticalVelocity = 0f; 
    private bool isGrounded;
    private Rigidbody2D rb; // Declaration du Rigidbody2D

    public float friction = 40000f; // Frottement dynamique

    private Vector2 spawnPoint; // Position du point de spawn

    public TextMeshProUGUI victoryText; // Référence au texte UI

    private void Start()
    {
        // Ajouter un Rigidbody2D par code
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Desactiver la gravite par defaut
        rb.freezeRotation = true; // desactiver la rotation
        rb.mass = 2;
        rb.drag = 2;
        //rb.drag = 0;
        spawnPoint = transform.position; // Initialisation de point de spawn

        // Le texte de victoire est masqué au départ
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Vérifier si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.16f, 0), 0.17f, groundLayer);

        // Gérer le mouvement horizontal
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;

        // Appliquer le frottement
        if (isGrounded)
        {
            if (moveSpeed < 0.1f)
            {
                Debug.Log("velocité avant friction immo:" + velocity.x);
                velocity.x *= friction; // Appliquer le frottement si presque immobile
                Debug.Log("velocité apres friction immo:" + velocity.x);
            }
            else
            {
                Debug.Log("velocité avant friction dynamique:" + velocity.x);
                // Appliquer le frottement sur le mouvement
                velocity.x = horizontalMovement * moveSpeed; // Changer directement la vélocité
                velocity.x *= friction;
                Debug.Log("velocité apres friction dynamique:" + velocity.x);
            }
        }
        else
        {
            velocity.x = horizontalMovement * moveSpeed;
            velocity.x *= friction;
        }

        // Gérer la gravité
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity = 0; // Réinitialiser la vitesse verticale si au sol
        }
        // Appliquer la gravité accumulée
        velocity.y += verticalVelocity;

        // Appliquer les vitesses au Rigidbody
        rb.velocity = velocity;
    }

    void Update()
    {
        // Vérifier si le joueur est tombé sous un certain seuil
        if (transform.position.y < -10f)
        {
            Respawn(); // Appeler la fonction de respawn
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        CheckCollisions();
    }


    private void CheckCollisions()
    {
        // Recuperer la position du centre du collider
        Vector2 center = transform.position;
        //Vector2 center = (Vector2)transform.position + new Vector2(0, -radius);

        // Verifier les collisions avec d'autres objets
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Ignorer le collider du joueur
            {
                if (collider.CompareTag("Finish")) // Lorsque le joueur atteint le point de victoire
                {
                    Victory();
                }
                else
                {
                    //Debug.Log("Collision avec : " + collider.gameObject.name);
                    // Logique de collision ici (ex: reduire la vie, rebondir, etc.)
                }
            }
        }
    }

    private void Victory()
    {
        // On force la balle à s'arrêter
        rb.velocity = Vector2.zero;
        moveSpeed = 0f;
        jumpForce = 0f;
        
        // Activer et afficher le texte de victoire
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true);
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawn at spawn point");

        // Réinitialiser la position du joueur au point de respawn
        transform.position = spawnPoint;

        // Réinitialiser la vélocité pour éviter des mouvements résiduels
        rb.velocity = Vector2.zero;
        verticalVelocity = 0f;
    }

    private void OnDrawGizmos()
    {
        // Visualiser le collider dans l'editeur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
