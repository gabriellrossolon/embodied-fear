using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private Light lightObject;
    private AudioSource soundSourceSpace;

    private float timer;
    public float maxTime;
    public float minTime;

    public AudioClip lightFlickSound;
    void Start()
    {
        lightObject = GetComponent<Light>();
        soundSourceSpace = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        LightFlicker();
    }

    private void LightFlicker()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            lightObject.enabled = !lightObject.enabled;
            timer = Random.Range(minTime, maxTime);
            soundSourceSpace.PlayOneShot(lightFlickSound);
        }
    }
}
