using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player1;
    public Transform player2;

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    [Header("필요한 열쇠 개수")]
    public int totalKeys = 3;

    [Header("맵의 모든 열쇠")]
    public GameObject[] keys;

    [Header("UI")]
    public UIManager uiManager;

    private int currentKeys = 0;

    // 골 상태
    private bool p1Goal = false;
    private bool p2Goal = false;

    [Header("이동 플랫폼")]
    public MovingPlatform[] movingPlatforms;

    void Start()
{
    p1StartPos = player1.position;
    p2StartPos = player2.position;

    // 열쇠 개수 자동 설정
    totalKeys = keys.Length;
}

    // 열쇠 획득
    public void GetKey(string playerTag)
    {
        currentKeys++;

        Debug.Log(playerTag + " 가 열쇠 획득! (" + currentKeys + "/" + totalKeys + ")");
    }

    // 열쇠 전부 모았는지
    public bool HasAllKeys()
    {
        return currentKeys >= totalKeys;
    }

    public int GetCurrentKeys()
    {
        return currentKeys;
    }

    // 골 상태 업데이트
    public void SetGoalState(string playerTag, bool isInside)
    {
        if (playerTag == "Bobi")
        {
            p1Goal = isInside;
        }
        else if (playerTag == "Rio")
        {
            p2Goal = isInside;
        }

        CheckClear();
    }

    // 클리어 확인
    void CheckClear()
    {
        // 열쇠 부족
        if (!HasAllKeys())
        {
            Debug.Log("열쇠를 모두 모으세요!");
            return;
        }

        // 두 플레이어 모두 골 도착
        if (p1Goal && p2Goal)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayGoal();
            }

            Debug.Log("스테이지 클리어!");

            uiManager.OpenClearMenu();
        }
    }

    // 리스폰
    public void RespawnPlayers()
    {
        // 위치 초기화
        player1.position = p1StartPos;
        player2.position = p2StartPos;

        // 속도 초기화
        player1.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // 상태 초기화
        player1.GetComponent<PlayerController>().ResetState();
        player2.GetComponent<PlayerController>().ResetState();

        // 열쇠 초기화
        currentKeys = 0;

        foreach (GameObject key in keys)
        {
            key.SetActive(true);
        }

        // 골 상태 초기화
        p1Goal = false;
        p2Goal = false;

        Debug.Log("리스폰 완료");

        foreach (MovingPlatform platform in movingPlatforms)
{
    platform.ResetPlatform();
}
    }
    
}