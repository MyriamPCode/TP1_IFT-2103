using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static Player2 Instance { get; private set; }
    public float moveSpeed = 1f;
    public float jumpForce = 3f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb;

    public float friction = 0.5f;

    private Vector2 spawnPoint;

    public LogicManager logic;

    public int playerIndex = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 3f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        spawnPoint = transform.position; // Initialisation du point d'apparition 

        logic = FindObjectOfType<LogicManager>();

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

        float horizontalMovement = GetCustomHorizontalInput();
        Vector2 velocity = rb.velocity;

        velocity.x = horizontalMovement * moveSpeed;
        rb.velocity = velocity;


        if (horizontalMovement != 0)
        {
            // Applique le mouvement horizontal normal
            velocity.x = horizontalMovement * moveSpeed;
        }
        else
        {
            // Si le joueur n'est pas en mouvement, appliquer la friction pour ralentir progressivement la vitesse horizontale
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction * Time.fixedDeltaTime);
        }

        rb.velocity = velocity;

    }

    void Update()
    {
        // À partir de y = -10, le joueur retourne au point d'apparition
        if (transform.position.y < -10f)
        {
            Respawn();
        }


        foreach (var binding in InputManager.Instance.keyBindings)
        {
            if (binding.playerID == playerIndex && binding.actionName == "Jump" && Input.GetKeyDown(binding.key))
            {
                Jump();
                break; // On ne veut pas que le joueur saute plus d'une fois par appui de touche
            }
        }


        CheckCollisions();
    }


    private void CheckCollisions()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f, groundLayer);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Finish"))
            {
                logic.Victory();
                break;
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
        //verticalVelocity = 0f;
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private float GetCustomHorizontalInput()
    {
        float horizontalMovement = 0f;

        /*
        if (Input.GetKey(KeyCode.LeftArrow))  // Joueur 2, touche flèche gauche
            horizontalMovement = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) // Joueur 2, touche flèche droite
                horizontalMovement = 1f;
        */
        foreach (var keyBinding in InputManager.Instance.keyBindings)
        {
            if (keyBinding.playerID == playerIndex) // Si la clé appartient au bon joueur
            {
                if (keyBinding.actionName == "MoveLeft" && Input.GetKey(keyBinding.key))
                {
                    horizontalMovement = -1f;
                }
                else if (keyBinding.actionName == "MoveRight" && Input.GetKey(keyBinding.key))
                {
                    horizontalMovement = 1f;
                }
            }
        }
        return horizontalMovement;
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }
}
