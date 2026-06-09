using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public bool moveRight = true;

    private bool isMoving = false;
    private bool isStopped = false;

    private Rigidbody2D rb;
    private Vector2 lastPosition;

    private Transform playerOnPlatform;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
void FixedUpdate()
{
    if (!isMoving || isStopped)
        return;

    lastPosition = rb.position;

    Vector2 direction = moveRight ? Vector2.right : Vector2.left;
    Vector2 move = direction * moveSpeed * Time.fixedDeltaTime;

    rb.MovePosition(rb.position + move);

    if (playerOnPlatform != null)
    {
        playerOnPlatform.position += (Vector3)move;
    }
}
    public void ResetPlatform()
{
    gameObject.SetActive(true);

    rb.position = startPosition;

    rb.linearVelocity = Vector2.zero;

    isMoving = false;
    isStopped = false;

    playerOnPlatform = null;
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 밟으면 이동 시작
        if (collision.gameObject.CompareTag("Bobi") ||
            collision.gameObject.CompareTag("Rio"))
        {
            isMoving = true;
            playerOnPlatform = collision.transform;
        }

        // FallingDisappearPlatform에 닿으면 정지
        FallingDisappearPlatform stopPlatform =
            collision.gameObject.GetComponent<FallingDisappearPlatform>();

        if (stopPlatform != null && stopPlatform.stopMovingPlatform)
        {
            rb.position = lastPosition;
            isStopped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == playerOnPlatform)
        {
            playerOnPlatform = null;
        }
    }
}