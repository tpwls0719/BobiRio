using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("패널")]
    public GameObject pausePanel;
    public GameObject clearPanel;

    // Animator 사용 안 함
    // private Animator pauseAnimator;

    void Start()
    {
        // pauseAnimator = pausePanel.GetComponent<Animator>();

        // 씬 시작 시 게임 정상 속도
        Time.timeScale = 1f;
    }

    // 일시정지 메뉴 열기
    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);

        // 애니메이션 제거
        // pauseAnimator.SetTrigger("Open");

        Time.timeScale = 0f;
    }

    // 일시정지 메뉴 닫기
    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    // 다음 스테이지 이동
    public void NextStage()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }

    // 다시 시작
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // 클리어 메뉴 열기
    public void OpenClearMenu()
    {
        clearPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}