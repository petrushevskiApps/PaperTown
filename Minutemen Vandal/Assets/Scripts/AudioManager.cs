using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip _fireSoundClip;

    [SerializeField]
    private AudioClip _onHitSoundClip;

    [SerializeField]
    private AudioClip _onStepSoundClip;

    [Header("Music")]
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioClip menuMusicClip;
    [SerializeField]
    private AudioClip gameplayMusicClip;

    public static AudioManager Instance;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource soundsAudioSource;
    [SerializeField]
    private AudioClip levelCompletedClip;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        GameManager.Instance.OnLevelStarted.AddListener(OnLevelStarted);
        GameManager.Instance.OnLevelPaused.AddListener(OnLevelPaused);
        GameManager.Instance.OnLevelCompleted.AddListener(OnLevelCompleted);
        GameManager.Instance.OnLevelExited.AddListener(OnLevelExited);
        GameManager.Instance.OnLevelResumed.AddListener(OnLevelStarted);
        PlayMenuMusic();
    }


    public void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted.RemoveListener(OnLevelStarted);
        GameManager.Instance.OnLevelPaused.RemoveListener(OnLevelPaused);
        GameManager.Instance.OnLevelCompleted.RemoveListener(OnLevelCompleted);
        GameManager.Instance.OnLevelExited.RemoveListener(OnLevelExited);
        GameManager.Instance.OnLevelResumed.RemoveListener(OnLevelStarted);
    }

    public void OnStep(Vector3 position)
    {
        var audioSource = CreateAudioSource(position);
        audioSource.clip = _onStepSoundClip;
        audioSource.volume = 0.5f;
        audioSource.pitch = Random.Range(0.9f, 1f);
        audioSource.Play();
        Destroy(audioSource.gameObject, _onStepSoundClip.length + 0.1f);
    }

    public void OnFire(Vector3 position)
    {
        var audioSource = CreateAudioSource(position);
        audioSource.clip = _fireSoundClip;
        audioSource.volume = 1;
        audioSource.spatialBlend = 0.5f;
        audioSource.pitch = 1;
        audioSource.Play();
        Destroy(audioSource.gameObject, _fireSoundClip.length + 0.1f);
    }

    public void OnBulletHit(Vector3 position)
    {
        var audioSource = CreateAudioSource(position);
        audioSource.clip = _onHitSoundClip;
        audioSource.volume = 1;
        audioSource.spatialBlend = 1;
        audioSource.pitch = Random.Range(0.3f, 1f);
        audioSource.Play();
        Destroy(audioSource.gameObject, _onHitSoundClip.length + 0.1f);
    }

    public void PlayMenuMusic()
    {
        musicAudioSource.Stop();
        if (menuMusicClip == null)
        {
            return;
        }
        if (musicAudioSource.clip != menuMusicClip)
        {
            musicAudioSource.clip = menuMusicClip;
        }
        musicAudioSource.volume = 1;
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayMusicClip == null)
        {
            musicAudioSource.Stop();
            return;
        }
        if (musicAudioSource.clip != gameplayMusicClip)
        {
            musicAudioSource.clip = gameplayMusicClip;
        }
        musicAudioSource.volume = 1;
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void PlayLevelCompletedSoundEffect()
    {
        if (levelCompletedClip == null)
        {
            return;
        }
        soundsAudioSource.PlayOneShot(levelCompletedClip);
    }

    public void DimMusic()
    {
        musicAudioSource.volume = 0.33f;
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    private AudioSource CreateAudioSource(Vector3 position)
    {
        var tempObject = new GameObject();
        var audioSource = tempObject.AddComponent<AudioSource>();
        audioSource.transform.position = position;
        return audioSource;
    }

    private void OnLevelCompleted()
    {
        PlayLevelCompletedSoundEffect();
    }

    private void OnLevelPaused()
    {
        DimMusic();
    }

    private void OnLevelStarted()
    {
        PlayGameplayMusic();
    }
    private void OnLevelExited()
    {
        PlayMenuMusic();
    }
}
