using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
        {
            FindObjectOfType<GameManager>().GetKey();

            // 삭제 대신 비활성화
            gameObject.SetActive(false);
        }
    }
}