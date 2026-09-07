using UnityEngine;

public class Player_GroundState : PlayerState
{
    private Vector3 movementDir;
    private ZombieDetector zombieDetector;
    public Player_GroundState(PlayerControler player, IStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
        zombieDetector = player.GetComponent<ZombieDetector>();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        AnimationControllers();
    }
    private void AnimationControllers()
    {
        if(zombieDetector.NearestZombie != null)
            m_machine.ChangeState<Player_BattleState>();
        movementDir = m_player.GetDir();
        if (movementDir.sqrMagnitude < 0.0001f)
        {
            m_anim.SetFloat("xVelocity", 0f, 0.1f, Time.deltaTime);
            m_anim.SetFloat("zVelocity", 0f, 0.1f, Time.deltaTime);
            return;
        }

        Vector3 moveDir = new Vector3(
            movementDir.x,
            0f,
            movementDir.y
        ).normalized;

        float xVelocity = Vector3.Dot(
            moveDir,
            m_player.transform.right
        );

        float zVelocity = Vector3.Dot(
            moveDir,
            m_player.transform.forward
        );

        m_anim.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        m_anim.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
    }
}
