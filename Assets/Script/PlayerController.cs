using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Ability")]
    public bool canPush = false; // 밀기 가능 여부

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isPlayer1; // Bobi = true / Rio = false

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 태그 기반 자동 설정
        if (CompareTag("Bobi"))
        {
            isPlayer1 = true;
            canPush = true; // ⭐ Bobi만 밀기 가능
        }
        else if (CompareTag("Rio"))
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

    void Move()
    {
        float moveInput = 0f;

        if (CompareTag("Bobi"))
        {
            // 방향키 (Bobi)
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput += 1f;
        }
        else
        {
            // WASD (Rio)
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

    // ⭐ 밀기 기능 (나중에 확장용)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canPush) return;

        if (collision.gameObject.CompareTag("Pushable"))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
