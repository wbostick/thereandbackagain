using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPosition : MonoBehaviour
{
    [SerializeField] GameObject spawn;

    public void Spawn(Transform transform)
    {
        Instantiate(spawn, transform.position, Quaternion.identity);
    }

    public void Spawn(Vector3 position)
    {
        Instantiate(spawn, position, Quaternion.identity);
    }
}
