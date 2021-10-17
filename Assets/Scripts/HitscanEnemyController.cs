using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HitscanEnemyController : MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent navMeshAgent;

    private AudioSource audioSource;
    [SerializeField] private AudioClip m_idleSound;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        target = GameManager.instance.Player.transform; 
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        if (!audioSource.isPlaying)
        {
            audioSource.clip = m_idleSound;
            audioSource.Play();
        }
    }
}
