using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private GameObject playerObject;

    public bool lanternObject;

    public AudioClip pickupSound;

    private float distanceToPlayer;
    public float activeDistance = 4;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, playerObject.transform.position);

    }
    private void OnMouseOver()
    {
        if (distanceToPlayer <= activeDistance)
        {
            HudManager.Instance.textWarn.SetActive(true);
            if (lanternObject) { CollectLantern(); }
        }
        else
        {
            HudManager.Instance.textWarn.SetActive(false);
        } 
    }

    private void OnMouseExit()
    {
        HudManager.Instance.textWarn.SetActive(false);
    }

    private void CollectLantern()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HudManager.Instance.textWarn.SetActive(false);
            SoundManager.Instance.eventsSoundSource.PlayOneShot(pickupSound);
            Inventory.Instance.lanternEquipped = true;
            Destroy(this.gameObject);
        }
    }
}
