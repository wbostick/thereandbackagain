using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ArcProjectile : Projectile
{
    public AnimationCurve arcHeight;

    void FixedUpdate()
    {
        float flightTime = Time.time - launchTimestamp;
        float newHeight = launchHeight + arcHeight.Evaluate(flightTime);

        Vector3 GroundVelocity = launchForward;
        GroundVelocity.y = 0;

        Vector3 GroundPosition = rigid.position + GroundVelocity;
        Vector3 NewPosition = GroundPosition;
        NewPosition.y = newHeight;

        rigid.MovePosition(NewPosition);
    }
}
