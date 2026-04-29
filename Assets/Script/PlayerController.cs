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

    // 🔥 중력 상태 (true = 아래, false = 위)
    private bool gravityDown = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 👉 기본 중력 끄기 (필수)
        rb.gravityScale = 0f;

        if (CompareTag("Bobi"))
        {
            isPlayer1 = true;
            canPush = true;
        }
        else
        {
            isPlayer1 = false;
            canPush = false;
        }
    }

    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        // 🔥 중력 적용 (위/아래 토글)
        float gravity = Physics2D.gravity.y; // -9.81
        float dir = gravityDown ? 1f : -1f;

        rb.linearVelocity += new Vector2(0, gravity * dir * Time.fixedDeltaTime);
    }

    void Move()
    {
        float moveInput = 0f;

        if (isPlayer1)
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

        float dir = gravityDown ? 1f : -1f;

        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * dir);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * dir);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGround(collision);

        if (!collision.gameObject.CompareTag("Pushable")) return;

        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb == null) return;

        if (!canPush) return;

        float moveInput = 0f;

        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput += 1f;
        }

        if (Mathf.Abs(moveInput) < 0.01f) return;

        // 🔥 벽 체크
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            otherRb.position,
            otherRb.GetComponent<Collider2D>().bounds.size,
            0f
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Wall"))
            {
                return;
            }
        }

        // 👉 이동
        otherRb.MovePosition(
            otherRb.position + new Vector2(moveInput * moveSpeed * Time.deltaTime, 0)
        );
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
            collision.gameObject.CompareTag("Rio"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (gravityDown)
                {
                    if (contact.normal.y > 0.5f)
                    {
                        isGrounded = true;
                        return;
                    }
                }
                else
                {
                    if (contact.normal.y < -0.5f)
                    {
                        isGrounded = true;
                        return;
                    }
                }
            }
        }
    }

    // 🔥 중력 토글
    public void ToggleGravity()
    {
        gravityDown = !gravityDown;

        // (선택) 캐릭터 뒤집기
        transform.rotation = gravityDown
            ? Quaternion.identity
            : Quaternion.Euler(0, 0, 180);
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