using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public enum ButtonUser
    {
        Both,
        BobiOnly,
        RioOnly
    }

    [Header("누를 수 있는 캐릭터")]
    public ButtonUser buttonUser = ButtonUser.Both;

    [Header("버튼 애니메이션")]
    public Animator buttonAnimator;

    [Header("길 애니메이션")]
    public Animator pathAnimator;

    [Header("활성화할 오브젝트들")]
    public GameObject[] targetPlatforms;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanPress(collision.tag))
            return;

        if (buttonAnimator != null)
            buttonAnimator.SetBool("isPressed", true);

        if (pathAnimator != null)
            pathAnimator.SetBool("isPressed", true);

        foreach (GameObject obj in targetPlatforms)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.ButtonSound();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!CanPress(collision.tag))
            return;

        if (buttonAnimator != null)
            buttonAnimator.SetBool("isPressed", false);

        if (pathAnimator != null)
            pathAnimator.SetBool("isPressed", false);

        foreach (GameObject obj in targetPlatforms)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private bool CanPress(string tag)
    {
        switch (buttonUser)
        {
            case ButtonUser.Both:
                return tag == "Bobi" || tag == "Rio";

            case ButtonUser.BobiOnly:
                return tag == "Bobi";

            case ButtonUser.RioOnly:
                return tag == "Rio";
        }

        return false;
    }
}