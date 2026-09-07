using UnityEngine;

public class ZombieDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private float interval = .3f;
    [SerializeField] private float timer;
    private Transform nearestZombie;

    public Transform NearestZombie => nearestZombie;
    public void SetDetectionRadius(float radius) => detectionRadius = radius;
    private void Update()
    {
        if (Time.time > timer + interval)
        {
            timer = Time.time;
            FindNearestZombie();
        }
        LookAtZombie();
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
    private void LookAtZombie()
    {
        if (nearestZombie == null)
            return;

        Vector3 direction =
            nearestZombie.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
