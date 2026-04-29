using UnityEngine;

public class Pushable : MonoBehaviour
{
    public float gravity = -20f;
    public float slideSpeed = 5f;
    public float groundCheckDistance = 0.2f;

    private float velocityY = 0f;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance
        );

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            // 👉 바닥에 닿으면
            velocityY = 0f;

            // 🔥 경사면 방향 계산
            Vector2 normal = hit.normal;

            // 경사가 있으면 옆으로 미끄러짐
            if (normal.x != 0)
            {
                float slide = -normal.x * slideSpeed;
                transform.position += new Vector3(slide * Time.fixedDeltaTime, 0, 0);
            }
        }
        else
        {
            // 공중이면 중력 적용
            velocityY += gravity * Time.fixedDeltaTime;
            transform.position += new Vector3(0, velocityY * Time.fixedDeltaTime, 0);
        }
    }
}