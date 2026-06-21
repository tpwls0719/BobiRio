using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("패널")]
    public GameObject pausePanel;
    public GameObject clearPanel;

    [Header("클리어 시간")]
public TMP_Text clearTimeText;
    

    void Start()
    {
        Time.timeScale = 1f;
    }

    // 일시정지 메뉴 열기
    public void OpenPauseMenu()
    {
        AudioManager.Instance.PlayButtonClick();

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 일시정지 메뉴 닫기
    public void ClosePauseMenu()
    {
        AudioManager.Instance.PlayButtonClick();

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // 다음 스테이지 이동
    public void GoToStageSelect()
    {
        AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f;

        int currentStage = SceneManager.GetActiveScene().buildIndex;

        if (currentStage == 6) // Stage5
        {
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            SceneManager.LoadScene("StageSelect");
        }
    }

    // 다시 시작
    public void RestartGame()
    {
        AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // 클리어 메뉴 열기
public void OpenClearMenu()
{
    SaveStageClear();

    GameManager gm = FindObjectOfType<GameManager>();

    if (gm != null && clearTimeText != null)
    {
        float time = gm.GetStageTime();

        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);

        clearTimeText.text =
            $"클리어 시간 : {min:00}:{sec:00}";
    }

    clearPanel.SetActive(true);

    Time.timeScale = 0f;
}

    public void SaveStageClear()
    {
        int currentStage = SceneManager.GetActiveScene().buildIndex;

        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        if (currentStage + 1 > unlockedStage)
        {
            PlayerPrefs.SetInt("UnlockedStage", currentStage + 1);
            PlayerPrefs.Save();
        }
    }

    public void GoToMainMenu()
    {
        AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}