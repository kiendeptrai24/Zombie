using UnityEngine;

public class Zombie_GroundState : ZombieState
{
    private ZombieTargetFinder zombieTarget;

    public Zombie_GroundState(ZombieController zombie, IStateMachine stateMachine, string animName) : base(zombie, stateMachine, animName)
    {
        zombieTarget = zombie.GetComponent<ZombieTargetFinder>();

    }
    public override void Enter()
    {
        base.Enter();
        SFXManager.Instance.PlayOneShot("zombie chase");

    }
    public override void Excute()
    {
        base.Excute();
        if (zombieTarget.CurrentTarget == null) return;
        if (Vector3.Distance(zombieTarget.CurrentTarget.position, m_zombie.transform.position) < 1)
            m_machine.ChangeState<Zombie_BattleState>();

    }

}
