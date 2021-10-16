using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Gun : MonoBehaviour
{
    public float fireCooldown = 0.0f;

    [SerializeField] float damage;
    public Transform muzzleTransorm;
    public GameObject HitEffectParticle;
    public UnityEvent OnShoot;

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

        if (CanShoot() && Input.GetButtonDown("Fire1"))
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
        OnShoot.Invoke();
        Debug.Log("Shoot: Shot was fired", this);
        
        Ray ray = new Ray(muzzleTransorm.position, muzzleTransorm.forward);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
        {
            
            GameObject particle = GameObject.Instantiate(HitEffectParticle, Hit.point, Quaternion.identity);

            particle.transform.LookAt(muzzleTransorm, Vector3.up);

            GameObject HitObject = Hit.collider.gameObject;
            if (HitObject.tag == "Enemy") {
                HitObject.GetComponent<Health>().TakeDamage(damage);
            }
            Debug.Log("Shoot: hit ", this);
        }
    }
}
