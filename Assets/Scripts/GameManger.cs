using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManger : KienMonoBehaviour
{
    public enum GameState
    {
        Ready,
        Start,
        During,
        End
    }

    public enum GameResult
    {
        Lost,
        Win
    }

    public event Action OnGameReadied;
    public event Action OnGameStarted;
    public event Action OnGameEnded;

    [Header("Game State")]
    public GameState currentState;

    [Header("Game Settings")]
    [SerializeField] private float readyTime = 3f;
    [SerializeField] private float gameDuration = 180f;

    [Header("Runtime")]
    public float readyTimer;
    public float gameTimer;

    public GameResult GameResult_ { get; private set; }

    public event Action<int> OnTimeChange;

    [SerializeField] private PlayerHealth playerHealth;

    private int lastRemainingTime;

    protected override void Awake()
    {
        base.Awake();

        playerHealth.OnDead += EndGame;

        var missionData = GameController.Instance.GetMissionData();
        gameDuration = missionData.duration;

        lastRemainingTime = Mathf.CeilToInt(gameDuration);
    }

    private void EndGame()
    {
        GameEnd(GameResult.Lost);
    }

    protected override void Start()
    {
        base.Start();

        GameReady();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Ready:
                UpdateReady();
                break;

            case GameState.During:
                UpdateGame();
                break;
        }
    }

    private void UpdateReady()
    {
        readyTimer -= Time.deltaTime;

        if (readyTimer <= 0f)
        {
            GameStart();
        }
    }

    private void UpdateGame()
    {
        gameTimer += Time.deltaTime;

        // Thời gian còn lại
        float remainingTime = gameDuration - gameTimer;

        // Làm tròn lên để hiển thị 03:00 -> 02:59 -> ...
        int remainingSeconds = Mathf.CeilToInt(remainingTime);

        // Chỉ invoke event khi số giây thay đổi
        if (remainingSeconds != lastRemainingTime)
        {
            lastRemainingTime = remainingSeconds;

            OnTimeChange?.Invoke(remainingSeconds);
        }

        // Sống đủ thời gian
        if (gameTimer >= gameDuration)
        {
            GameEnd(GameResult.Win);
        }
    }

    public void GameReady()
    {
        currentState = GameState.Ready;

        readyTimer = readyTime;

        OnGameReadied?.Invoke();
    }

    public void GameStart()
    {
        currentState = GameState.Start;

        gameTimer = 0f;

        lastRemainingTime = Mathf.CeilToInt(gameDuration);

        OnGameStarted?.Invoke();

        currentState = GameState.During;
    }

    public void GameEnd(GameResult result)
    {
        if (currentState == GameState.End)
            return;

        currentState = GameState.End;

        GameResult_ = result;

        OnGameEnded?.Invoke();
        SFXManager.Instance.Stop();
        Debug.Log($"Game End: {result}");
    }

    public void PlayerDied()
    {
        if (currentState != GameState.During)
            return;

        GameEnd(GameResult.Lost);
    }
}
