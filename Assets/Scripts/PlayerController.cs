using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    private float moveInput;

    // Pour détecter si le joueur est au sol
    public LayerMask groundMask;
    private bool isGrounded;

    public string horizontalInputAction = "Horizontal"; // A configurer dans le Input Manager pour chaque joueur
    public string jumpInputAction = "Jump";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Vérifier si le joueur est au sol
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        // Récupérer l'entrée pour le mouvement horizontal
        moveInput = Input.GetAxis(horizontalInputAction);

        // Si le joueur appuie sur la touche de saut et est au sol, sauter
        if (Input.GetButtonDown(jumpInputAction) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        // Appliquer le mouvement horizontal
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}

