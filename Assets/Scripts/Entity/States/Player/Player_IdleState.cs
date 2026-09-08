using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(PlayerControler player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
    }
}
