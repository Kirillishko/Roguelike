using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputSystem : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private float _time = 0f;
    private bool _started;

    private InputActions _inputActions;

    private void Start()
    {
        _inputActions = InputManager.Instance.InputActions;

        _inputActions.Player.Check.started += _ => Started();
        _inputActions.Player.Check.performed += _ => Performed();
        _inputActions.Player.Check.canceled += _ => Canceled();
    }

    private void Update()
    {
        if (_started)
            _time += Time.deltaTime;
    }

    private void Started()
    {
        _time = 0f;
        _slider.value = 50;
        _started = true;
        
        Debug.Log("Started");
        Debug.Log(_time);
    }

    private void Performed()
    {
        _slider.value = 100;
        _started = false;
        
        Debug.Log("Performed");
        Debug.Log(_time);
    }

    private void Canceled()
    {
        _slider.value = 0;
        _started = false;
        
        Debug.Log("Canceled");
        Debug.Log(_time);
    }
}
