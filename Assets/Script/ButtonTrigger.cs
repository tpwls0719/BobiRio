using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Animator pathAnimator;

    void Start()
    {
        pathAnimator.SetBool("isPressed", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
        {
            pathAnimator.SetBool("isPressed", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
        {
            pathAnimator.SetBool("isPressed", false);
        }
    }
}