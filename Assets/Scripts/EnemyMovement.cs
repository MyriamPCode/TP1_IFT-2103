using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public float fastMoveSpeed = 3f; 
    public float xMin = 16.14f;
    public float xMax = 18.452f;

    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    public TMP_Text deathMessage;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deadSound;


    private bool isFastState = false; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float speed = isFastState ? fastMoveSpeed : moveSpeed;

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= xMax)
            {
                movingRight = false;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= xMin)
            {
                movingRight = true;
                spriteRenderer.flipX = false;
            }
        }
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Respawn")) // 确保碰到的是玩家
    {
        // 判断玩家的相对速度是否是向下的
        if (collision.relativeVelocity.y < 0) // y 轴速度小于 0，说明玩家是从上往下碰到的
        {
            Debug.Log("Enemy destroyed from top!");
            SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 1f);
            Destroy(gameObject); // 销毁敌人自己
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
        deathMessage.text = "You Die ! ";
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
