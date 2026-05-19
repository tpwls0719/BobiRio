using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float fallDelay = 0.2f;
    public float resetTime = 3f;

    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody2D rb;
    private Collider2D col;

    private Coroutine resetCoroutine;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player =
            collision.gameObject.GetComponent<PlayerController>();

        if (player == null)
            return;

        // 이미 리셋 코루틴 돌고 있으면 취소
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }

        StartCoroutine(Fall(player));
    }

    IEnumerator Fall(PlayerController player)
    {
        yield return new WaitForSeconds(fallDelay);

        // 발판 활성화
        rb.bodyType = RigidbodyType2D.Dynamic;

        // 🔥 플레이어 중력 방향 따라 변경
        rb.gravityScale = player.IsGravityDown() ? 1f : -1f;

        // 기존 속도 제거
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // 다시 리셋 예약
        resetCoroutine = StartCoroutine(ResetPlatform());
    }

    IEnumerator ResetPlatform()
    {
        yield return new WaitForSeconds(resetTime);

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.position = startPos;
        transform.rotation = startRot;
    }
}