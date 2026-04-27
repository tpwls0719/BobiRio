using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.2f;
    public float respawnTime = 2f;

    private Vector3 startPos;
    private Quaternion startRot; // ⭐ 회전 저장

    private Rigidbody2D rb;
    private Collider2D col;

    private bool isTriggered = false;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation; // ⭐ 초기 회전 저장

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTriggered) return;

        if (collision.gameObject.CompareTag("Bobi") ||
            collision.gameObject.CompareTag("Rio"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        isTriggered = true;

        yield return new WaitForSeconds(fallDelay);

        // 떨어지기
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(respawnTime);

        // ⭐ 완전 초기화
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.position = startPos;
        transform.rotation = startRot; // ⭐ 회전 복구

        isTriggered = false;
    }
}