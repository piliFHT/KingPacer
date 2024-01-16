using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 15.0f; // Adjust the speed as needed
    [System.NonSerialized]
    private Animator animator;
    private Rigidbody2D rb;
    private Transform currentPoint; // Store the player's transform
    public Transform pointA;
    public Transform pointB;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
        animator.SetBool("EnemyWalk", true);
    }

    void Update()
    {
        GoToNextLocation();
    }

    public void GoToNextLocation()
    {
        // Move towards the current target point
           transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the current target point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            // Switch target point
            if (currentPoint == pointA)
            {
                currentPoint = pointB;
                sr.flipX=true; // Flip the enemy when changing direction
            }
            else
            {
                currentPoint = pointA;
                sr.flipX = false; // Flip the enemy when changing direction
            }
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
