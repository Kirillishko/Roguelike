using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SurfaceSlider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _jumpForce;
    private SurfaceSlider _surfaceSlider;
    private Rigidbody _rigidbody;
    private bool _isJumped = false;
    private float _jumpTime;
    private float _jumpDeadZone = 0.2f;
    private const float _minJumpTime = 0.01f;
    private const float _maxJumpTime = 0.1f;

    private void Start()
    {
        _surfaceSlider = GetComponent<SurfaceSlider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TryGetDirection(out var direction))
            Move(direction);

        if (TryGetJumpTime(out var jumpTimeInProcent))
            Jump(jumpTimeInProcent * _jumpForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumped == false)
            return;

        if (collision.transform.TryGetComponent(out Surface surface))
            _isJumped = false;
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += transform.forward;
        if (Input.GetKey(KeyCode.S))
            direction -= transform.forward;
        if (Input.GetKey(KeyCode.D))
            direction += transform.right;
        if (Input.GetKey(KeyCode.A))
            direction -= transform.right;

        return direction != Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        float speed = _speed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = _accelerationSpeed;

        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private bool TryGetJumpTime(out float jumpTimeInProcent)
    {
        jumpTimeInProcent = 0;

        if (Input.GetKey(KeyCode.Space) && _isJumped == false)
        {
            _jumpTime += Time.deltaTime;

            if (_jumpTime >= _maxJumpTime)
            {
                jumpTimeInProcent = (_jumpTime - _minJumpTime) / ((_maxJumpTime - _minJumpTime) / 100) / 100;
                _jumpTime = 0;
                return true;
            }
        }
        else if (_jumpTime != 0 && _isJumped == false)
        {
            jumpTimeInProcent = (_jumpTime - _minJumpTime) / ((_maxJumpTime - _minJumpTime) / 100) / 100;
            jumpTimeInProcent = Mathf.Max(_jumpDeadZone, jumpTimeInProcent);
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
