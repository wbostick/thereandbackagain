using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectileGun gun;
    public float forwardSpeed;
    public UnityEvent OnImpact;
    protected Rigidbody rigid;
    protected float launchHeight;

    protected float launchTimestamp;
    protected Vector3 launchForward;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Launch(ProjectileGun shootingGun)
    {
        gun = shootingGun;
        forwardSpeed = shootingGun.launchSpeed;
        launchHeight = gameObject.transform.position.y;
        launchForward = gun.muzzleTransorm.forward;
        launchTimestamp = Time.time;

        rigid.velocity = launchForward * forwardSpeed;
    }

    private void OnCollisionEnter(Collision other) {
        Impact(other);
    }

    virtual protected void Impact(Collision other)
    {
        GameObject hitObject = other.gameObject;
        if (gun.isPlayer) {
            if (hitObject.tag == "Enemy") {
                Debug.Log("Impact: hit enemy", this);
            }
        }
        else {
            if (hitObject.tag == "Player") {
                Debug.Log("Impact: hit Player", this);
            }
        }

        OnImpact.Invoke();
    }
}
