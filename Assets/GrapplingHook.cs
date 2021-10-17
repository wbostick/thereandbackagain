using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GrapplingHook : Gun
{
    [SerializeField] float speed = 10f;
    [SerializeField] float targetRadius = 10f;
    public float gravity = 9.8f;
    bool grappling = false;
    bool reset;
    Vector3 target;

    override protected void Update()
    {
        /*
        if (Time.time - startTime > fireCooldown) {
            startTime = Time.time;
            if (isPlayer) {
                #if UNITY_ANDROID && !UNITY_EDITOR
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
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
        }*/

        if (grappling) {
            updateGrapple();
        }
        else if (((Time.time - lastShotTimestamp) > fireCooldown) && GrappleInput()){
            Debug.Log("Entered Shoot " + (Time.time - lastShotTimestamp) + "," + fireCooldown);
            Shoot();
        }
        
    }

    bool GrappleInput()
    {
        if (isPlayer)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
                {
                    return true;
                }
            #else
                if (Input.GetButton("Fire1"))
                {
                    return true;
                }
            #endif
            return false;
        }
        return true;
    }

    void updateGrapple()
    {
        if (Vector3.Distance(Player.transform.position, target) > targetRadius && GrappleInput())
        {
            float targetDistance = Vector3.Distance(Player.transform.position, target);
            Vector3 velocity = new Vector3((target.x - Player.transform.position.x) * (speed / targetDistance),
                ((target.y - Player.transform.position.y) * (speed / targetDistance)) - gravity,
                (target.z - Player.transform.position.z) * (speed / targetDistance));
            Player.transform.Translate(velocity * Time.deltaTime, Space.World);

        }
        else if (!reset)
        {
            lastShotTimestamp = Time.time;
            reset = true;
            grappling = false;
            Player.GetComponent<Rigidbody>().useGravity = true;
            GameManager.instance.EnablePlayerMovement();
        }
    }

    override protected void Shoot_Implementation()
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

            GameManager.instance.DisablePlayerMovement();
            
            target = Hit.point;

            grappling = true;
            reset = false;
            Player.GetComponent<Rigidbody>().useGravity = false;

            Debug.Log("Shoot: hit ", this);
        }
    }

}
