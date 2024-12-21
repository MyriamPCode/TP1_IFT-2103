using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fastMoveSpeed = 3f;

    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    public TMP_Text deathMessage;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deadSound;

    private bool isFastState = false;

    [Header("Raycast Parameters")]
    public float groundCheckDistance = 0.1f; // Distance pour vérifier le sol
    public LayerMask groundLayer; // La couche utilisée pour détecter les plateformes

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float speed = isFastState ? fastMoveSpeed : moveSpeed;
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        transform.Translate(direction * speed * Time.deltaTime);

        if (!IsGroundAhead())
        {
            FlipDirection();
        }
    }

    bool IsGroundAhead()
    {
        Vector2 raycastOrigin = movingRight ? 
            new Vector2(transform.position.x + 0.5f, transform.position.y) : 
            new Vector2(transform.position.x - 0.5f, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(raycastOrigin, Vector2.down * groundCheckDistance, Color.red);

        return hit.collider != null;
    }

    void FlipDirection()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if (collision.relativeVelocity.y < 0)
            {
                Debug.Log("Enemy destroyed from top!");
                SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 1f);
                Destroy(gameObject);
                EnemyManager.instance.EnemyDestroyed();
            }
            else
            {
                Debug.Log("Player died, hit enemy body.");
                ShowDeathMessage();
                SoundFXManager.instance.PlaySoundFXClip(deadSound, transform, 1f);
                Invoke("RestartScene", deadSound.length);
            }
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ShowDeathMessage()
    {
        deathMessage.text = "You Die!";
        deathMessage.gameObject.SetActive(true);
        Invoke("HideDeathMessage", 1.5f);
    }

    void HideDeathMessage()
    {
        deathMessage.gameObject.SetActive(false);
    }

    public void SwitchToFastState()
    {
        isFastState = true;
        Debug.Log("Switching to fast state!");
    }
}
