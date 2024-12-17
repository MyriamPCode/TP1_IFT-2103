using UnityEngine;

public class FootstepSound : MonoBehaviour {
    public AudioSource audioSource; 
    public AudioClip[] footstepClips; 
    public float stepInterval = 1f;  
    public LayerMask groundLayer; 
    public float raycastDistance = 0.6f; 
    private float stepTimer = 0f; 
    private Rigidbody2D rb; 
    private bool isGrounded = false; 

    void Start() {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update() {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        // 如果小球有运动并且在地面上
        if (isGrounded && rb.velocity.magnitude > 0.01f) {
            stepTimer -= Time.deltaTime;

            // 只有当计时器为0时播放脚步声
            if (stepTimer <= 0f) {
                PlayFootstepSound();
                // 根据小球的速度调整步伐播放间隔
                stepTimer = Mathf.Max(stepInterval / rb.velocity.magnitude, 0.5f);  // 保证步伐间隔不会小于0.2秒
            }
        }
    }

    void PlayFootstepSound() {
        if (footstepClips == null || footstepClips.Length == 0) {
            Debug.LogWarning("没有脚步声音效文件，请检查 footstepClips 数组！");
            return;
        }

        // 随机选择一个脚步音效并播放
        int index = Random.Range(0, footstepClips.Length);
        audioSource.clip = footstepClips[index];
        audioSource.Play();
    }

    // 用于调试 Raycast 射线
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
    }
}
