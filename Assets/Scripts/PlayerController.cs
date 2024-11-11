using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public int playerID = 1; // Identifiant du joueur (1 pour le premier, 2 pour le deuxième)
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Vector2 spawnPoint;
    private float friction = 0.5f;

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

        spawnPoint = transform.position;
    }

    private void Start()
    {
        // Si nécessaire, charger les contrôles du joueur à partir d'une configuration
        // par exemple, InputManager.Instance.LoadKeyBindings();
    }

    private bool IsGrounded()
    {
        float rayLength = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        // Vérifier si le joueur est au sol
        isGrounded = IsGrounded();

        // Obtenir l'entrée du joueur pour le mouvement horizontal
        float horizontalMovement = GetHorizontalInput();
        Vector2 velocity = rb.velocity;

        // Appliquer la vitesse de déplacement sur l'axe X
        velocity.x = horizontalMovement * moveSpeed;

        if (horizontalMovement == 0)
        {
            // Appliquer la friction si le joueur ne bouge pas
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction * Time.fixedDeltaTime);
        }

        rb.velocity = velocity;
    }

    void Update()
    {
        // Si le joueur tombe trop bas, respawn
        if (transform.position.y < -10f)
        {
            Respawn();
        }

        // Vérifier si le joueur appuie sur la touche de saut
        if (isGrounded && Input.GetKeyDown(GetJumpKey()))
        {
            Jump();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero;
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Obtenir la touche de déplacement horizontal en fonction du playerID
    private float GetHorizontalInput()
    {
        float horizontalMovement = 0f;

        KeyCode moveLeftKey = InputManager.Instance.keyBindings.Find(kb => kb.actionName == "MoveLeft" && kb.playerID == playerID).key;
        KeyCode moveRightKey = InputManager.Instance.keyBindings.Find(kb => kb.actionName == "MoveRight" && kb.playerID == playerID).key;

        if (Input.GetKey(moveLeftKey))
        {
            horizontalMovement = -1f;
        }
        else if (Input.GetKey(moveRightKey))
        {
            horizontalMovement = 1f;
        }

        return horizontalMovement;
    }

    // Obtenir la touche de saut en fonction du playerID
    private KeyCode GetJumpKey()
    {
        return InputManager.Instance.keyBindings.Find(kb => kb.actionName == "Jump" && kb.playerID == playerID).key;
    }
}

