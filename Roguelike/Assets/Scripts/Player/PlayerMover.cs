using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(SurfaceSlider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _jumpForce;
    
    private InputAction _moveInput;
    private InputAction _jumpInput;
    private SurfaceSlider _surfaceSlider;
    private Rigidbody _rigidbody;
    private bool _isJumped;
    private float _jumpTime;
    
    private readonly float _jumpDeadZone = 0.2f;
    private const float _minJumpTime = 0.01f;
    private const float _maxJumpTime = 0.1f;

    private void Start()
    {
        _moveInput = InputManager.Instance.InputActions.Player.Move;
        _jumpInput = InputManager.Instance.InputActions.Player.Jump;

        _surfaceSlider = GetComponent<SurfaceSlider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TryGetDirection(out var direction))
            Move(direction);

        if (TryGetJumpTime(out var jumpTimeInPercent))
            Jump(jumpTimeInPercent * _jumpForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumped == false)
            return;

        if (collision.transform.TryGetComponent(out Surface _))
            _isJumped = false;
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        var value = _moveInput.ReadValue<Vector2>();
        
        direction = new Vector3(value.x, 0, value.y);
        direction = transform.TransformDirection(direction);

        return direction != Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        var speed = _speed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = _accelerationSpeed;

        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private bool TryGetJumpTime(out float jumpTimeInPercent)
    {
        jumpTimeInPercent = 0;

        if (_isJumped)
            return false;

        var isJumpButtonPressed = _jumpInput.ReadValue<float>() > 0.5f;
        
        if (isJumpButtonPressed)
        {
            _jumpTime += Time.deltaTime;
        
            if (_jumpTime >= _maxJumpTime)
            {
                jumpTimeInPercent = (_jumpTime - _minJumpTime) / ((_maxJumpTime - _minJumpTime) / 100) / 100;
                _jumpTime = 0;
                return true;
            }
        }
        else if (_jumpTime != 0)
        {
            jumpTimeInPercent = (_jumpTime - _minJumpTime) / ((_maxJumpTime - _minJumpTime) / 100) / 100;
            jumpTimeInPercent = Mathf.Max(_jumpDeadZone, jumpTimeInPercent);
            _jumpTime = 0;
            return true;
        }

        return false;
    }

    private void Jump(float jumpForce)
    {
        _isJumped = true;
        _rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }
}
