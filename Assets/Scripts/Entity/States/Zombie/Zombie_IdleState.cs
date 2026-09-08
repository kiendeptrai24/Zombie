using UnityEngine;

public class Zombie_IdleState : Zombie_GroundState
{
    private ZombieTargetFinder zombieTarget;

    public Zombie_IdleState(ZombieController zombie, IStateMachine stateMachine, string animName) : base(zombie, stateMachine, animName)
    {
        zombieTarget = zombie.GetComponent<ZombieTargetFinder>();

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (zombieTarget.CurrentTarget != null)
            m_machine.ChangeState<Zombie_MoveState>();

    }
}
