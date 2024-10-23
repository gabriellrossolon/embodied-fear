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
    public GameObject pausedHud;
    public GameObject optionsHud;

    private bool isPaused = false;
    private bool optionsOpened = false;

    public GameObject textWarn;

    public Slider staminaBar;

    public Slider globalVolumeSlider;

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

    private void Start()
    {
        if(VolumeSettings.Instance != null)
        {
            VolumeSettings.Instance.ChangeSlider(globalVolumeSlider);
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
        StaminaBar();

        if (Input.GetKeyDown(KeyCode.Escape)) { PauseGame(); }

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

    public void PauseGame()
    {
        isPaused = !isPaused;
        pausedHud.SetActive(isPaused);

        if (isPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            if (VolumeSettings.Instance != null)
            {
                VolumeSettings.Instance.globalMixer.SetFloat("GlobalPitch", 0f);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            if(VolumeSettings.Instance != null)
            {
                VolumeSettings.Instance.globalMixer.SetFloat("GlobalPitch", 1f);
            }

        }
    }

    public void OpenCloseOptions()
    {
        optionsOpened = !optionsOpened;
        optionsHud.SetActive(optionsOpened);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
