using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _jumpForce;

    private CharacterController _characterController;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    private bool _isGrounded;
    private float _jumpTime;
    private Vector3 _fallingVelocity;
    private float _lengthGroundChecking;
    private Vector3 _groundCheckPosition;
    private Vector3 _groundCheckSize = Vector3.one;
    private float _height;
    
    private const float JumpDeadZone = 0.2f;
    private const float MinJumpTime = 0.01f;
    private const float MaxJumpTime = 0.1f;
    private const float Gravity = -9.81f;
    private const float GroundCheckYOffset = 0.055f;

    private void Start()
    {
        _moveInput = InputManager.Instance.InputActions.Player.Move;
        _jumpInput = InputManager.Instance.InputActions.Player.Jump;
        
        _characterController = GetComponent<CharacterController>();

        _height = _characterController.height;
        _lengthGroundChecking = _height / 2 + 0.1f;
        _groundCheckSize.y = 0.1f;
    }

    private void Update()
    {
        //CheckGrounded();
        
        if (_isGrounded)
            _fallingVelocity.y = 0f;

        if (TryGetDirection(out var direction))
            Move(direction);

        if (TryGetJumpTime(out var jumpTimeInPercent))
            Jump(jumpTimeInPercent * _jumpForce);
        
        TryEmulateGravity();
        _characterController.Move(_fallingVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _isGrounded = false;
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        var value = _moveInput.ReadValue<Vector2>();
        direction = transform.forward * value.y + transform.right * value.x;
        return direction != Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        float speed = _speed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = _accelerationSpeed;
        
        var offset = direction * (speed * Time.deltaTime);
        _characterController.Move(offset);
    }

    private bool TryGetJumpTime(out float jumpTimeInPercent)
    {
        jumpTimeInPercent = 0;

        if (_isGrounded == false)
            return false;

        bool isJumpButtonPressed = _jumpInput.ReadValue<float>() > 0.5f;
        
        if (isJumpButtonPressed)
        {
            _jumpTime += Time.deltaTime;
        
            if (_jumpTime >= MaxJumpTime)
            {
                jumpTimeInPercent = (_jumpTime - MinJumpTime) / ((MaxJumpTime - MinJumpTime) / 100) / 100;
                _jumpTime = 0;
                return true;
            }
        }
        else if (_jumpTime != 0)
        {
            jumpTimeInPercent = (_jumpTime - MinJumpTime) / ((MaxJumpTime - MinJumpTime) / 100) / 100;
            jumpTimeInPercent = Mathf.Max(JumpDeadZone, jumpTimeInPercent);
            _jumpTime = 0;
            return true;
        }

        return false;
    }

    private void Jump(float jumpForce)
    {
        _fallingVelocity.y = Mathf.Sqrt(jumpForce * -2f * Gravity);
    }

    private void TryEmulateGravity()
    {
        float velocity = Gravity * Time.deltaTime;
        _fallingVelocity.y += velocity;
    }

    private void CheckGrounded()
    {
        var groundCheckPosition = transform.position;
        groundCheckPosition.y -= _height / 2 + GroundCheckYOffset;

        var raycastHits = Array.Empty<RaycastHit>();
        
        _isGrounded = Physics.BoxCastNonAlloc(groundCheckPosition, _groundCheckSize, Vector3.zero, raycastHits) > 0;


        // var ray = new Ray(transform.position, transform.up * -1);
        // _isGrounded = Physics.Raycast(ray, _lengthGroundChecking);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var groundCheckPosition = transform.position;
        groundCheckPosition.y -= _height / 2 + GroundCheckYOffset;

        Gizmos.DrawWireCube(groundCheckPosition, _groundCheckSize);
    }
}
