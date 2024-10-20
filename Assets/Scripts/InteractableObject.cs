using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private GameObject playerObject;

    public bool phoneObject;

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
}
