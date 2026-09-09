

using System;
using UnityEngine;

public class PlayerHealth : KienMonoBehaviour, IDamageable
{
    public bool isDead = false;
    public float maxHealth = 100;
    public float health = 100;
    public GameObject attackEffect;
    public event Action<float, float> OnHealthChanged;
    public event Action OnDead;
    protected override void Awake()
    {
        base.Awake();
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        OnHealthChanged?.Invoke(maxHealth, health);
        var effect = ObjectPool.Instance.GetObject(attackEffect, transform.position + Vector3.up, Quaternion.identity);
        ObjectPool.Instance.ReturnObject(effect, .2f);
        CheckDie();
    }
    public void CheckDie()
    {
        if (health <= 0 && isDead == false)
        {
            isDead = true;
            OnDead?.Invoke();
            Destroy(gameObject, .1f);
        }
    }
}