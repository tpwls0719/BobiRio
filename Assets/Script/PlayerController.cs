using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Ability")]
    public bool canPush = false;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isPlayer1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // =========================
        // 플레이어 설정
        // =========================
        if (CompareTag("Bobi"))
        {
            isPlayer1 = true;
            canPush = true;
        }
        else if (CompareTag("Rio"))
        {
            isPlayer1 = false;
            canPush = false;
        }

        // ❌ 이거 제거해야 함 (중요)
        // Physics2D.IgnoreLayerCollision(...)

        // ✔ 대신: 플레이어끼리 "붙는 문제"는 마찰로 해결
        // (Unity에서 Physics Material 2D 적용해야 함)
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = 0f;

        if (CompareTag("Bobi"))
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput += 1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.A)) moveInput -= 1f;
            if (Input.GetKey(KeyCode.D)) moveInput += 1f;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (!isGrounded) return;

        if (CompareTag("Bobi"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    // =========================
    // 바닥 판정 (Ground + Player 포함)
    // =========================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGround(collision);

        // 밀기 기능
        if (canPush && collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (otherRb != null)
            {
                float moveInput = isPlayer1 ?
                    (Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : 0f) :
                    (Input.GetKey(KeyCode.LeftArrow) ? -1f : Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);

                otherRb.linearVelocity = new Vector2(moveInput * moveSpeed, otherRb.linearVelocity.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Pushable") ||
            collision.gameObject.CompareTag("Bobi") ||
            collision.gameObject.CompareTag("Rio"))
        {
            isGrounded = false;
        }
    }

    void CheckGround(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Pushable") ||
            collision.gameObject.CompareTag("Bobi") ||
            collision.gameObject.CompareTag("Rio")) // ⭐ 핵심 추가
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("사망!");
        FindObjectOfType<GameManager>().RespawnPlayers();
    }
}