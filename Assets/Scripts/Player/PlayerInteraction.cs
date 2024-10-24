using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject monsterObject;

    public GameObject lantern;
    private bool light_actived = false;
    public float lanternMaxLightTime;
    private float lanternLightTimer;
    private bool lanternRecharging = false;
    private bool canUseLantern = true;
    public AudioClip lanternClick;

    public float actualStamina;
    public float maxStamina = 100f;
    public bool canRun = true;
    private void Start()
    {
        lanternLightTimer = lanternMaxLightTime;
        HudManager.Instance.lanternSlider.maxValue = lanternMaxLightTime;

        actualStamina = maxStamina;

        monsterObject = ObjectiveManager.Instance.monster;
    }
    private void Update()
    {
        if (Inventory.Instance.lanternEquipped) { LanternManagement(); }

        if(actualStamina <= 0)
        {
            canRun = false;
        }
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

        if (light_actived == true)
        {
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit lanternHit);

            if (lanternHit.transform.CompareTag("Monster"))
            {
                monsterObject.GetComponent<MonsterBehavior>().navMeshAgent.speed = 0;
            }
            else
            {
                if(monsterObject != null && monsterObject.activeSelf)
                {
                    monsterObject.GetComponent<MonsterBehavior>().navMeshAgent.speed = monsterObject.GetComponent<MonsterBehavior>().originalMonsterSpeed;
                }
            }
        }
        else
        {
            if (monsterObject != null && monsterObject.activeSelf)
            {
                monsterObject.GetComponent<MonsterBehavior>().navMeshAgent.speed = monsterObject.GetComponent<MonsterBehavior>().originalMonsterSpeed;
            }
        }


        if (lanternLightTimer <= 0)
        {
            SoundManager.Instance.eventsSoundSource.PlayOneShot(lanternClick);
            lantern.GetComponent<Light>().enabled = false;
            light_actived = false;
            canUseLantern = false;
            lanternRecharging = true;
        }

        if (lanternRecharging)
        {
            lanternLightTimer += Time.deltaTime * 3f;
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
