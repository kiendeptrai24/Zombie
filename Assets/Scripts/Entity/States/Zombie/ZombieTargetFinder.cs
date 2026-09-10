using UnityEngine;

public class ZombieTargetFinder : MonoBehaviour
{
    [SerializeField] private float searchRadius = 20f;

    private Transform currentTarget;

    public Transform CurrentTarget => currentTarget;

    private void OnEnable()
    {
        FindNearestPlayer();
    }
    private void Update()
    {
        LookAtPlayer();
    }

    private void FindNearestPlayer()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(
                transform.position,
                player.transform.position
            );

            if (distance < closestDistance && distance <= searchRadius)
            {
                closestDistance = distance;
                nearestPlayer = player.transform;
            }
        }

        currentTarget = nearestPlayer;
    }
    private void LookAtPlayer()
    {
        if (CurrentTarget == null)
            return;

        Vector3 direction =
            CurrentTarget.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}