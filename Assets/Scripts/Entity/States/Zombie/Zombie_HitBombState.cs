using UnityEngine;

public class Zombie_HitBombState : Zombie_GroundState, IAnimationTrigger
{
    public Zombie_HitBombState(ZombieController player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }

    public void ActiveTrigger()
    {
        m_machine.ChangeState<Zombie_IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        SFXManager.Instance.PlayOneShot("zombie death");
    }
    public override void Excute()
    {
        base.Excute();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
