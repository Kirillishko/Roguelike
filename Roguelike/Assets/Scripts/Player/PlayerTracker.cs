using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _sensibility;
    [SerializeField] private float _movementEffectPower;
    [SerializeField] private float _maxMovementEffectPower;

    private InputActions _input;
    private Vector3 _currentRotation = new Vector3(0, 0, 0);

    private void Start()
    {
        _input = InputManager.InputActions;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseRotate();

        var direction = _input.Player.Move.ReadValue<Vector2>();
        var directionX = direction.x;

        //if (Input.GetKey(KeyCode.A))
        //    directionX = 1;
        //if (Input.GetKey(KeyCode.D))
        //    directionX = -1;

        Rotate(directionX);
    }

    private void MouseRotate()
    {
        float x = Input.GetAxisRaw("Mouse X") * _sensibility;
        float y = Input.GetAxisRaw("Mouse Y") * _sensibility;

        _currentRotation.x -= y;
        _currentRotation.y += x;
        _currentRotation.x = Mathf.Clamp(_currentRotation.x, -90, 90);

        transform.eulerAngles = _currentRotation;
        _player.transform.eulerAngles = new Vector3(0, _currentRotation.y, 0);
    }

    private void Rotate(float directionX)
    {
        float endValue = _maxMovementEffectPower * directionX;
        float step = _movementEffectPower * Time.deltaTime;

        _currentRotation.z = Mathf.Lerp(_currentRotation.z, endValue, step);
    }
}
