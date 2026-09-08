using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : KienMonoBehaviour
{
    public Animator anim;
    public IStateMachine machine;
    private PlayerMovement playerMovement;
    public Vector3 GetDir() => new Vector3(playerMovement.movementDir.x, 0, playerMovement.movementDir.y);
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        machine = new PlayerStateMachine(this);
    }
    protected override void Start()
    {
        base.Start();
        machine.Init<Player_IdleState>();
    }
    private void Update()
    {
        machine?.Update();
    }
}
