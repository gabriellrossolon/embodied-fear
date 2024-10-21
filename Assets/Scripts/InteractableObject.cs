using UnityEngine;
using UnityEngine.UI;
using DoorScript;

public class InteractableObject : MonoBehaviour
{
    private GameObject playerObject;

    public bool phoneObject;
    public bool doorLocked;
    public bool doorOpenable;

    public AudioClip interactionSound;

    private float distanceToPlayer;
    public float activeDistance = 3;

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
            if (phoneObject) { AnswerCall(); }
            if (doorLocked || doorOpenable) { OpenDoor(); }
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

    private void AnswerCall()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HudManager.Instance.textWarn.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            SoundManager.Instance.eventsSoundSource.PlayOneShot(interactionSound);
            AudioSource aS = GetComponentInChildren<AudioSource>();
            aS.enabled = false;
        }
    }

    private void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorLocked)
            {
                AudioSource aS = GetComponent<AudioSource>();
                aS.PlayOneShot(interactionSound);
            }
            if (doorOpenable)
            {
                HudManager.Instance.textWarn.SetActive(false);
                gameObject.GetComponent<Door>().OpenDoor();
            }
        }
    }
}
