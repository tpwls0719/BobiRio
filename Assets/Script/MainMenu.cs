using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }

    public void ExitGame()
    {
        Application.Quit();

        // 에디터 테스트용
        Debug.Log("게임 종료");
    }
}