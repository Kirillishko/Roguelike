using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BindingUI : MonoBehaviour
{
    private InputActions _inputActions;
    public PlayerInput _playerInput;
    [SerializeField] private InputActionReference _jumpAction;

    private void OnEnable()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();

        _inputActions.Player.Jump.performed += _ => Jump();
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= _ => Jump();
        
        _inputActions.Disable();
    }

    public void Jump()
    {
        Debug.Log("Space");
    }

    public void OnStart()
    {
        _jumpAction.action.Disable();
    }
    
    public void OnEnd()
    {
        _jumpAction.action.Enable();
    }
}
