using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Gun : MonoBehaviour
{
    public float fireCooldown = 1.0f;
    float startTime;
    [SerializeField] float damage;
    [SerializeField] bool isPlayer;
    public Transform muzzleTransorm;
    public GameObject HitEffectParticle;
    public UnityEvent OnShoot;
    

    void Start()
    {
        if (!muzzleTransorm)
        {
            muzzleTransorm = gameObject.transform;
        }
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > fireCooldown) {
            startTime = Time.time;
            if (isPlayer) {
                #if UNITY_ANDROID && !UNITY_EDITOR
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                    {
                        Shoot();
                    }
                #else
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                #endif
            }
            else {
                Shoot();
            }
        }
    }



    virtual protected void Shoot()
    {
        OnShoot.Invoke();
        Debug.Log("Shoot: Shot was fired", this);
        int layerMask = LayerMask.NameToLayer("Triggers");
        //layerMask = ~layerMask;
        Ray ray = new Ray(muzzleTransorm.position, muzzleTransorm.forward);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit, Mathf.Infinity, layerMask))
        {
            
            GameObject particle = GameObject.Instantiate(HitEffectParticle, Hit.point, Quaternion.identity);

            particle.transform.LookAt(muzzleTransorm, Vector3.up);

            GameObject HitObject = Hit.collider.gameObject;

            if (isPlayer) {
                if (HitObject.tag == "Enemy") {
                    HitObject.GetComponent<Health>().TakeDamage(damage);
                }
            }
            else {
                if (HitObject.tag == "Player") {
                    HitObject.GetComponent<Health>().TakeDamage(damage);
                }
            }

            Debug.Log("Shoot: hit ", this);
        }
    }
}
