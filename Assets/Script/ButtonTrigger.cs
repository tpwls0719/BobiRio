using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Animator pathAnimator;

    [Header("선택사항")]
    public GameObject targetPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bobi") &&
            !collision.CompareTag("Rio"))
            return;

        // 애니메이션 실행
        if (pathAnimator != null)
        {
            pathAnimator.SetBool("isPressed", true);
        }

        // 발판 활성화
        if (targetPlatform != null)
        {
            targetPlatform.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bobi") &&
            !collision.CompareTag("Rio"))
            return;

        // 애니메이션 복귀
        if (pathAnimator != null)
        {
            pathAnimator.SetBool("isPressed", false);
        }

        // 발판 비활성화
        if (targetPlatform != null)
        {
            targetPlatform.SetActive(false);
        }
    }
}