using System;
using UnityEngine;

public class PlayerMovement : KienMonoBehaviour
{
    private InputManager input;
    private Animator anim;
    private Rigidbody rb;
    public Vector2 movementDir;

    public float moveSpeed = 12f;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        
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

    
}
