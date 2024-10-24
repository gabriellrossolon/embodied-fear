using UnityEngine;
using UnityEngine.UI;
using DoorScript;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    private GameObject playerObject;

    public bool phoneObject;
    public bool radioObject;
    public bool tvObject;
    public bool doorLocked;
    public bool doorOpenable;

    public bool finalDoor;

    public int radioStation = 0;
    public AudioClip radioMusicOne;
    public AudioClip radioMusicTwo;

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
            if (radioObject) { TurnRadioStation(); }
            if (tvObject) { ChangeTvChannel(); }
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
            ObjectiveManager.Instance.objectiveOneCompleted = true;
            StartCoroutine(ObjectiveCompleted());
        }
    }

    private void TurnRadioStation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (radioStation == 0)
            {
                SoundManager.Instance.eventsSoundSource.PlayOneShot(interactionSound);
                AudioSource aS = GetComponent<AudioSource>();
                aS.clip = radioMusicOne;
                aS.Play();
                radioStation++;
            }
            else if (radioStation == 1)
            {
                SoundManager.Instance.eventsSoundSource.PlayOneShot(interactionSound);
                AudioSource aS = GetComponent<AudioSource>();
                aS.clip = radioMusicTwo;
                aS.Play();
                radioStation++;
            }
            else if (radioStation == 2)
            {
                SoundManager.Instance.eventsSoundSource.PlayOneShot(interactionSound);
                AudioSource aS = GetComponent<AudioSource>();
                aS.clip = null;
                aS.Stop();

                gameObject.GetComponent<BoxCollider>().enabled = false;
                HudManager.Instance.textWarn.SetActive(false);

                ObjectiveManager.Instance.objectiveTwoComplete = true;
                StartCoroutine(ObjectiveCompleted());
            }
        }
    }

    private void ChangeTvChannel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HudManager.Instance.textWarn.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            SoundManager.Instance.eventsSoundSource.PlayOneShot(interactionSound);
            AudioSource aS = GetComponent<AudioSource>();
            aS.enabled = false;
            GetComponentInChildren<Light>().enabled = false;
            GetComponentInChildren<FlickerLight>().enabled = false;
            ObjectiveManager.Instance.objectiveThreeComplete = true;
            StartCoroutine(ObjectiveCompleted());
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

    private IEnumerator ObjectiveCompleted()
    {
        yield return new WaitForSeconds(1.0f);
        SoundManager.Instance.eventsSoundSource.PlayOneShot(SoundManager.Instance.objectiveActived);
    }
}
