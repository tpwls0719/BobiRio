using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageIndex;

    void Start()
{
    if (!PlayerPrefs.HasKey("UnlockedStage"))
    {
        PlayerPrefs.SetInt("UnlockedStage", 2);
        PlayerPrefs.Save();
    }
}

    public void LoadStage()
    {
        int unlockedStage =
            PlayerPrefs.GetInt("UnlockedStage", 2);

        if (stageIndex <= unlockedStage)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(stageIndex);
        }
        else
        {
            Debug.Log("아직 클리어하지 않은 스테이지입니다.");
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}