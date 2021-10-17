using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GrapplingHook : MonoBehaviour
{
    public float fireCooldown = 3f;
    float startTime;

    [SerializeField] float speed = 10f;
    public float gravity = 9.8f;
    [SerializeField] bool isPlayer;
    public Transform muzzleTransorm;
    public GameObject HitEffectParticle;
    public UnityEvent OnShoot;
    bool grappling;
    Transform Player;
    float journeyLength;
    Vector3 startPos;
    bool reset;
    Vector3 target;
    void Start()
    {
        if (!muzzleTransorm)
        {
            muzzleTransorm = gameObject.transform;
        }
        startTime = Time.time;

        Player = GameManager.instance.Player.transform;

    }

    void Update()
    {
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
        }

        if (grappling) {
            if (Player.position != target) {
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                Player.position = Vector3.Lerp(startPos, target, fractionOfJourney);
            }
            else if (!reset) {
                reset = true;
                GameManager.instance.EnablePlayerMovement();
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

            GameManager.instance.DisablePlayerMovement();
                        startPos = Player.position;
            
            target = Hit.point;
                //MovePlayerAlongCurve(Hit.point, (Hit.point - GameManager.instance.Player.transform.position).normalized));

             journeyLength = Vector3.Distance(Player.position, target);
             startTime = Time.time;

             grappling = true;

            Debug.Log("Shoot: hit ", this);
        }
    }


/*
    IEnumerator MovePlayerAlongCurve(Vector3 target, Vector3 firingAngle) {
        
        GameManager.instance.DisablePlayerMovement();
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        Transform Player = GameManager.instance.Player.transform;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Player.position, target);
 
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle.x * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(Mathf.Abs(projectile_Velocity)) * Mathf.Cos(firingAngle.x * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(Mathf.Abs(projectile_Velocity)) * Mathf.Sin(firingAngle.x * Mathf.Deg2Rad);
 
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
   
        // Rotate projectile to face the target.
        //Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        Debug.Log("flight duration: " + flightDuration.ToString());
        Debug.Log("(Vx, Vy): (" + Vx.ToString() + ", " + Vy.ToString() + ")");
       
        float elapse_time = 0;
 
        
        while (elapse_time < flightDuration)
        {
            Player.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
           
            elapse_time += Time.deltaTime;
 
            yield return null;
        }
    

        GameManager.instance.EnablePlayerMovement();
        yield return null;
    }*/
}
