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
    private float _jumpTime = 17.2f;
    private const float _MinJumpTime = 10f;
    private const float _MaxJumpTime = 20f;

    private void Start()
    {
        _surfaceSlider = GetComponent<SurfaceSlider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = GetDirection();
        Move(direction);

        if (Input.GetKey(KeyCode.Space) && _isJumped == false)
        {
            _jumpTime += Time.deltaTime;

            if (_jumpTime >= _MaxJumpTime)
            {
                float jumpForce = Mathf.Lerp(_MinJumpTime, _MaxJumpTime, _jumpTime) * _jumpForce;
                Jump(jumpForce);
                _jumpTime = 0;
            }
        }
        else if (_jumpTime != 0 && _isJumped == false)
        {
            float jumpForce = Mathf.Lerp(_MinJumpTime, _MaxJumpTime, _jumpTime);
            jumpForce = Mathf.Max(_MinJumpTime, jumpForce) * _jumpForce;
            Jump(jumpForce);
            _jumpTime = 0;
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += transform.forward;
        if (Input.GetKey(KeyCode.S))
            direction -= transform.forward;
        if (Input.GetKey(KeyCode.D))
            direction += transform.right;
        if (Input.GetKey(KeyCode.A))
            direction -= transform.right;

        return direction;
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

    private void Jump(float jumpForce)
    {
        _isJumped = true;
        _rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumped == false)
            return;

        if (collision.transform.TryGetComponent(out Surface surface))
            _isJumped = false;
    }

}
