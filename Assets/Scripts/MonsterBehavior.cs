using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent navMeshAgent;
    public GameObject jumpScare;
    public AudioClip jumpScareAudio;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            SoundManager.Instance.eventsSoundSource.PlayOneShot(jumpScareAudio);
            jumpScare.SetActive(true);
            HudManager.Instance.gameOverHud.SetActive(true);
            HudManager.Instance.StopTime(2);
        }
    }
}
