

using UnityEngine;

public class ZombieHealth : KienMonoBehaviour, IDamageable
{
    public bool isDead = false;
    public float health = 10;
    public GameObject attackEffect;
    private void OnEnable()
    {
        Reset();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        var effect = ObjectPool.Instance.GetObject(attackEffect, transform.position + Vector3.up, Quaternion.identity);
        ObjectPool.Instance.ReturnObject(effect, .2f);
        CheckDie();
    }
    public void CheckDie()
    {
        if (health <= 0 && isDead == false)
        {
            isDead = true;
            ObjectPool.Instance.ReturnObject(gameObject);
        }
    }
    private void Reset()
    {
        health = 10;
        isDead = false;
    }
}