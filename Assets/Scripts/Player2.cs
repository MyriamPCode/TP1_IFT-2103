using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player2 : MonoBehaviour
{
    public static Player2 Instance { get; private set; }
    public float moveSpeed = 1f;      
    public float jumpForce = 3f;        
    public LayerMask groundLayer;

    public bool isGrounded;
    private Rigidbody2D rb; 
    public float friction = 0.5f; 

    private Vector2 spawnPoint;
    private static int playerHealth;

    public LogicManager logic;

    public int playerIndex = 2;

    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 3f;
            rb.freezeRotation = true;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spawnPoint = transform.position;
        logic = FindObjectOfType<LogicManager>();
    }

    public bool IsGrounded() 
    {
        float rayLength = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        float horizontalMovement = GetCustomHorizontalInput();
        Vector2 velocity = rb.velocity;

        velocity.x = horizontalMovement * moveSpeed;
        rb.velocity = velocity;

        if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;  
        }
        else if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;  
        }

        if (horizontalMovement != 0)
        {
            velocity.x = horizontalMovement * moveSpeed;
            animator.SetBool("isWalking", true);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction * Time.fixedDeltaTime);
            animator.SetBool("isWalking", false);
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

            if (hit.CompareTag("Ennemy"))
            {
                TakeDamage();
                Destroy(hit.gameObject);
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
        playerHealth -= 1;
        playerHealth = Mathf.Max(playerHealth, 0);

        if (playerHealth == 0)
        {
            Respawn();
            playerHealth = 3;
        }
    }

}
