using UnityEngine;

public class ZombieController : KienMonoBehaviour
{
    public Animator anim;
    public IStateMachine machine;
    public Transform attackPoint;
    public LayerMask attackLayer;
    public float attackRadius = 1f;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        machine = new ZombieStateMachine(this);
    }
    void OnEnable()
    {
        machine.Init<Zombie_IdleState>();
    }
    void Update()
    {
        machine?.Update();
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
}
