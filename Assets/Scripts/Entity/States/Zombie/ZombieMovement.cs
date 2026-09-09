using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private ZombieTargetFinder targetFinder;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetFinder = GetComponent<ZombieTargetFinder>();
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (agent.isActiveAndEnabled == false) return;
        Transform target = targetFinder.CurrentTarget;

        if (target == null)
            return;

        agent.SetDestination(target.position);
    }
}