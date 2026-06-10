using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    // 처음부터 다시
    public void NewGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        // 진행도 초기화
        PlayerPrefs.DeleteKey("UnlockedStage");

        // 다시 Stage1만 열어줌
        PlayerPrefs.SetInt("UnlockedStage", 2);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }

    // 진행도 유지하고 계속 플레이
    public void ContinueGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }

    // 게임 종료
    public void ExitGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Application.Quit();

        Debug.Log("게임 종료");
    }
}