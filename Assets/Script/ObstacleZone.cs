using UnityEngine;
using System.Collections;

public class ObstacleZone : MonoBehaviour
{
    public GameObject[] obstacles;

    // 장애물이 켜져있는 시간
    public float activeTime = 1.5f;

    // 다시 생성되기까지 대기 시간
    public float cooldownTime = 3f;

    private bool isRunning = false;
    private bool isCooldown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 감지
        if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
        {
            // 실행중이거나 쿨타임이면 무시
            if (isRunning || isCooldown)
                return;

            StartCoroutine(ActivateObstacles());
        }
    }

    IEnumerator ActivateObstacles()
    {
        isRunning = true;

        // 장애물 활성화
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }

        // 장애물 유지 시간
        yield return new WaitForSeconds(activeTime);

        // 장애물 비활성화
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }

        isRunning = false;

        // 🔥 쿨타임 시작
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}