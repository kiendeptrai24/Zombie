using UnityEngine;

public class Player_MoveState : Player_GroundState
{
    public Player_MoveState(PlayerControler player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
}
