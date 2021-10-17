using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class DamageRadius : MonoBehaviour
{
    public float radius;
    public List<string> ignoreTags = new List<string>{"Player"};
    public void DamageEnemiesInRadius()
    {
        float damage = GetComponent<Projectile>().gun.damage;
        Collider[] nearby = Physics.OverlapSphere(gameObject.transform.position, radius);

        foreach (Collider collider in nearby)
        {
            GameObject hitObject = collider.gameObject;
            if (ignoreTags.Contains(hitObject.tag))
            {
                continue;
            }

            hitObject.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}
