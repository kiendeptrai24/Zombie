using UnityEngine;

public class Ak : WeaponBase
{
    public override void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        Debug.Log("Rifle Shoot");

        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}