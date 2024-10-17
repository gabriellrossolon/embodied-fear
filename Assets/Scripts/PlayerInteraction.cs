using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject lantern;
    private bool light_actived = false;
    public float lanternMaxLightTime;
    private float lanternLightTimer;
    private bool lanternRecharging = false;
    private bool canUseLantern = true;
    public AudioClip lanternClick;
    private void Start()
    {
        lanternLightTimer = lanternMaxLightTime;
        HudManager.Instance.lanternSlider.maxValue = lanternMaxLightTime;
    }
    private void Update()
    {
        if (Inventory.Instance.lanternEquipped) { LanternManagement(); }
    }

    private void LanternManagement()
    {
        if (Input.GetKeyDown(KeyCode.F) && canUseLantern)
        {
            light_actived = !light_actived;
            lantern.GetComponent<Light>().enabled = light_actived;
            SoundManager.Instance.eventsSoundSource.PlayOneShot(lanternClick);
        }

        if (lantern.GetComponent<Light>().isActiveAndEnabled)
        {
            lanternLightTimer -= Time.deltaTime;
        }

        if (lanternLightTimer <= 0)
        {
            lantern.GetComponent<Light>().enabled = false;
            light_actived = false;
            SoundManager.Instance.eventsSoundSource.PlayOneShot(lanternClick);
            canUseLantern = false;
            lanternRecharging = true;
        }

        if (lanternRecharging)
        {
            lanternLightTimer += Time.deltaTime;
            HudManager.Instance.lanternSlider.fillRect.GetComponent<Image>().color = Color.Lerp(new Color(185f / 255f, 68f / 255f, 32f / 255f), new Color(171f / 255f, 157f / 255f, 94f / 255f), lanternLightTimer / lanternMaxLightTime);
            if (lanternLightTimer >= lanternMaxLightTime)
            {
                lanternRecharging = false;
                canUseLantern = true;
            }
        }

        HudManager.Instance.lanternSlider.value = lanternLightTimer;
    }
}
