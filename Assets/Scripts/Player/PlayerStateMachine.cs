using System;
using System.Collections.Generic;

public class PlayerStateMachine : IStateMachine
{
    protected Dictionary<Type, IState> _statesDirtionary = new Dictionary<Type, IState>();
    protected IState _curState;
    public PlayerStateMachine(PlayerControler player)
    {
        _statesDirtionary.Add(typeof(Player_GroundState), new Player_GroundState(player, this, "Ground"));
        _statesDirtionary.Add(typeof(Player_IdleState), new Player_IdleState(player, this, "Idle"));
        _statesDirtionary.Add(typeof(Player_MoveState), new Player_MoveState(player, this, "Move"));
        _statesDirtionary.Add(typeof(Player_BattleState), new Player_BattleState(player, this, "Battle"));
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