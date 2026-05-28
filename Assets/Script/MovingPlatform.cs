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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 충돌 정확도 향상
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        if (!isMoving || isStopped)
            return;

        // 이전 위치 저장
        lastPosition = rb.position;

        Vector2 direction = moveRight ? Vector2.right : Vector2.left;
        Vector2 move = direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        // 플레이어도 같이 이동
        if (playerOnPlatform != null)
        {
            playerOnPlatform.position += (Vector3)move;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Wall"))
    {
        Destroy(gameObject);
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