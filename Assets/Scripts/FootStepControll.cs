using NUnit.Framework;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    private AudioSource footStepSource;
    public AudioClip[] footStepClips;

    private void Start()
    {
        footStepSource = GetComponent<AudioSource>();
    }

    private void FootStepCall()
    {
        AudioClip clip = GetRandomClip();
        footStepSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return footStepClips[Random.Range(0, footStepClips.Length)];
    }
}
