using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HitscanEnemyController : MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameManager.instance.Player.transform; 
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
}
