using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    [Header("필요한 열쇠 개수")]
    public int totalKeys = 3;

    [Header("맵의 모든 열쇠")]
    public GameObject[] keys;

    private int currentKeys = 0;



    void Start()
    {
        p1StartPos = player1.position;
        p2StartPos = player2.position;

    }

    public void GetKey()
    {
        currentKeys++;

        Debug.Log("열쇠 획득! (" + currentKeys + "/" + totalKeys + ")");
    }

    public bool HasAllKeys()
    {
        return currentKeys >= totalKeys;
    }

    public int GetCurrentKeys()
    {
        return currentKeys;
    }

    public void RespawnPlayers()
    {
        // 위치 초기화
        player1.position = p1StartPos;
        player2.position = p2StartPos;

        // 속도 초기화
        player1.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // 중력 상태 초기화 ⭐
        player1.GetComponent<PlayerController>().ResetState();
        player2.GetComponent<PlayerController>().ResetState();

        // 열쇠 초기화
        currentKeys = 0;

        foreach (GameObject key in keys)
        {
            key.SetActive(true);
        }

        Debug.Log("리스폰 완료");
    }
}