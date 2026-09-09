using System;
using UnityEngine;

public class ZombieBombTarget : MonoBehaviour
{
    [SerializeField] private float scanRadius = 10f;
    [SerializeField] private float explosionRadius = 3;
    [SerializeField] private LayerMask whatIsZombie;
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
}