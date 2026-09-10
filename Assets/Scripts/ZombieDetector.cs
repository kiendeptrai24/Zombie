using UnityEngine;

public class ZombieDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private float interval = .3f;
    [SerializeField] private float timer;
    private Transform nearestZombie;
    [SerializeField] private Transform gunPoint;

    public Transform NearestZombie => nearestZombie;
    public void SetDetectionRadius(float radius) => detectionRadius = radius;
    [Space]
    [SerializeField] private float scanRadius = 10f;
    [SerializeField] private float explosionRadius = 3;
    [SerializeField] private LayerMask whatIsZombie;
    private void Update()
    {
        if (Time.time > timer + interval)
        {
            timer = Time.time;
            FindNearestZombie();
            LookAtZombie(transform);
            LookAtZombie(gunPoint);
        }
    }

    private void FindNearestZombie()
    {
        Collider[] zombies = Physics.OverlapSphere(
            transform.position,
            detectionRadius,
            zombieLayer
        );

        nearestZombie = null;

        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider zombie in zombies)
        {
            float distanceSqr =
                (zombie.transform.position - transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                nearestZombie = zombie.transform;
            }
        }
    }
    private void LookAtZombie(Transform point)
    {
        if (nearestZombie == null)
            return;

        Vector3 direction =
            nearestZombie.position - point.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            point.rotation = Quaternion.LookRotation(direction);
        }
    }
    public Vector3 FindBestBombPosition()
    {
        Collider[] zombies = Physics.OverlapSphere(
            transform.position,
            scanRadius,
            whatIsZombie
        );
        Vector3 bestPosition = transform.position;
        int maxCount = 0;

        foreach (Collider candidate in zombies)
        {
            Vector3 candidatePosition = candidate.transform.position;

            int count = 0;

            foreach (Collider zombie in zombies)
            {
                float distance = Vector3.Distance(
                    candidatePosition,
                    zombie.transform.position
                );

                if (distance <= explosionRadius)
                {
                    count++;
                }
            }

            if (count > maxCount)
            {
                maxCount = count;
                bestPosition = candidatePosition;
            }
        }

        return bestPosition;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
