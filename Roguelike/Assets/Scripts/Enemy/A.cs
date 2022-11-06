using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    [SerializeField] private float _duration;
    
    private float _timer;
    private B _b;
    
    private void Awake()
    {
        _b = new B();
        Debug.Log("A Awake");
    }

    private void OnEnable()
    {
        Debug.Log("A OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("A OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("A OnDestroy");
    }

    private void Start()
    {
        Debug.Log("A Start");
    }
    
    private void Update()
    {
        Debug.Log("A Update");

        _timer += Time.deltaTime;
        
        if (_timer >= _duration)
            Destroy(_b);
    }
}
