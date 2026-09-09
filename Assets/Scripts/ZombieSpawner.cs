using System;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : Singleton<ZombieSpawner>
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject zombiePrefab;

    [Header("Spawn Position")]
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float maxSpawnDistance = 20f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastHeight = 20f;
    [SerializeField] private float raycastDistance = 50f;

    [Header("Spawn Rate")]
    [SerializeField] private float startSpawnInterval = 2f;
    [SerializeField] private float minSpawnInterval = 0.2f;

    [SerializeField] private float difficultyIncreaseTime = 30f;
    [SerializeField] private float spawnIntervalDecrease = 0.2f;

    [Header("Limit")]
    [SerializeField] private int maxZombieCount = 100;

    private float spawnTimer;
    private float currentSpawnInterval;
    [SerializeField] private List<GameObject> zombies;
    protected override void Start()
    {
        currentSpawnInterval = startSpawnInterval;
        spawnTimer = currentSpawnInterval;
    }
    private void OnEnable()
    {
        GameManger.Instance.OnGameEnded += ReleaseZombie;

    }
    private void OnDisable()
    {
        if (GameManger.Instance != null)
            GameManger.Instance.OnGameEnded -= ReleaseZombie;
    }
    public void ReleaseZombieDeaded(GameObject zombie)
    {
        if (zombies.Contains(zombie))
            zombies.Remove(zombie);
    }
    private void ReleaseZombie()
    {
        foreach (var zombie in zombies)
        {
            ObjectPool.Instance.ReturnObject(zombie);
        }
    }

    private void Update()
    {
        UpdateSpawnRate();

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawnZombie();

            spawnTimer = currentSpawnInterval;
        }
    }

    private void UpdateSpawnRate()
    {
        float decrease =
            Time.timeSinceLevelLoad /
            difficultyIncreaseTime *
            spawnIntervalDecrease;

        currentSpawnInterval = Mathf.Max(
            minSpawnInterval,
            startSpawnInterval - decrease
        );
    }

    private void TrySpawnZombie()
    {
        if (player == null || zombiePrefab == null)
            return;

        if (GetZombieCount() >= maxZombieCount)
            return;

        if (!TryGetRandomSpawnPosition(out Vector3 spawnPosition))
            return;

        var zombie = ObjectPool.Instance.GetObject(
            zombiePrefab,
            spawnPosition,
            Quaternion.identity
        );
        zombies.Add(zombie);
    }

    private bool TryGetRandomSpawnPosition(out Vector3 spawnPosition)
    {
        spawnPosition = Vector3.zero;

        Vector2 randomDirection =
            UnityEngine.Random.insideUnitCircle.normalized;

        float distance = UnityEngine.Random.Range(
            minSpawnDistance,
            maxSpawnDistance
        );

        Vector3 position = player.position + new Vector3(
            randomDirection.x,
            0f,
            randomDirection.y
        ) * distance;

        // Đưa điểm bắt đầu raycast lên cao
        Vector3 rayOrigin = position + Vector3.up * raycastHeight;

        if (Physics.Raycast(
            rayOrigin,
            Vector3.down,
            out RaycastHit hit,
            raycastDistance,
            groundLayer
        ))
        {
            spawnPosition = hit.point;
            return true;
        }

        return false;
    }

    private int GetZombieCount()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Length;
    }
}