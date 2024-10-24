using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource eventsSoundSource;
    public AudioSource breathSoundSource;

    public AudioClip hoverSound;
    public AudioClip objectiveActived;

    public AudioClip[] breathClips;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        PlayRandomBreathClip();
    }

    void Update()
    {
        if (!breathSoundSource.isPlaying)
        {
            PlayRandomBreathClip();
        }
    }

    void PlayRandomBreathClip()
    {
        int randomIndex = Random.Range(0, breathClips.Length);
        breathSoundSource.clip = breathClips[randomIndex];
        breathSoundSource.Play();
    }
}
