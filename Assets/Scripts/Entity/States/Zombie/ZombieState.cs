using UnityEngine;

public abstract class ZombieState : IState
{

    protected ZombieController m_zombie;
    protected IStateMachine m_machine;
    protected Animator m_anim;
    protected string m_animName;
    protected float m_stateTimer;


    public ZombieState(ZombieController player, IStateMachine stateMachine, string animName)
    {
        m_zombie = player;
        m_machine = stateMachine;
        m_animName = animName;
        m_anim = player.anim;
    }

    public virtual void Enter()
    {
        m_stateTimer = 0;
        m_anim.SetBool(m_animName, true);
    }

    public virtual void Excute()
    {
        m_stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        m_anim.SetBool(m_animName, false);
    }
}
