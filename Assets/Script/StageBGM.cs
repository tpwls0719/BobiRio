using UnityEngine;

public class StageBGM : MonoBehaviour
{
    public AudioClip stageMusic;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(stageMusic);
    }
}