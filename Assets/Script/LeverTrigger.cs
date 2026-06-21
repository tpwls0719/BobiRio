using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public enum LeverUser
    {
        Both,
        BobiOnly,
        RioOnly
    }

    [Header("사용 가능 캐릭터")]
    public LeverUser leverUser = LeverUser.Both;

    [Header("레버 애니메이션")]
    public Animator leverAnimator;

    [Header("활성화할 오브젝트")]
    public GameObject[] targetObjects;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        if (!CanUse(collision.tag))
            return;

        activated = true;

        if (leverAnimator != null)
            leverAnimator.SetTrigger("Pull");

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
    }

    private bool CanUse(string tag)
    {
        switch (leverUser)
        {
            case LeverUser.Both:
                return tag == "Bobi" || tag == "Rio";

            case LeverUser.BobiOnly:
                return tag == "Bobi";

            case LeverUser.RioOnly:
                return tag == "Rio";
        }

        return false;
    }
}