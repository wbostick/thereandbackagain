using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGameObject : MonoBehaviour
{
    public float damageAmount;
    public void Damage(GameObject obj) {
        obj.GetComponent<Health>().TakeDamage(damageAmount);
    }
}
