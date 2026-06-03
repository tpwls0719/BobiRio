using UnityEngine;

public class TwoPlayerFallPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;

    private Rigidbody2D rb;

    private bool bobiOn;
    private bool rioOn;
    private bool falling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 처음에는 안 떨어짐
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bobi"))
        {
            bobiOn = true;
        }

        if (collision.gameObject.CompareTag("Rio"))
        {
            rioOn = true;
        }

        // 둘 다 올라왔으면 떨어지기 시작
        if (bobiOn && rioOn && !falling)
        {
            falling = true;
            Invoke(nameof(StartFalling), fallDelay);
        }

        // 떨어지는 중 Ground에 닿으면 삭제
        if (falling && collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bobi"))
        {
            bobiOn = false;
        }

        if (collision.gameObject.CompareTag("Rio"))
        {
            rioOn = false;
        }
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}