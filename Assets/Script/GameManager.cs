using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    void Start()
    {
        // 시작 위치 저장
        p1StartPos = player1.position;
        p2StartPos = player2.position;
    }

    public void RespawnPlayers()
    {
        // 위치 초기화
        player1.position = p1StartPos;
        player2.position = p2StartPos;

        // 속도 초기화 (중요!)
        player1.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}