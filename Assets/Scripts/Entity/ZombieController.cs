using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : KienMonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    public IStateMachine machine;
    public Transform attackPoint;
    public LayerMask attackLayer;
    public float attackRadius = 1f;
    private ZombieHealth health;
    private NavMeshAgent agent;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        machine = new ZombieStateMachine(this);
        health = GetComponent<ZombieHealth>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void OnEnable()
    {
        machine.Init<Zombie_IdleState>();
    }
    void Update()
    {
        machine?.Update();
    }
    public void Knockback(Vector3 explosionPosition, float force)
    {
        health.TakeDamage(100);
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(
            explosionPosition
            + Vector3.up * force,
            ForceMode.Impulse
        );
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Vẽ Attack Point
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRadius
        );

        // Vẽ hướng từ Zombie đến Attack Point
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            attackPoint.position
        );
    }

    public void Dead()
    {
        machine.ChangeState<Zombie_HitBombState>();
    }
}
