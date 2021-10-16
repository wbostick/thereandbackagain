using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AggroOnSight : MonoBehaviour
{
    [SerializeField] float visionConeAngle = 90f;
    public UnityEvent OnAggro;

    private void FixedUpdate() {
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 targetDir = playerPos - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, targetDir);
        
        if (angleBetween < visionConeAngle / 2) {
            Ray ray = new Ray(transform.position, targetDir);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, Mathf.Infinity)) {
                GameObject HitObject = Hit.collider.gameObject;
                if (HitObject.tag == "Player") {
                    OnAggro.Invoke();
                }
            }
        }
    }
    public void ForceAggro() {
        OnAggro.Invoke();
    }
}
