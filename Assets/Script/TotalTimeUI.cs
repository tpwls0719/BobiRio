using UnityEngine;
using TMPro;

public class TotalTimeUI : MonoBehaviour
{
    public TMP_Text totalTimeText;

    void Start()
    {
        float totalTime = 0f;

        for (int i = 2; i <= 6; i++)
        {
            totalTime += PlayerPrefs.GetFloat(
                "StageTime_" + i,
                0f
            );
        }

        int min = Mathf.FloorToInt(totalTime / 60);
        int sec = Mathf.FloorToInt(totalTime % 60);

        totalTimeText.text =
            $"총 플레이 시간 : {min:00}:{sec:00}";
    }
}