

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;


public class InputManager : Singleton<InputManager>
{
    public InputHandler inputHandler;
    protected override void Awake()
    {
        base.Awake();
        inputHandler = new InputHandler();
    }
    void OnEnable()
    {
        TurnOnAllInput();
        TurnOnPlayerInput();
    }
    void OnDisable()
    {
        TurnOffAllInput();
        TurnOffPlayerInput();
    }
    public Vector2 GetInputDirection()
    {
        if (!inputHandler.Player.enabled) return Vector2.zero;
        return inputHandler.Player.Move.ReadValue<Vector2>();
    }

    #region Toggle Player

    public void TurnOnPlayerInput()
    {
        inputHandler.Player.Enable();
    }
    public void TurnOffPlayerInput()
    {
        inputHandler.Player.Disable();
    }
    #endregion

    public void TurnOffAllInput()
    {
        inputHandler.Disable();
    }
    public void TurnOnAllInput()
    {
        inputHandler.Enable();
    }
}