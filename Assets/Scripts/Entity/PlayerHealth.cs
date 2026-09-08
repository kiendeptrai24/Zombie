

using UnityEngine;

public class PlayerHealth : KienMonoBehaviour, IDamageable
{
    public bool isDead = false;
    public float health = 10;
    public GameObject attackEffect;
    public void TakeDamage(float damage)
    {
        health -= damage;
        var effect = ObjectPool.Instance.GetObject(attackEffect, transform.position + Vector3.up, Quaternion.identity);
        ObjectPool.Instance.ReturnObject(effect, .2f);
    }
    public void CheckDie()
    {
        if (health <= 0 && isDead == false)
            isDead = true;
    }
}