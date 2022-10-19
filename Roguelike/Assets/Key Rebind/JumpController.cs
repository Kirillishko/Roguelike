using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    private InputActions gameActions;

    private Rigidbody rb;
    private Vector2 _direction;
    private InputAction _move;
    private InputManager _input;

    private void OnEnable()
    {
        if (_input == null)
            _input = InputManager.Instance;
        
        gameActions = _input.InputActions;

        gameActions.Player.Jump.started += context => DoJump();
        gameActions.Player.Enable();
        _move = gameActions.Player.Move;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TryMove();
    }

    private void DoJump()
    {
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    private void TryMove()
    {
        var value = _move.ReadValue<Vector2>();
        
        if (value == Vector2.zero)
            return;
        
        var direction = new Vector3(value.x, 0, value.y);
        rb.AddForce(direction / 10, ForceMode.Impulse);
    }
}
