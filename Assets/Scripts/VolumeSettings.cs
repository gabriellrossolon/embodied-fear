using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings Instance { get; private set; }

    public AudioMixer globalMixer;
    public Slider globalSlider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        globalSlider.minValue = -60f;
        globalSlider.maxValue = 0;
    }

    public void SetGlobalVolume()
    {
        float volume = globalSlider.value;
        globalMixer.SetFloat("GlobalVolume", volume);
    }

    public void ChangeSlider(Slider sliderToSubstitute)
    {
        if (globalSlider == null)
        {
            globalSlider = sliderToSubstitute;
            globalSlider.minValue = -60f;
            globalSlider.maxValue = 0;
            globalMixer.GetFloat("GlobalVolume", out float currentVolume);
            globalSlider.value = currentVolume;
            globalSlider.onValueChanged.AddListener(delegate { SetGlobalVolume(); });
        }
    }
}
 