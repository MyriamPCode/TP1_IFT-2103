using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerHUD playerHUD;
    public float moveSpeed = 3f;      
    public float jumpForce = 5f;        
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb; 

    public float friction = 0.5f; 

    private Vector2 spawnPoint;
    private static int playerHealth = 3;
    public PlayerDamageFlash playerDamageFlash;

    public LogicManager logic;

    public int playerIndex;

    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (player != this && player.playerIndex == this.playerIndex)
            {
                Destroy(gameObject);
                Debug.Log($"Clone du joueur {playerIndex} détruit.");
                return;
            }
        }
    }

    private void Start()
    {
        spawnPoint = transform.position;
        logic = FindObjectOfType<LogicManager>();
        playerDamageFlash = GetComponent<PlayerDamageFlash>();
    }

    private bool IsGrounded() 
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
                playerHUD.TakeDamage();
                Destroy(hit.gameObject);
            }
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero;
        playerDamageFlash.FlashOnRespawn();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Applique une force verticale
        animator.SetTrigger("Jump"); // Déclenche l'animation de saut si nécessaire
    }


    public static int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void TakeDamage()
    {
        playerHealth -= 50;
        playerHealth = Mathf.Max(playerHealth, 0);

        playerDamageFlash.FlashOnDamage();

        if (playerHealth == 0)
        {
            Respawn();
            playerHealth = 100;
        }
    }

}
