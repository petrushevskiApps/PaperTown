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

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStep(Vector3 position)
    {
        var audioSource = CreateAudioSource(position);
        audioSource.clip = _onStepSoundClip;
        audioSource.volume = 0.5f;
        audioSource.pitch = Random.Range(0.9f,1f);
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

    private AudioSource CreateAudioSource(Vector3 position)
    {
        var tempObject = new GameObject();
        var audioSource = tempObject.AddComponent<AudioSource>();
        audioSource.transform.position = position;
        return audioSource;
    }

}
