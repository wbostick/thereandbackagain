using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireCooldown = 0.0f;

    public Transform muzzleTransorm;


    void Start()
    {
        if (!muzzleTransorm)
        {
            muzzleTransorm = gameObject.transform;
        }
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (CanShoot() && Input.GetKeyDown("Shoot"))
        {
            Shoot();
        }
        
    }

    bool CanShoot()
    {
        if (fireCooldown > 0.0f)
        {
            return false;
        }

        return true;
    }

    void Shoot()
    {
        Debug.Log("Shoot: Shot was fired", this);
        
        Ray ray = new Ray(muzzleTransorm.position, muzzleTransorm.forward);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
        {
            GameObject HitObject = Hit.collider.gameObject;
            Debug.Log("Shoot: hit ", this);
        }
    }
}
