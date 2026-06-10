using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM")]
    public AudioClip bgmClip;

    [Header("SFX")]
    public AudioClip buttonClickSound;
    public AudioClip keySound;
    public AudioClip movingPlatformSound;
    public AudioClip fallingPlatformSound;
    public AudioClip goalSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayBGM();
    }

    // BGM

    public void PlayBGM()
    {
        if (bgmClip == null)
            return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void RestartBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    // SFX

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }

    public void PlayKey()
    {
        sfxSource.PlayOneShot(keySound);
    }

    public void PlayMovingPlatform()
    {
        sfxSource.PlayOneShot(movingPlatformSound);
    }

    public void PlayFallingPlatform()
    {
        sfxSource.PlayOneShot(fallingPlatformSound);
    }

    public void PlayGoal()
    {
        sfxSource.PlayOneShot(goalSound);
    }
}