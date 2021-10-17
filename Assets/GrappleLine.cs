using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleLine : MonoBehaviour
{
    public Transform GrapplingHookMuzzle;
    public Transform RopeEnd;
    [SerializeField] LineRenderer line;

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, GrapplingHookMuzzle.position);
        line.SetPosition(1, RopeEnd.position);
        
    }
}
