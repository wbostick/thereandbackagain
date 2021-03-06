using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HitscanEnemyController : MonoBehaviour
{
    public Transform target;
    public float shotRange = 6.0f;
    // how close they try to get to you
    public float followDistance = 2.0f;
    protected NavMeshAgent navMeshAgent;

    private AudioSource audioSource;
    [SerializeField] private AudioClip m_idleSound;

    Gun m_Gun;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        target = GameManager.instance.Player.transform; 
        m_Gun = GetComponentInChildren<Gun>();
    }

    void Update()
    {
        Vector3 currentOffset = transform.position - target.position;
        Vector3 desiredOffset = currentOffset.normalized * followDistance;

        navMeshAgent.SetDestination(target.position + desiredOffset);

        
        

        if (Vector3.Distance(target.transform.position, transform.position) < shotRange && m_Gun)
        {
            m_Gun.enabled = true;
        }
        else if (m_Gun)
        {
            m_Gun.enabled = false;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = m_idleSound;
            audioSource.Play();
        }
    }
}
