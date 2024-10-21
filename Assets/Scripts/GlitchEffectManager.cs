using UnityEngine;

public class GlitchEffectManager : MonoBehaviour
{
    public static GlitchEffectManager Instance { get; private set; }

    public Material glitchMat;

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
}
