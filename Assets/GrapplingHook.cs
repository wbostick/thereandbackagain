using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GrapplingHook : MonoBehaviour
{
    public float fireCooldown = 3f;
    float startTime;

    public float gravity = 9.8f;
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



    void Shoot()
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

            StartCoroutine(MovePlayerAlongCurve(Hit.point, (GameManager.instance.Player.transform.position - Hit.point)));


            Debug.Log("Shoot: hit ", this);
        }
    }

    IEnumerator MovePlayerAlongCurve(Vector3 target, Vector3 firingAngle) {
        
        GameManager.instance.DisablePlayerMovement();
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        Transform Player = GameManager.instance.Player.transform;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Player.position, target);
 
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle.x * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle.x * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle.x * Mathf.Deg2Rad);
 
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
   
        // Rotate projectile to face the target.
        //Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);
       
        float elapse_time = 0;
 
        while (elapse_time < flightDuration)
        {
            Player.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
           
            elapse_time += Time.deltaTime;
 
            yield return null;
        }

        GameManager.instance.EnablePlayerMovement();
        StopCoroutine("MovePlayerAlongCurve");
    }
}
