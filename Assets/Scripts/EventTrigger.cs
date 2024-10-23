using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject monsterCrawling;
    public bool activeMonsterCrawling;
    public AudioClip eventAudio;

    public bool winTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activeMonsterCrawling)
            {
                monsterCrawling.SetActive(true);
                //SoundManager.Instance.eventsSoundSource.PlayOneShot(eventAudio);
                //Destroy(this.gameObject);
                activeMonsterCrawling = false;
                Destroy(monsterCrawling, 7f);
            }

            if (winTrigger)
            {
                HudManager.Instance.winHud.SetActive(true);
                SoundManager.Instance.eventsSoundSource.PlayOneShot(eventAudio);
                HudManager.Instance.StopTime(3f);

            }
            
        }
    }
}
