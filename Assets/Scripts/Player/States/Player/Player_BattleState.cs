using UnityEngine;

public class Player_BattleState : Player_GroundState
{
    private ZombieDetector zombieDetector;
    public Player_BattleState(PlayerControler player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
        zombieDetector = player.GetComponent<ZombieDetector>();
    }
    public override void Enter()
    {
        base.Enter();
        m_anim.SetLayerWeight(1, 1f);
    }
    public override void Excute()
    {
        base.Excute();
        if (zombieDetector.NearestZombie == null)
            m_machine.ChangeState<Player_IdleState>();
    }
    public override void Exit()
    {
        base.Exit();
        m_anim.SetLayerWeight(1, 0);
    }
}
