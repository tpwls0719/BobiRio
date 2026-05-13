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
            FindObjectOfType<GameManager>().GetKey(playerTag);

            // 삭제 대신 비활성화
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(gameObject.name + " 는 해당 플레이어가 획득할 수 없습니다.");
        }
    }
}