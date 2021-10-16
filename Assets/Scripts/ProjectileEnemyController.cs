using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnityStandardAssets.Cameras.LookatTarget))]
public class ProjectileEnemyController : MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent navMeshAgent;
    protected UnityStandardAssets.Cameras.LookatTarget lookatTarget;

    public GameObject destinationTransform;

    public float scareDistance = 5.0f;

    // scareDistance * stopScareMultiplier = distance before stops running away
    public float stopScareDistanceMultipler = 3.0f;
    public float runAwayDestinationMultiplier = 1.5f;

    private bool _scared = false;
    private bool scared { 
        get {return _scared;}
        set {
            if (!_scared && value) {OnScare.Invoke();}
            if (_scared && !value) {OnEndScare.Invoke();}
            _scared = value;
        }
    }

    public UnityEvent OnScare;
    public UnityEvent OnEndScare;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lookatTarget = GetComponent<UnityStandardAssets.Cameras.LookatTarget>();
        if (!destinationTransform)
        {
            destinationTransform = new GameObject("Destination");
            destinationTransform.transform.SetParent(gameObject.transform);
        }
    }

    void Start()
    {
        target = GameManager.instance.Player.transform;
    }

    void Update()
    {
        UpdateScared();
        UpdateDestination();
        UpdateLookAt();
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

        Vector3 destination = GetDestinationPosition();
        navMeshAgent.SetDestination(destination);

        this.destinationTransform.transform.position = destination;
    }

    void UpdateLookAt()
    {
        if (scared)
        {
            lookatTarget.SetTarget(this.destinationTransform.transform);
        }
        else
        {
            lookatTarget.SetTarget(target);
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

    public Vector3 GetDestinationPosition()
    {
        if (!target || !scared)
        {
            return gameObject.transform.position;
        }

        float OffsetMultiplier = scareDistance * stopScareDistanceMultipler * runAwayDestinationMultiplier;
        Vector3 currentOffsetFromTarget = gameObject.transform.position - target.position;
        Vector3 desiredOffsetFromTarget = currentOffsetFromTarget.normalized * OffsetMultiplier;
        return target.position + desiredOffsetFromTarget;
    }
}
