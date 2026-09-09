using System;
using System.Collections.Generic;

public class ZombieStateMachine : IStateMachine
{
    protected Dictionary<Type, IState> _statesDirtionary = new Dictionary<Type, IState>();
    protected IState _curState;
    public ZombieStateMachine(ZombieController zombie)
    {
        _statesDirtionary.Add(typeof(Zombie_GroundState), new Zombie_GroundState(zombie, this, "Ground"));
        _statesDirtionary.Add(typeof(Zombie_IdleState), new Zombie_IdleState(zombie, this, "Idle"));
        _statesDirtionary.Add(typeof(Zombie_MoveState), new Zombie_MoveState(zombie, this, "Move"));
        _statesDirtionary.Add(typeof(Zombie_BattleState), new Zombie_BattleState(zombie, this, "Attack"));
        _statesDirtionary.Add(typeof(Zombie_HitBombState), new Zombie_HitBombState(zombie, this, "HitBomb"));
    }
    public void SetStateList(Dictionary<Type, IState> dic)
    {
        _statesDirtionary.Clear();
        _statesDirtionary = dic;
    }
    public void Init<T>() where T : IState
    {
        if (GetState<T>() == null)
            return;
        SetState(GetState<T>());

        _curState.Enter();
    }

    public void ChangeState<T>() where T : IState
    {
        if (GetState<T>() == null)
            return;
        _curState.Exit();
        Init<T>();
    }

    public void SetState(IState curState) => _curState = curState;

    public IState GetState<T>() where T : IState => _statesDirtionary[typeof(T)];

    public void Update()
    {
        if (_curState != null)
            _curState.Excute();
    }

    public IState GetCurrentState() => _curState;

    public T GetFeature<T>() where T : class
    {
        return _curState as T;
    }

    public void ChangeState(Type stateType)
    {
        if (!_statesDirtionary.TryGetValue(stateType, out var nextState))
            return;

        _curState?.Exit();
        _curState = nextState;
        _curState.Enter();
    }
}