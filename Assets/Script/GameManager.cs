using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    // ⭐ 추가
    private bool hasKey = false;

    void Start()
    {
        p1StartPos = player1.position;
        p2StartPos = player2.position;
    }

    // ⭐ 키 획득
    public void GetKey()
    {
        hasKey = true;
        Debug.Log("열쇠 획득!");
    }

    // ⭐ 클리어 가능 여부
    public bool HasKey()
    {
        return hasKey;
    }

    public void RespawnPlayers()
    {
        player1.position = p1StartPos;
        player2.position = p2StartPos;

        player1.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // ⭐ 죽으면 키 초기화 (선택)
        hasKey = false;
    }
}