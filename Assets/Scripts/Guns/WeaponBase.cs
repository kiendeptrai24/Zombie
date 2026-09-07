using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IShootable
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;

    protected float nextFireTime;

    public abstract void Shoot();
}