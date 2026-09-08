using System;
using UnityEngine;

public class PlayerMovement : KienMonoBehaviour
{
    private InputManager input;
    private Rigidbody rb;
    public Vector2 movementDir;
    private ZombieDetector zombieDetector;
    public float moveSpeed = 12f;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        zombieDetector = GetComponent<ZombieDetector>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    protected override void Start()
    {
        base.Start();
        input = InputManager.Instance;
    }

    private void Update()
    {
        if (input == null) return;

        movementDir = input.GetInputDirection();
        LookAtZombie();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(
            movementDir.x,
            0f,
            movementDir.y
        );

        rb.linearVelocity = dir * moveSpeed;
    }
    private void LookAtZombie()
    {
        if (movementDir.magnitude < .001f || zombieDetector.NearestZombie != null)
            return;

        Vector3 dir = new Vector3(
            movementDir.x,
            0f,
            movementDir.y
        );

        if (dir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

}
