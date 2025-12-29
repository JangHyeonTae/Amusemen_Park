using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        EnsureAudioSource();
    }

    private void EnsureAudioSource()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (audioSource == null || audioSource.Equals(null) || audioSource.gameObject != gameObject)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (!audioSource.enabled) audioSource.enabled = true;

        if (!audioSource.gameObject.activeSelf)
            audioSource.gameObject.SetActive(true);
    }

    public void PlayeSFX(AudioClip clip)
    {
        if (clip == null) return;

        EnsureAudioSource();

        audioSource.PlayOneShot(clip);
    }
}
