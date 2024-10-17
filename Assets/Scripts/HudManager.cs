using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    public Slider lanternSlider;
    public GameObject lanternInHud;

    public GameObject gameOverHud;

    public GameObject textWarn;
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

    public IEnumerator StopTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Inventory.Instance.lanternEquipped) { lanternInHud.SetActive(true); }

    }
}
