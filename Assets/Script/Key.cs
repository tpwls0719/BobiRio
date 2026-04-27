using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bobi") || collision.CompareTag("Rio"))
        {
            FindObjectOfType<GameManager>().GetKey();

            // 키 사라지게
            Destroy(gameObject);
        }
    }
}