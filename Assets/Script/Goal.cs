using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("전용 골 여부")]
    public bool useSpecificPlayer = false;

    [Header("들어올 수 있는 플레이어")]
    public string allowedTag;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager == null)
            return;

        // 공용 골
        if (!useSpecificPlayer)
        {
            if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
            {
                gameManager.SetGoalState(collision.tag, true);
            }
        }
        // 전용 골
        else
        {
            if (collision.CompareTag(allowedTag))
            {
                gameManager.SetGoalState(collision.tag, true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameManager == null)
            return;

        // 공용 골
        if (!useSpecificPlayer)
        {
            if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
            {
                gameManager.SetGoalState(collision.tag, false);
            }
        }
        // 전용 골
        else
        {
            if (collision.CompareTag(allowedTag))
            {
                gameManager.SetGoalState(collision.tag, false);
            }
        }
    }
}