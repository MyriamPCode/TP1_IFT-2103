using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;  
    public float xMin = 16.14f;  
    public float xMax = 18.452f;   

    private bool movingRight = true;  
    private SpriteRenderer spriteRenderer;
    public TMP_Text deathMessage;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  
    } 

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= xMax) 
            {
                movingRight = false;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= xMin)  
            {
                movingRight = true;
                spriteRenderer.flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn")) 
        {
            Debug.Log("Player die, restart");
            ShowDeathMessage();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
}
