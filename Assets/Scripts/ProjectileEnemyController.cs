using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ProjectileEnemyController : MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent navMeshAgent;

    private Transform debugDestination;

    public float scareDistance = 5.0f;

    // scareDistance * stopScareMultiplier = distance before stops running away
    public float stopScareDistanceMultipler = 3.0f;
    private bool scared = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameManager.instance.Player.transform;
        debugDestination = GameObject.Instantiate(new GameObject("debugTransform"), transform).transform;
    }

    void Update()
    {
        UpdateScared();
        UpdateDestination();
    }

    void UpdateScared()
    {
        if (!target)
        {
            scared = false;
            return;
        }

        if (Vector3.Distance(target.position, gameObject.transform.position) < GetStopScareDistance(scared))
        {
            scared = true;
            return;
        }
        else
        {
            scared = false;
            return;
        }
    }

    void UpdateDestination()
    {
        if (!target)
        {
            return;
        }

        Vector3 destination = GetDestination();
        navMeshAgent.SetDestination(destination);

        if (debugDestination)
        {
            debugDestination.position = destination;
        }
    }

    public float GetStopScareDistance(bool inScared)
    {
        if (inScared)
        {
            return scareDistance * stopScareDistanceMultipler;
        }
        else
        {
            return scareDistance;
        }

    }

    public bool IsScared()
    {
        return scared;
    }

    public Vector3 GetDestination()
    {
        if (!target)
        {
            return gameObject.transform.position;
        }

        Vector3 currentOffsetFromTarget = gameObject.transform.position - target.position;
        Vector3 desiredOffsetFromTarget = currentOffsetFromTarget.normalized * GetStopScareDistance(IsScared());
        return target.position + desiredOffsetFromTarget;
    }
}
