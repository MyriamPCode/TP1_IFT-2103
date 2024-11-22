using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public float moveSpeed = 1f;      
    public float jumpForce = 3f;        
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb; 

    public float friction = 0.5f; 

    private Vector2 spawnPoint;
    private static int playerHealth = 100;
    public  TextMeshProUGUI healthText;

    public LogicManager logic;

    public int playerIndex = 1;


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

        if (healthText != null)
        {
            healthText.text = $"Vie : {playerHealth}";
        }
    }

    private bool IsGrounded() 
    {
        float rayLength = 0.1f;
        Vector2 position = transform.position;

        RaycastHit2D hitCenter = Physics2D.Raycast(position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(position + new Vector2(-0.3f, 0), Vector2.down, rayLength, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(position + new Vector2(0.3f, 0), Vector2.down, rayLength, groundLayer);

        return hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        float horizontalMovement = GetCustomHorizontalInput();
        Vector2 velocity = rb.velocity;

        velocity.x = horizontalMovement * moveSpeed;

        if (horizontalMovement != 0)
        {
            velocity.x = horizontalMovement * moveSpeed;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction * Time.fixedDeltaTime);
        }

        rb.velocity = velocity;
    }

    void Update()
    {
        if (transform.position.y < -5f)
        {
            Respawn(); 
        }

        if (isGrounded && Input.GetKeyDown(GetJumpKey()))
        {
            Jump();
        }

        CheckCollisions();
        
        if (healthText != null)
        {
            healthText.text = $"Vie : {playerHealth}";
        }
    }

    private KeyCode GetJumpKey()
    {
        return InputManager.Instance.keyBindings.Find(kb => kb.actionName == "Jump" && kb.playerID == playerIndex).key;
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
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero;
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private float GetCustomHorizontalInput()
    {
        float horizontalMovement = 0f;

        foreach (var keyBinding in InputManager.Instance.keyBindings)
        {
            if (keyBinding.playerID == playerIndex) 
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

    public static int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void TakeDamage()
    {
        playerHealth -= 50;
        playerHealth = Mathf.Max(playerHealth, 0);

        if (playerHealth == 0)
        {
            Respawn();
            playerHealth = 100;
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Vie : {playerHealth}";
        }
    }
}
