using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    public GameObject playerObject;
    public PlayerInteraction playerInteract;

    public Slider lanternSlider;
    public GameObject lanternInHud;

    public GameObject gameOverHud;
    public GameObject winHud;

    public GameObject textWarn;

    public Slider staminaBar;
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

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerInteract = playerObject.GetComponent<PlayerInteraction>();
    }

    public IEnumerator StopTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Inventory.Instance.lanternEquipped) { lanternInHud.SetActive(true); }
        StaminaBar();

    }

    private void StaminaBar()
    {
        staminaBar.maxValue = playerInteract.maxStamina;
        staminaBar.value = playerInteract.actualStamina;
        if (playerInteract.actualStamina >= playerInteract.maxStamina)
        {
            staminaBar.gameObject.SetActive(false);
        }
        else
        {
            staminaBar.gameObject.SetActive(true);
        }
    }
}
