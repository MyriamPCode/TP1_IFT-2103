using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIPlayer2 : MonoBehaviour
{
    public DifficultyLevel difficulty;
    public Transform target;
    public float moveSpeed = 1f;
    public float jumpForce = 3f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private List<Transform> path;
    private int currentPathIndex = 0;
    private bool isGrounded;

    public TextMeshProUGUI healthText;
    private Pathfinding pathfinding;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pathfinding = FindObjectOfType<Pathfinding>();
        rb.gravityScale = 3f;

        if (pathfinding != null && target != null)
        {
            Transform startNode = pathfinding.FindClosestNode(transform.position);
            Transform endNode = pathfinding.FindClosestNode(target.position);
            path = pathfinding.FindPath(startNode, endNode);
        }
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        if (difficulty == DifficultyLevel.Easy)
        {
            FollowPath();
        }
        else if (difficulty == DifficultyLevel.Hard)
        {
            FollowPath();
            AvoidPlayer();
        }
    }

    private void FollowPath()
    {
        if (path == null || currentPathIndex >= path.Count)
            return;

        Transform currentTarget = path[currentPathIndex];

        MoveTowards(currentTarget.position);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentPathIndex++;
        }
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (isGrounded && direction.y > 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AvoidPlayer()
    {
        HPlayer1 player = FindObjectOfType<HPlayer1>();
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 2f)
        {
            Vector2 avoidance = (Vector2)transform.position - (Vector2)player.transform.position;
            rb.velocity += avoidance.normalized * 2f;
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (pathfinding != null && target != null)
        {
            Transform startNode = pathfinding.FindClosestNode(transform.position);
            Transform endNode = pathfinding.FindClosestNode(target.position);
            path = pathfinding.FindPath(startNode, endNode);
            currentPathIndex = 0;
        }
    }
}
