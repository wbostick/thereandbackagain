using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun
{
    public GameObject projectilePrefab;
    public float launchSpeed = 3.0f;

    override protected void Shoot_Implementation()
    {
        GameObject projectileObject = GameObject.Instantiate(projectilePrefab, muzzleTransorm.position, muzzleTransorm.rotation);
        projectileObject.GetComponent<Projectile>().Launch(this);
    }
}
