using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public float moveSpeed = 1f;
    public float gravity = -9.81f;      
    public float jumpForce = 3f;        
    public LayerMask groundLayer;

    public float radius = 0.5f;

    private float verticalVelocity = 0f; 
    private bool isGrounded;
    private Rigidbody2D rb; 

    public float friction = 0.9f; 

    private Vector2 spawnPoint;

    public LogicManager logic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Implémentation du RigidBody2D
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        rb.freezeRotation = true; // desactiver la rotation
        rb.mass = 2;
        rb.drag = 2;
        spawnPoint = transform.position; // Initialisation du point d'apparition 

        logic = FindObjectOfType<LogicManager>();

        /*
        // Le texte de victoire est masqué au départ
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false);
        }
        */
    }

    private bool IsGrounded() 
    {
    // Longueur du rayon qui va être projeté vers le bas
    float rayLength = 0.05f;

    // Projeter un rayon vers le bas à partir de la position du joueur avec un léger décalage
    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, rayLength, groundLayer);

    // Si le rayon touche un objet appartenant à la couche du sol, renvoyer true, sinon false
    return hit.collider != null;
    }

    private void FixedUpdate()
    {
        // On vérifie si le joueur est au sol
        isGrounded = IsGrounded();

        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;

        if (isGrounded)
        {
            if (moveSpeed < 0.1f)
            {
                //Debug.Log("velocité avant friction immo:" + velocity.x);
                velocity.x *= friction; // On applique le frottement statique
                //Debug.Log("velocité apres friction immo:" + velocity.x);
            }
            else
            {
                //Debug.Log("velocité avant friction dynamique:" + velocity.x);
                velocity.x = horizontalMovement * moveSpeed; 
                velocity.x *= friction; // On applique le frottement dynamique
                //Debug.Log("velocité apres friction dynamique:" + velocity.x);
            }
        }
        else
        {
            velocity.x = horizontalMovement * moveSpeed;
            velocity.x *= friction;
        }

        // On applique la gravité
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity = 0; 
        }

        velocity.y += verticalVelocity;

        rb.velocity = velocity;
    }

    void Update()
    {
        // À partir de y = -10, le joueur retourne au point d'apparition
        if (transform.position.y < -10f)
        {
            Respawn(); 
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        CheckCollisions();
    }


    private void CheckCollisions()
    {
        // Initialisation du centre du cercle (position du joueur)
        Vector2 ballCenter = transform.position;

        foreach (Collider2D collider in FindObjectsOfType<Collider2D>())
        {
            if (collider.gameObject == gameObject)
                continue;

            Bounds bounds = collider.bounds;

            // On calcule le centre et la taille du rectangle
            Vector2 platformCenter = bounds.center;
            Vector2 platformSize = bounds.extents;

            // On trouve le point le plus proche du centre du cercle par rapport au rectangle
            float closestX = Mathf.Clamp(ballCenter.x, platformCenter.x - platformSize.x, platformCenter.x + platformSize.x);
            float closestY = Mathf.Clamp(ballCenter.y, platformCenter.y - platformSize.y, platformCenter.y + platformSize.y);

            // Calcul de la distance entre le point le plus proche et le centre du cercle
            float distanceX = ballCenter.x - closestX;
            float distanceY = ballCenter.y - closestY;
            float distanceSquared = distanceX * distanceX + distanceY * distanceY;

            // Si la distance est inférieure au rayon du cercle, cela signifie qu'il y a collision
            if (distanceSquared < radius * radius)
            {
                if (collider.CompareTag("Finish"))
                {
                    logic.Victory();
                }
            }
        }
    }


    private void Respawn()
    {
        Debug.Log("Respawn at spawn point");

        // On réinitialise la position du joueur au point de respawn
        transform.position = spawnPoint;

        // On réinitialise la vélocité pour éviter des mouvements résiduels
        rb.velocity = Vector2.zero;
        verticalVelocity = 0f;
    }

    private void OnDrawGizmos()
    {
        // Pour visualiser le collider
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
