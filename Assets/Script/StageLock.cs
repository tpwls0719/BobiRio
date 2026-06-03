using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    public int stageIndex;

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();

        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 2);

        btn.interactable = stageIndex <= unlockedStage;
    }
}