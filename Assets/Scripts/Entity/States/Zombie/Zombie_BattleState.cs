using UnityEngine;

public class Zombie_BattleState : Zombie_GroundState, IAnimationTrigger, ISkillTrigger
{
    public Zombie_BattleState(ZombieController player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }

    public void ActiveSkill()
    {
        Collider[] players = Physics.OverlapSphere(
            m_zombie.attackPoint.position,
            m_zombie.attackRadius,
            m_zombie.attackLayer
        );



        foreach (Collider player in players)
        {
            var target = player.GetComponent<IDamageable>();
            if (target != null)
                target.TakeDamage(1);
        }
    }

    public void ActiveTrigger()
    {
        m_machine.ChangeState<Zombie_IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        SFXManager.Instance.PlayOneShot("zombie attack");
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
