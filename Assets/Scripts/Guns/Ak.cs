using UnityEngine;

public class Ak : WeaponBase
{
    public override void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        SFXManager.Instance.PlayOneShot("rifle");
        ObjectPool.Instance.GetObject(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}