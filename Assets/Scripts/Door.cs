using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Door pairedDoor;
    [SerializeField] Transform teleportPoint;

    public Door GetPartner() {
        return pairedDoor;
    }

    public Vector3 GetTeleportPoint() {
        return teleportPoint.position;
    }

    public void EnteredDoor() {
        LoopSystem.instance.EneteredPortal(gameObject);
    }
}
