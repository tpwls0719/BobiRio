using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Key Owner")]
    public bool canBobiGet = true;
    public bool canRioGet = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerTag = collision.tag;

        bool canGet =
            (playerTag == "Bobi" && canBobiGet) ||
            (playerTag == "Rio" && canRioGet);

        // 획득 가능
        if (canGet)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayKey();
            }

            FindObjectOfType<GameManager>().GetKey(playerTag);

            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(gameObject.name + " 는 해당 플레이어가 획득할 수 없습니다.");
        }
    }
}