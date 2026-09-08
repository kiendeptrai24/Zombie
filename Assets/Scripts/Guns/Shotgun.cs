using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] private int pellets = 5;
    [SerializeField] private float spreadAngle = 30f;

    public override void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        for (int i = 0; i < pellets; i++)
        {
            float angle = Random.Range(
                -spreadAngle / 2f,
                spreadAngle / 2f
            );

            Quaternion rotation =
                firePoint.rotation *
                Quaternion.Euler(0f, angle, 0f);

            ObjectPool.Instance.GetObject(
                bulletPrefab,
                firePoint.position,
                rotation
            );
        }
    }
}
