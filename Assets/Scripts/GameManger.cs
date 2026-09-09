using System;
using UnityEngine;

public class GameManger : KienMonoBehaviour
{
    public enum GameState
    {
        Ready,
        Start,
        During,
        End,
    }
    public enum GameResult
    {
        Lost,
        Win
    }
    public event Action OnGameReadied;
    public event Action OnGameStarted;
    public event Action OnGameProcessed;
    public event Action OnGameEnded;
    public GameState currentState;
    public int readyTime = 3;
    public float readyTimer;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        currentState = GameState.Ready;
    }
    public void GameReady()
    {

    }
    public void GameStart()
    {

    }
    public void GameProcess()
    {

    }
    public void GameEnd()
    {

    }
}
