

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : KienMonoBehaviour, IDamageable
{
    public bool isDead = false;
    public float health = 10;
    public GameObject attackEffect;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Collider cl;
    private ZombieDissolve zombieDissolve;
    private ZombieController zombie;
    private ZombieSpawner zombieSpawner;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        zombieDissolve = GetComponent<ZombieDissolve>();
        zombie = GetComponent<ZombieController>();
        cl = GetComponent<Collider>();
        zombieSpawner = FindAnyObjectByType<ZombieSpawner>();

    }
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
            zombieDissolve.PlayDissolve();
            cl.enabled = false;
            zombieSpawner.ReleaseZombieDeaded(gameObject);
            zombie.Dead();
        }
    }
    private void Reset()
    {
        health = 10;
        agent.enabled = true;
        isDead = false;
        rb.isKinematic = true;
        cl.enabled = true;
    }
}