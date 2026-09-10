using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : KienMonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameManger gameManger;

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

    private readonly List<GameObject> zombies = new();

    protected override void Start()
    {
        ResetSpawner();

        if (gameManger != null)
        {
            gameManger.OnGameEnded += ReleaseZombie;
        }
    }

    private void OnDisable()
    {
        if (gameManger != null)
        {
            gameManger.OnGameEnded -= ReleaseZombie;
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

        // Không cần FindGameObjectsWithTag
        if (zombies.Count >= maxZombieCount)
            return;

        if (!TryGetRandomSpawnPosition(out Vector3 spawnPosition))
            return;

        GameObject zombie = ObjectPool.Instance.GetObject(
            zombiePrefab,
            spawnPosition,
            Quaternion.identity
        );

        if (zombie != null)
        {
            zombies.Add(zombie);
        }
    }

    private bool TryGetRandomSpawnPosition(out Vector3 spawnPosition)
    {
        spawnPosition = Vector3.zero;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float distance = Random.Range(
            minSpawnDistance,
            maxSpawnDistance
        );

        Vector3 position = player.position + new Vector3(
            randomDirection.x,
            0f,
            randomDirection.y
        ) * distance;

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

    public void ReleaseZombieDead(GameObject zombie)
    {
        if (zombie == null)
            return;

        zombies.Remove(zombie);
    }

    private void ReleaseZombie()
    {
        if (zombies.Count == 0)
            return;

        for (int i = zombies.Count - 1; i >= 0; i--)
        {
            GameObject zombie = zombies[i];

            if (zombie != null)
            {
                ObjectPool.Instance.ReturnObject(zombie);
            }
        }

        zombies.Clear();
        ResetSpawner();
    }
    private void ResetSpawner()
    {
        currentSpawnInterval = startSpawnInterval;
        spawnTimer = currentSpawnInterval;
    }
}