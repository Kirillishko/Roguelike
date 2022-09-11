using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float _jumpModifier;
    [SerializeField] float _step;

    private bool _isJumped = false;
    private float _currentStep;

    private void Move(Vector3 direction)
    {
        float speed = _speed;

        if (direction.y != 0)
        {
            //_isJumped = true;
            direction.y = 0;
            _currentStep = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            speed = _accelerationSpeed;

        transform.Translate(speed * Time.deltaTime * direction, Space.World);
    }

    private void Update()
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

        Move(direction);

        //if (_isJumped == false)
        //    return;

        //_currentStep += _step * Time.deltaTime;

        //Vector3 nextPosition = transform.position;

        //if (_currentStep >= 1)
        //{
        //    _isJumped = false;
        //    _currentStep = 1;
        //}

        //nextPosition.y += _curve.Evaluate(_currentStep) * _jumpModifier;
        //Vector3 direction = (nextPosition - transform.position).normalized;
        //transform.position = nextPosition;
    }
}
