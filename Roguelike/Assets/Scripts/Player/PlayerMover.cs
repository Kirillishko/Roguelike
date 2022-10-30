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
    
    private const float _jumpDeadZone = 0.2f;
    private const float _minJumpTime = 0.01f;
    private const float _maxJumpTime = 0.1f;
    private const float _gravity = -9.81f;

    private void Start()
    {
        _moveInput = InputManager.Instance.InputActions.Player.Move;
        _jumpInput = InputManager.Instance.InputActions.Player.Jump;
        
        _characterController = GetComponent<CharacterController>();
        
        _lengthGroundChecking = _characterController.height / 2 + 0.1f;
    }

    private void Update()
    {
        CheckGrounded();
        
        if (_isGrounded)
            _fallingVelocity.y = 0f;

        if (TryGetDirection(out var direction))
            Move(direction);

        if (TryGetJumpTime(out var jumpTimeInPercent))
            Jump(jumpTimeInPercent * _jumpForce);
        
        TryEmulateGravity();
        _characterController.Move(_fallingVelocity * Time.deltaTime);
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        var value = _moveInput.ReadValue<Vector2>();
        direction = transform.forward * value.y + transform.right * value.x;
        return direction != Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        var speed = _speed;

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
        _fallingVelocity.y = Mathf.Sqrt(jumpForce * -2f * _gravity);
    }

    private void TryEmulateGravity()
    {
        var velocity = _gravity * Time.deltaTime;
        _fallingVelocity.y += velocity;
    }

    private void CheckGrounded()
    {
        var ray = new Ray(transform.position, transform.up * -1);
        _isGrounded = Physics.Raycast(ray, _lengthGroundChecking);
    }
}
