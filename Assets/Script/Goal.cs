using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool p1In = false;
    private bool p2In = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi"))
        {
            p1In = true;
        }
        else if (collision.CompareTag("Rio"))
        {
            p2In = true;
        }

        CheckClear();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi"))
        {
            p1In = false;
        }
        else if (collision.CompareTag("Rio"))
        {
            p2In = false;
        }
    }

    void CheckClear()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (p1In && p2In && gm.HasKey())
        {
            Debug.Log("클리어!");
            // 👉 여기서 씬 이동, UI 표시 등 가능
        }
        else if(!gm.HasKey())
        {
            Debug.Log("열쇠를 가지고 오세요!");
        }
    }
}