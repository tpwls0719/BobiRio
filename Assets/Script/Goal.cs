using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("전용 골 여부")]
    public bool useSpecificPlayer = false;

    [Header("들어올 수 있는 플레이어")]
    public string allowedTag; // Bobi 또는 Rio

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 공용 골
        if (!useSpecificPlayer)
        {
            if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
            {
                FindObjectOfType<GameManager>()
                    .SetGoalState(collision.tag, true);
            }
        }
        // 전용 골
        else
        {
            if (collision.CompareTag(allowedTag))
            {
                FindObjectOfType<GameManager>()
                    .SetGoalState(collision.tag, true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 공용 골
        if (!useSpecificPlayer)
        {
            if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
            {
                FindObjectOfType<GameManager>()
                    .SetGoalState(collision.tag, false);
            }
        }
        // 전용 골
        else
        {
            if (collision.CompareTag(allowedTag))
            {
                FindObjectOfType<GameManager>()
                    .SetGoalState(collision.tag, false);
            }
        }
    }
}