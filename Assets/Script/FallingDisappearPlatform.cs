using UnityEngine;
using System.Collections;

public class FallingRespawnPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float fallDelay = 0.2f;
    public float respawnTime = 2f;

    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private bool isTriggered = false;
    private bool isRespawning = false;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // 플레이어가 밟으면 떨어짐
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                StartCoroutine(Fall(player));
            }
        }

        // 떨어진 뒤 Ground에 닿으면 사라지고 리스폰 시작
        if (isTriggered && !isRespawning && collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(RespawnPlatform());
        }
    }

    IEnumerator Fall(PlayerController player)
    {
        isTriggered = true;

        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;

        // 플레이어 중력 방향 적용
        rb.gravityScale = player.IsGravityDown() ? 1f : -1f;
    }

    IEnumerator RespawnPlatform()
    {
        isRespawning = true;

        // 발판 숨기기
        sr.enabled = false;
        col.enabled = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(respawnTime);

        // 원래 위치/회전으로 복구
        transform.position = startPos;
        transform.rotation = startRot;

        // 다시 보이게
        sr.enabled = true;
        col.enabled = true;

        isTriggered = false;
        isRespawning = false;
    }
}