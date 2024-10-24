using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent navMeshAgent;
    public GameObject jumpScare;
    public AudioClip jumpScareAudio;

    private Animator anim;

    public float originalMonsterSpeed;

    private void Awake()
    {
        
    }
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        originalMonsterSpeed = navMeshAgent.speed;
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);

        if (IsMoving())
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    bool IsMoving()
    {
        // Verifica se a velocidade do NavMeshAgent é maior que um valor bem pequeno
        return navMeshAgent.velocity.magnitude > 0.1f && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.eventsSoundSource.PlayOneShot(jumpScareAudio);
            Destroy(this.gameObject);
            jumpScare.SetActive(true);
            HudManager.Instance.gameOverHud.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            HudManager.Instance.StopTime(2);
            HudManager.Instance.StopAudio(2);
            //GlitchEffectManager.Instance.glitchMat.SetFloat("NoiseAmount", 100f);
            //GlitchEffectManager.Instance.glitchMat.SetFloat("GlitchStrength", 100f);
        }
    }
}
